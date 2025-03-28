using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Utilities;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

namespace SoccerPerformanceDetail.Services;

public interface LeagueExcelService
{
    Task<ExcelPackage> ExportExcelLeaguePerformance(LeaguePerformanceList data,
        GetLeaguePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes);
}

public class LeagueExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : LeagueExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private const int StartFilterBlockRowIndex = 3;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelLeaguePerformance(LeaguePerformanceList data, GetLeaguePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups, IEnumerable<BetTypeItem> betTypes)
    {
        LoadTemplate("League.xlsx");
        var rowIndex = BuildFilterBlock(criteria, leagueGroups, betTypes);
        var startBodyRowIndex = rowIndex + 4;
        var bodyData = criteria.MarketFilter switch
        {
            Market.NonLive => data.ItemNonLiveList,
            Market.Live => data.ItemLiveList,
            _ => data.ItemByOrderList
        };
        BuildBodyRows(bodyData, startBodyRowIndex);
        var startTotalRowIndex = data.ItemByOrderList.Count + startBodyRowIndex;
        BuildTotalRow(data.TotalItemLive, data.TotalItemNonLive, startTotalRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage!);
    }

    private int BuildFilterBlock(GetLeaguePerformanceCriteria criteria, IEnumerable<LeagueGroup> leagueGroups, IEnumerable<BetTypeItem> betTypes)
    {
        var rowIndex = StartFilterBlockRowIndex;
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.MarketFilter;
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.FromDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = criteria.ToDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:O{rowIndex}"].Value =
            ModelsHelper.MakeBetTypeSelectString(betTypes, criteria.BetTypes);
        var lineCount = (int)Math.Ceiling((double)sheet!.Cells[$"B{rowIndex}:O{rowIndex}"].Text.Length / ExcelConstants.Width);
        if (lineCount > 1)
        {
            sheet.Row(rowIndex).Height = 8 * lineCount;
        }

        rowIndex++;
        if (criteria.IsVirtual)
        {
            if (criteria.Turnover.HasValue)
            {
                sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value =
                    criteria.Turnover.Value.ToString("#,##0.00");
            }
            else
            {
                sheet!.DeleteRow(rowIndex);
            }

            if (criteria.WinLoss.HasValue)
            {
                sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value =
                    criteria.WinLoss.Value.ToString("#,##0.00");
            }
            else
            {
                sheet!.DeleteRow(rowIndex);
            }
        }

        if (criteria.IsReal)
        {
            if (criteria.TurnoverReal.HasValue)
            {
                sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value =
                    criteria.TurnoverReal.Value.ToString("#,##0.00");
            }
            else
            {
                sheet!.DeleteRow(rowIndex);
            }

            if (criteria.WinLossReal.HasValue)
            {
                sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value =
                    criteria.WinLossReal.Value.ToString("#,##0.00");
            }
            else
            {
                sheet!.DeleteRow(rowIndex);
            }
        }

        if (criteria.IsVirtual && criteria.MarginFrom.HasValue || criteria.MarginTo.HasValue)
        {
            var marginString = GenerateMarginString(criteria.MarginFrom, criteria.MarginTo);
            sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = marginString;
        }
        else
        {
            sheet!.DeleteRow(rowIndex);
        }

        if (criteria.IsReal && criteria.MarginFromReal.HasValue || criteria.MarginToReal.HasValue)
        {
            var marginString = GenerateMarginString(criteria.MarginFromReal, criteria.MarginToReal);
            sheet!.Cells[$"B{rowIndex}:O{rowIndex++}"].Value = marginString;
        }
        else
        {
            sheet!.DeleteRow(rowIndex);
        }

        sheet!.Cells[$"B{rowIndex}:O{rowIndex}"].Value = ModelsHelper.MakeLeagueGroupSelectString(criteria.LeagueGroupIds, leagueGroups);

        return rowIndex;
    }

    private void BuildBodyRows(IEnumerable<LeaguePerformanceItem> list, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<LeaguePerformanceExcelItem>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            item.Market = item.IsLive ? ExcelConstants.LiveText : ExcelConstants.NonLiveText;
            sheet.ApplyModel(template, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    private void BuildTotalRow(LeaguePerformanceItem liveData, LeaguePerformanceItem nonLivedata, int startIndex)
    {
        var excelModel = mapper.Map<LeaguePerformanceExcelItem>(liveData);
        excelModel.Market = ExcelConstants.LiveText;
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
        UpdateValueSpecificColumns(startIndex, excelModel);
        excelModel = mapper.Map<LeaguePerformanceExcelItem>(nonLivedata);
        excelModel.Market = ExcelConstants.NonLiveText;
        sheet.ApplyModel(totalSheet, excelModel, startIndex + 1);
        UpdateValueSpecificColumns(startIndex + 1, excelModel);
        var cellTotalAddress = $"A{startIndex}:B{startIndex + 1}";
        sheet!.Cells[cellTotalAddress].Merge = true;
        sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
    }

    private void UpdateValueSpecificColumns(int startIndex, LeaguePerformanceExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"K{startIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"O{startIndex}"], model.MarginReal);
        sheet!.Cells[$"I{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.Turnover / model.NoOfMatches : ExcelConstants.NotAvailableText;
        sheet!.Cells[$"M{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.TurnoverReal / model.NoOfMatches : ExcelConstants.NotAvailableText;
    }

    private string GenerateMarginString(decimal? marginFrom, decimal? marginTo)
    {
        var marginFromString = marginFrom != null ? marginFrom.ToString() : "min";
        var marginToString = marginTo != null ? marginTo.ToString() : "max";

        if (marginFrom == null)
            return $"≤ {marginTo}";
        return marginTo == null ? $"≥ {marginFrom}" : $"from {marginFromString} to {marginToString}";
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SoccerPerformanceDetail/League/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["League Performance"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void ClearTemplate()
    {
        ExcelPackage!.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage!.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage!.Workbook.Worksheets.Delete("TotalTemplate");
    }
}
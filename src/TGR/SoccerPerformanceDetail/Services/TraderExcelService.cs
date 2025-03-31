using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Utilities;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

namespace SoccerPerformanceDetail.Services;

public interface TraderExcelService
{
    Task<ExcelPackage> ExportExcelTraderPerformance(TraderPerformanceList data,
        GetTraderPerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes);
}

public class TraderExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : TraderExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet titleSheet;

    private ExcelWorksheet bodySheet;

    private ExcelWorksheet totalSheet;

    private const int StartFilterBlockRowIndex = 3;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelTraderPerformance(TraderPerformanceList data, GetTraderPerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups, IEnumerable<BetTypeItem> betTypes)
    {
        LoadTemplate("Trader.xlsx");
        var rowIndex = BuildFilterBlock(criteria, leagueGroups, betTypes);
        var currentRowIndex = rowIndex + 4;
        if (data.ItemLiveList.Any())
        {
            BuildBodyRows(data.ItemLiveList, currentRowIndex, ExcelConstants.LiveText);
            currentRowIndex += data.ItemLiveList.Count;
        }

        if (data.ItemNonLiveList.Any())
        {
            BuildBodyRows(data.ItemNonLiveList, currentRowIndex + 1, ExcelConstants.NonLiveText);
            currentRowIndex += data.ItemNonLiveList.Count + 1;
        }

        BuildTotalRow(data.Total, currentRowIndex + 1);
        ClearTemplate();
        return Task.FromResult(ExcelPackage!);
    }

    private int BuildFilterBlock(GetTraderPerformanceCriteria criteria, IEnumerable<LeagueGroup> leagueGroups, IEnumerable<BetTypeItem> betTypes)
    {
        var rowIndex = StartFilterBlockRowIndex;
        sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value = criteria.MarketFilter;
        sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value = criteria.FromDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value = criteria.ToDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:P{rowIndex}"].Value =
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
                sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value =
                    criteria.Turnover.Value.ToString("#,##0.00");
            }
            else
            {
                sheet!.DeleteRow(rowIndex);
            }

            if (criteria.WinLoss.HasValue)
            {
                sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value =
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
                sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value =
                    criteria.TurnoverReal.Value.ToString("#,##0.00");
            }
            else
            {
                sheet!.DeleteRow(rowIndex);
            }

            if (criteria.WinLossReal.HasValue)
            {
                sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value =
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
            sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value = marginString;
        }
        else
        {
            sheet!.DeleteRow(rowIndex);
        }

        if (criteria.IsReal && criteria.MarginFromReal.HasValue || criteria.MarginToReal.HasValue)
        {
            var marginString = GenerateMarginString(criteria.MarginFromReal, criteria.MarginToReal);
            sheet!.Cells[$"B{rowIndex}:P{rowIndex++}"].Value = marginString;
        }
        else
        {
            sheet!.DeleteRow(rowIndex);
        }

        sheet!.Cells[$"B{rowIndex}:P{rowIndex}"].Value = ModelsHelper.MakeLeagueGroupSelectString(criteria.LeagueGroupIds, leagueGroups);

        return rowIndex;
    }

    private void BuildBodyRows(IEnumerable<TraderPerformanceItem> list, int startRowIndex, string title)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var titleExcelItem = new TraderPerformanceExcelTitleItem
        {
            Title = title
        };
        sheet.ApplyModel(titleSheet, titleExcelItem, rowIndex++);
        var excelModels = mapper.Map<IEnumerable<TraderPerformanceExcelItem>>(list);
        foreach (var item in excelModels)
        {
            item.No = index++;
            sheet.ApplyModel(bodySheet, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    private void BuildTotalRow(TraderPerformanceItem data, int startIndex)
    {
        var excelModel = mapper.Map<TraderPerformanceExcelItem>(data);
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
        UpdateValueSpecificColumns(startIndex, excelModel);
        var cellTotalAddress = $"A{startIndex}:D{startIndex}";
        sheet!.Cells[cellTotalAddress].Merge = true;
        sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
    }

    private void UpdateValueSpecificColumns(int startIndex, TraderPerformanceExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"H{startIndex}"], model.Score);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"L{startIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"P{startIndex}"], model.MarginReal);
        sheet!.Cells[$"J{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.Turnover / model.NoOfMatches : ExcelConstants.NotAvailableText;
        sheet!.Cells[$"N{startIndex}"].Value =
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
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SoccerPerformanceDetail/Trader/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Trader Performance"];
        titleSheet = ExcelPackage.Workbook.Worksheets["TitleTemplate"];
        bodySheet = ExcelPackage.Workbook.Worksheets["BodyTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void ClearTemplate()
    {
        ExcelPackage!.Workbook.Worksheets.Delete("TitleTemplate");
        ExcelPackage!.Workbook.Worksheets.Delete("BodyTemplate");
        ExcelPackage!.Workbook.Worksheets.Delete("TotalTemplate");
    }
}
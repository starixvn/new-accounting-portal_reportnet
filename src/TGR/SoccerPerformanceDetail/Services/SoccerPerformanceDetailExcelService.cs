using AutoMapper;
using Epplus.Mapper.Extensions;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

namespace SoccerPerformanceDetail.Services;

public interface SoccerPerformanceDetailExcelService
{
    Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypePerformanceList data,
        GetBetTypePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes);
}

public class SoccerPerformanceDetailExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : SoccerPerformanceDetailExcelService
{
    private ExcelWorksheet? sheet;

    private ExcelWorksheet? oddSheet;

    private ExcelWorksheet? evenSheet;

    private ExcelWorksheet? totalSheet;

    private const int StartRowIndex = 5;

    private ExcelPackage? ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypePerformanceList data,
        GetBetTypePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes)
    {
        LoadTemplate("BetType.xlsx");
        BuildFilterBlock(criteria, leagueGroups, betTypes);
        var bodyData = criteria.MarketFilter switch
        {
            Market.NonLive => data.ItemNonLiveList,
            Market.Live => data.ItemLiveList,
            _ => data.ItemByOrderList
        };
        BuildBodyRows(bodyData, StartRowIndex);
        var startTotalRowIndex = data.ItemByOrderList.Count + StartRowIndex;
        BuildTotalRow(data.TotalItemLive, data.TotalItemNonLive, startTotalRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage!);
    }

    private void BuildFilterBlock(GetBetTypePerformanceCriteria criteria, IEnumerable<LeagueGroup> leagueGroups, IEnumerable<BetTypeItem> betTypes)
    {
        var cellAddress = sheet!.Cells["A2:O2"];
        cellAddress.Merge = true;
        cellAddress.IsRichText = true;
        cellAddress.Style.Font.Size = 8;
        cellAddress.Style.WrapText = true;
        var richText = cellAddress.RichText;
        richText.Add("Market: ").Bold = true;
        richText.Add($"{criteria.MarketFilter.ToString()}").Bold = false;
        richText.Add("\nFrom Date: ").Bold = true;
        richText.Add($"{criteria.FromDate:MM/dd/yyyy} - ").Bold = false;
        richText.Add("To Date: ").Bold = true;
        richText.Add($"{criteria.ToDate:MM/dd/yyyy}").Bold = false;
        richText.Add("\nBetType: ").Bold = true;
        richText.Add($"{MakeBetTypeSelectString(betTypes, criteria.Bettypes)}").Bold = false;
        if (criteria.IsVirtual)
        {
            if (criteria.Turnover.HasValue)
            {
                richText.Add("\nMinimum Virtual Turnover: ").Bold = true;
                richText.Add($"{criteria.Turnover:#,##0.00}").Bold = false;
            }

            if (criteria.WinLoss.HasValue)
            {
                richText.Add("\nMinimum Virtual WinLoss: ").Bold = true;
                richText.Add($"{criteria.WinLoss:#,##0.00}").Bold = false;
            }
        }

        if (criteria.IsReal)
        {
            if (criteria.TurnoverReal.HasValue)
            {
                richText.Add("\nMinimum Real Turnover: ").Bold = true;
                richText.Add($"{criteria.TurnoverReal:#,##0.00}").Bold = false;
            }

            if (criteria.WinLossReal.HasValue)
            {
                richText.Add("\nMinimum Real WinLoss: ").Bold = true;
                richText.Add($"{criteria.WinLossReal:#,##0.00}").Bold = false;
            }
        }

        if (criteria.IsVirtual && criteria.MarginFrom.HasValue || criteria.MarginTo.HasValue)
        {
            var marginString = GenerateMarginString(criteria.MarginFrom, criteria.MarginTo);
            richText.Add("\nVirtual Margin %: ").Bold = true;
            richText.Add($"{marginString}").Bold = false;
        }

        if (criteria.IsReal && criteria.MarginFromReal.HasValue || criteria.MarginToReal.HasValue)
        {
            var marginString = GenerateMarginString(criteria.MarginFromReal, criteria.MarginToReal);
            richText.Add("\nReal Margin %: ").Bold = true;
            richText.Add($"{marginString}").Bold = false;
        }

        richText.Add("\nLeague Group: ").Bold = true;
        richText.Add($"{MakeLeagueGroupSelectString(criteria.LeagueGroupIds, leagueGroups)}").Bold = false;
        
        var lineCount = (int)Math.Ceiling((double)cellAddress.Text.Length / ExcelConstants.Width);

        sheet.Row(2).Height = lineCount * 8;
    }

    private void BuildBodyRows(IEnumerable<BetTypePerformanceItem> list, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<BetTypePerformanceExcelItem>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            item.Market = item.IsLive ? ExcelConstants.LiveText : ExcelConstants.NonLiveText;
            sheet.ApplyModel(template, item, rowIndex++);
        }
    }

    private void BuildTotalRow(BetTypePerformanceItem liveData, BetTypePerformanceItem nonLivedata, int startIndex)
    {
        var excelModel = mapper.Map<BetTypePerformanceExcelItem>(liveData);
        excelModel.Market = ExcelConstants.LiveText;
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
        UpdateValueSpecificColumns(startIndex, excelModel);
        excelModel = mapper.Map<BetTypePerformanceExcelItem>(nonLivedata);
        excelModel.Market = ExcelConstants.NonLiveText;
        sheet.ApplyModel(totalSheet, excelModel, startIndex + 1);
        UpdateValueSpecificColumns(startIndex + 1, excelModel);
        var cellTotalAddress = $"A{startIndex}:B{startIndex + 1}";
        sheet!.Cells[cellTotalAddress].Merge = true;
        sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
    }

    private void UpdateValueSpecificColumns(int startIndex, BetTypePerformanceExcelItem model)
    {
        if (model.AvgToPerMatch == 0)
        {
            sheet!.Cells[$"I{startIndex}"].Value = ExcelConstants.NotAvailableText;
        }
        if (!model.Margin.HasValue)
        {
            sheet!.Cells[$"K{startIndex}"].Value = ExcelConstants.NotAvailableText;
        }
        if (model.AvgToPerMatchReal == 0)
        {
            sheet!.Cells[$"M{startIndex}"].Value = ExcelConstants.NotAvailableText;
        }
        if (!model.MarginReal.HasValue)
        {
            sheet!.Cells[$"O{startIndex}"].Value = ExcelConstants.NotAvailableText;
        }
    }

    private string GenerateMarginString(decimal? marginFrom, decimal? marginTo)
    {
        var marginFromString = marginFrom != null ? marginFrom.ToString() : "min";
        var marginToString = marginTo != null ? marginTo.ToString() : "max";

        if (marginFrom == null)
            return $"≤ {marginTo}";
        else if (marginTo == null)
            return $"≥ {marginFrom}";
        else
            return $"from {marginFromString} to {marginToString}";
    }

    private string MakeLeagueGroupSelectString(string leagueGroupIds, IEnumerable<LeagueGroup> leagueGroups)
    {
        var leagueGroupNames = string.Empty;

        if (string.IsNullOrEmpty(leagueGroupIds))
            return leagueGroupNames;

        if (leagueGroups == null || !leagueGroups.Any())
            return leagueGroupNames;

        var ids = leagueGroupIds.Split(',');

        foreach (var id in ids)
        {
            var group = leagueGroups
                .Where(x => x.TargetLeagueGroupId.ToString().Equals(id))
                .Select(x => x.LeagueGroupName).ToList();

            if (group != null && group.Any())
            {
                leagueGroupNames += group[0] + ", ";
            }
        }

        leagueGroupNames = leagueGroupNames.TrimEnd(new char[] { ' ', ',' });

        return leagueGroupNames;
    }

    private string MakeBetTypeSelectString(IEnumerable<BetTypeItem> betTypes, string betTypeIds)
    {
        if (string.IsNullOrEmpty(betTypeIds) || betTypeIds == "0")
        {
            return string.Join(", ", betTypes.Select(obj => obj.BetTypeName));
        }
        else
        {
            var betTypeList = betTypeIds.Split(',').Select(int.Parse).ToList();

            var result = betTypes.Join(betTypeList, b1 => b1.BetTypeId, b2 => b2,
                (b1, b2) => new { b1.BetTypeName }).Select(x => x.BetTypeName);

            return string.Join(", ", result);
        }
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SoccerPerformanceDetail/BetType/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Bet Type Performance"];
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
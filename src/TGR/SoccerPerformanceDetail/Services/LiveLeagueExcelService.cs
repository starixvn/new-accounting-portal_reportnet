using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using SoccerPerformanceDetail.Constants;
using SoccerPerformanceDetail.Criteria;
using SoccerPerformanceDetail.Utilities;
using SoccerPerformanceDetail.ViewModel;
using Utilities.Configurations;

namespace SoccerPerformanceDetail.Services;

public interface LiveLeagueExcelService
{
    Task<ExcelPackage> ExportExcel(LeaguePerformanceList data,
        GetLeaguePerformanceOfBetChoiceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        string betType);
}

public class LiveLeagueExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : LiveLeagueExcelService
{
    private ExcelWorksheet? sheet;

    private ExcelWorksheet? oddSheet;

    private ExcelWorksheet? evenSheet;

    private ExcelWorksheet? totalSheet;

    private const int StartFilterBlockRowIndex = 3;

    private const int StartRowIndex = 11;

    private ExcelPackage? ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(LeaguePerformanceList data, GetLeaguePerformanceOfBetChoiceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups, string betType)
    {
        LoadTemplate("LiveLeague.xlsx");
        BuildFilterBlock(criteria, leagueGroups, betType);
        var bodyData = criteria.IsLive ? data.ItemLiveList : data.ItemNonLiveList;
        BuildBodyRows(bodyData, StartRowIndex);
        var startTotalRowIndex = bodyData.Count + StartRowIndex;
        BuildTotalRow(data.Total, startTotalRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage!);
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
            sheet.ApplyModel(template, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    private void BuildTotalRow(LeaguePerformanceItem data, int startIndex)
    {
        var excelModel = mapper.Map<LeaguePerformanceExcelItem>(data);
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
        var cellTotalAddress = $"A{startIndex}:C{startIndex}";
        sheet!.Cells[cellTotalAddress].Merge = true;
        sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
        UpdateValueSpecificColumns(startIndex, excelModel);
    }

    private void UpdateValueSpecificColumns(int startIndex, LeaguePerformanceExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"I{startIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"M{startIndex}"], model.MarginReal);
        sheet!.Cells[$"G{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.Turnover / model.NoOfMatches : ExcelConstants.NotAvailableText;
        sheet!.Cells[$"K{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.TurnoverReal / model.NoOfMatches : ExcelConstants.NotAvailableText;
    }


    private void BuildFilterBlock(GetLeaguePerformanceOfBetChoiceCriteria criteria, IEnumerable<LeagueGroup> leagueGroups, string betType)
    {
        var rowIndex = StartFilterBlockRowIndex;
        sheet!.Cells[$"B{rowIndex}:M{rowIndex++}"].Value = criteria.Market switch
        {
            -1 => "All",
            1 => ExcelConstants.LiveText,
            _ => ExcelConstants.NonLiveText
        };
        sheet!.Cells[$"B{rowIndex}:M{rowIndex++}"].Value = criteria.FromDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:M{rowIndex++}"].Value = criteria.ToDate.ToString("MM/dd/yyyy");
        sheet!.Cells[$"B{rowIndex}:M{rowIndex++}"].Value = betType;
        sheet!.Cells[$"B{rowIndex}:M{rowIndex}"].Value = ModelsHelper.MakeLeagueGroupSelectString(criteria.LeagueGroupIds, leagueGroups);
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SoccerPerformanceDetail/BetType/{fileExcel}";
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
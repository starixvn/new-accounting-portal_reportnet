namespace SoccerPerformanceDetail.Services;

using AutoMapper;
using Constants;
using Criteria;
using Epplus.Mapper.Extensions;
using global::Utilities.Configurations;
using OfficeOpenXml;
using Utilities;
using ViewModel;

public interface BetChoiceExcelService
{
    Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypeBetChoiceList data,
        GetBetChoicePerformanceOfBetTypeCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        string betType);
}

public class BetChoiceExcelServiceImp(ConfigurationService configurationService,
    IMapper mapper) : BetChoiceExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private const int StartFilterBlockRowIndex = 3;

    private const int StartRowIndex = 13;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypeBetChoiceList data, GetBetChoicePerformanceOfBetTypeCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups, string betType)
    {
        LoadTemplate("BetChoice.xlsx");
        BuildFilterBlock(criteria, leagueGroups, betType);
        BuildBodyRows(data.ItemList, StartRowIndex);
        var startTotalRowIndex = data.ItemList.Count + StartRowIndex;
        BuildTotalRow(data.Total, startTotalRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage!);
    }

    private void BuildBodyRows(IEnumerable<BetTypeBetChoiceItem> list, int startRowIndex)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        var excelModels = mapper.Map<IEnumerable<BetTypeBetChoiceExcelItem>>(list);
        foreach (var item in excelModels)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            item.No = index++;
            sheet.ApplyModel(template, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    private void BuildTotalRow(BetTypeBetChoiceItem data, int startIndex)
    {
        var excelModel = mapper.Map<BetTypeBetChoiceExcelItem>(data);
        sheet.ApplyModel(totalSheet, excelModel, startIndex);
        var cellTotalAddress = $"A{startIndex}:D{startIndex}";
        sheet!.Cells[cellTotalAddress].Merge = true;
        sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
        UpdateValueSpecificColumns(startIndex, excelModel);
    }

    private void UpdateValueSpecificColumns(int startIndex, BetTypeBetChoiceExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"I{startIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"M{startIndex}"], model.MarginReal);
        sheet!.Cells[$"G{startIndex}"].Value =
            model.MatchCount > 0 ? model.Turnover / model.MatchCount : ExcelConstants.NotAvailableText;
        sheet!.Cells[$"K{startIndex}"].Value =
            model.MatchCount > 0 ? model.TurnoverReal / model.MatchCount : ExcelConstants.NotAvailableText;
    }


    private void BuildFilterBlock(GetBetChoicePerformanceOfBetTypeCriteria criteria, IEnumerable<LeagueGroup> leagueGroups, string betType)
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
        sheet!.Cells[$"B{rowIndex}:M{rowIndex++}"].Value = FilterBeautyMapper.BeautyMatchGroup(criteria.MatchGroup);
        sheet!.Cells[$"B{rowIndex}:M{rowIndex++}"].Value = FilterBeautyMapper.BeautyVrSetting(criteria.VrSettings);
        sheet!.Cells[$"B{rowIndex}:M{rowIndex}"].Value = ModelsHelper.MakeLeagueGroupSelectString(criteria.LeagueGroupIds, leagueGroups);
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
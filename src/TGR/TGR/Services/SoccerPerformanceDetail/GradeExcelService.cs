using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using TGR.Constants;
using TGR.Criteria.SoccerPerformanceDetail;
using TGR.Utilities;
using TGR.ViewModel.SoccerPerformanceDetail;
using Utilities.Configurations;

namespace TGR.Services.SoccerPerformanceDetail;

public interface GradeExcelService
{
    Task<ExcelPackage> ExportExcelGradePerformance(GradePerformanceList data,
        GetGradePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes,
        bool allowVirtualPerformance,
        bool allowRealPerformance);
}

public class GradeExcelServiceImp(
    ConfigurationService configurationService,
    IMapper mapper)
    : BaseExcelService(configurationService), GradeExcelService
{

    public Task<ExcelPackage> ExportExcelGradePerformance(
        GradePerformanceList data,
        GetGradePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes,
        bool allowVirtualPerformance,
        bool allowRealPerformance)
    {
        LoadTemplate("Grade", "Grade.xlsx", ExcelConstants.GradePerformance);

        var rowIndex = BuildFilterBlock(criteria, leagueGroups, betTypes);
        var currentRowIndex = rowIndex + 4;

        currentRowIndex = ProcessDataList(data.ItemLiveList, currentRowIndex, ExcelConstants.LiveText, BuildBodyRows);
        currentRowIndex = ProcessDataList(data.ItemNonLiveList, currentRowIndex, ExcelConstants.NonLiveText, BuildBodyRows);

        BuildTotalRow(data.Total, currentRowIndex);
        HideNonPermissionColumn(allowVirtualPerformance, allowRealPerformance);
        ClearTemplate(ExcelConstants.GradePerformance);
        return Task.FromResult(ExcelPackage!);
    }

    private void BuildBodyRows(IEnumerable<GradePerformanceItem> list, int startRowIndex, string title)
    {
        var index = 1;
        var rowIndex = startRowIndex;

        var titleExcelItem = new GradePerformanceExceItem
        {
            Title = title
        };
        sheet!.ApplyModel(titleSheet, titleExcelItem, rowIndex++);

        var excelModels = mapper.Map<IEnumerable<GradePerformanceExcelItem>>(list);
        foreach (var item in excelModels)
        {
            item.No = index++;
            sheet!.ApplyModel(bodySheet, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    private void BuildTotalRow(GradePerformanceItem data, int startIndex)
    {
        var excelModel = mapper.Map<GradePerformanceExcelItem>(data);
        sheet!.ApplyModel(totalSheet, excelModel, startIndex);
        UpdateValueSpecificColumns(startIndex, excelModel);
        var cellTotalAddress = $"A{startIndex}:B{startIndex}";
        sheet!.Cells[cellTotalAddress].Merge = true;
        sheet!.Cells[cellTotalAddress].Value = ExcelConstants.GrandTotalText;
    }

    private void UpdateValueSpecificColumns(int startIndex, GradePerformanceExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"G{startIndex}"], model.Score);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"K{startIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"O{startIndex}"], model.MarginReal);
        sheet!.Cells[$"I{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.Turnover / model.NoOfMatches : ExcelConstants.NotAvailableText;
        sheet!.Cells[$"M{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.TurnoverReal / model.NoOfMatches : ExcelConstants.NotAvailableText;
    }
}
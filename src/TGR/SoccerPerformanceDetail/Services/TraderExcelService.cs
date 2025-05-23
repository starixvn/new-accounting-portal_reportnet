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
        IEnumerable<BetTypeItem> betTypes,
        bool allowVirtualPerformance,
        bool allowRealPerformance);
}

public class TraderExcelServiceImp(
    ConfigurationService configurationService,
    IMapper mapper)
    : BaseExcelService(configurationService), TraderExcelService
{

    public Task<ExcelPackage> ExportExcelTraderPerformance(
        TraderPerformanceList data,
        GetTraderPerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes,
        bool allowVirtualPerformance,
        bool allowRealPerformance)
    {
        LoadTemplate("Trader", "Trader.xlsx", ExcelConstants.TraderPerformance);
        
        var rowIndex = BuildFilterBlock(criteria, leagueGroups, betTypes);
        var currentRowIndex = rowIndex + 4;
        
        currentRowIndex = ProcessDataList(data.ItemLiveList, currentRowIndex, ExcelConstants.LiveText, BuildBodyRows);
        currentRowIndex = ProcessDataList(data.ItemNonLiveList, currentRowIndex, ExcelConstants.NonLiveText, BuildBodyRows);

        BuildTotalRow(data.Total, currentRowIndex);
        HideNonPermissionColumn(allowVirtualPerformance, allowRealPerformance);
        ClearTemplate(ExcelConstants.TraderPerformance);
        return Task.FromResult(ExcelPackage!);
    }

    private void BuildBodyRows(IEnumerable<TraderPerformanceItem> list, int startRowIndex, string title)
    {
        var index = 1;
        var rowIndex = startRowIndex;
        
        var titleExcelItem = new TraderPerformanceExcelTitleItem
        {
            Title = title
        };
        sheet!.ApplyModel(titleSheet, titleExcelItem, rowIndex++);
        
        var excelModels = mapper.Map<IEnumerable<TraderPerformanceExcelItem>>(list);
        foreach (var item in excelModels)
        {
            item.No = index++;
            sheet!.ApplyModel(bodySheet, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
        }
    }

    private void BuildTotalRow(TraderPerformanceItem data, int startIndex)
    {
        var excelModel = mapper.Map<TraderPerformanceExcelItem>(data);
        sheet!.ApplyModel(totalSheet, excelModel, startIndex);
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
}
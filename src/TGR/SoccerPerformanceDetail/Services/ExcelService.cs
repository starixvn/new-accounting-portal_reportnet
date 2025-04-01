namespace SoccerPerformanceDetail.Services;

using AutoMapper;
using Constants;
using Criteria;
using Epplus.Mapper.Extensions;
using global::Utilities.Configurations;
using OfficeOpenXml;
using Utilities;
using ViewModel;

public interface ExcelService
{
    Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypePerformanceList data,
        GetBetTypePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes);
}

public class ExcelServiceImp(ConfigurationService configurationService, IMapper mapper)
    : BaseExcelService(configurationService), ExcelService
{
    public Task<ExcelPackage> ExportExcelBetTypePerformance(BetTypePerformanceList data,
        GetBetTypePerformanceCriteria criteria,
        IEnumerable<LeagueGroup> leagueGroups,
        IEnumerable<BetTypeItem> betTypes)
    {
        LoadTemplate("BetType", "BetType.xlsx", ExcelConstants.BetTypePerformance);
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
        ClearTemplate(ExcelConstants.BetTypePerformance);
        return Task.FromResult(ExcelPackage!);
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
            sheet.ApplyModel(template, item, rowIndex);
            UpdateValueSpecificColumns(rowIndex++, item);
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
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"K{startIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"O{startIndex}"], model.MarginReal);
        sheet!.Cells[$"I{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.Turnover / model.NoOfMatches : ExcelConstants.NotAvailableText;
        sheet!.Cells[$"M{startIndex}"].Value =
            model.NoOfMatches > 0 ? model.TurnoverReal / model.NoOfMatches : ExcelConstants.NotAvailableText;
    }
}
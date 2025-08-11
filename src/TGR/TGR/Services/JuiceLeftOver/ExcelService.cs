using AutoMapper;
using OfficeOpenXml;
using TGR.Constants;
using TGR.Criteria.JuiceLeftOver;
using TGR.Model.JuiceLeftOver;
using TGR.Utilities;
using TGR.ViewModel.JuiceLeftOver;
using Utilities.Configurations;
using Utilities.LinqDynamicSum;

namespace TGR.Services.JuiceLeftOver;

public interface ExcelService
{
    Task<ExcelPackage> ExportExcelBetTypePerformance(IEnumerable<BetTypePerformanceInfo> data,
        GetBetTypePerformanceCriteria criteria);

    Task<ExcelPackage> ExportExcelLeaguePerformance(IEnumerable<LeaguePerformanceInfo> data,
        GetLeaguePerformanceCriteria criteria);
}

public class ExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : BaseExcelService(configurationService), ExcelService
{
    private const byte startBodyRowIndex = 5;
    public Task<ExcelPackage> ExportExcelBetTypePerformance(IEnumerable<BetTypePerformanceInfo> data, GetBetTypePerformanceCriteria criteria)
    {
        LoadTemplate("BetType", "BetType.xlsx", ExcelConstants.BetTypePerformance);
        BuildTitle(criteria.FromDate, criteria.ToDate, sheet.Cells["A2:Q2"]);
        BuildBodyRows<BetTypePerformanceInfo, BetTypePerformanceInfoExcelItem>(data, startBodyRowIndex, mapper, UpdateValueSpecificColumnsForBetType);
        var totalData = data.DynamicSum();
        BuildTotalRow<BetTypePerformanceInfo, BetTypePerformanceInfoExcelItem>(totalData, startBodyRowIndex + data.Count(), mapper, UpdateValueSpecificColumnsForBetType);
        ClearTemplate(ExcelConstants.BetTypePerformance);
        return Task.FromResult(ExcelPackage!);
    }

    public Task<ExcelPackage> ExportExcelLeaguePerformance(IEnumerable<LeaguePerformanceInfo> data, GetLeaguePerformanceCriteria criteria)
    {
        LoadTemplate("League", "League.xlsx", ExcelConstants.LeaguePerformance);
        BuildTitle(criteria.FromDate, criteria.ToDate, sheet.Cells["A2:P2"]);
        BuildBodyRows<LeaguePerformanceInfo, LeaguePerformanceInfoExcelItem>(data, startBodyRowIndex, mapper, UpdateValueSpecificColumnsForLeague);
        var totalData = data.DynamicSum();
        BuildTotalRow<LeaguePerformanceInfo, LeaguePerformanceInfoExcelItem>(totalData, startBodyRowIndex + data.Count(), mapper, UpdateValueSpecificColumnsForLeague);
        ClearTemplate(ExcelConstants.LeaguePerformance);
        return Task.FromResult(ExcelPackage!);
    }

    private void UpdateValueSpecificColumnsForBetType(int rowIndex, BetTypePerformanceInfoExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"H{rowIndex}"], model.JuiceMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"I{rowIndex}"], model.JuiceMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"L{rowIndex}"], model.LeftoverMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"M{rowIndex}"], model.LeftoverMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"P{rowIndex}"], model.Margin);
    }

    private void UpdateValueSpecificColumnsForLeague(int rowIndex, LeaguePerformanceInfoExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"G{rowIndex}"], model.JuiceMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"H{rowIndex}"], model.JuiceMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"K{rowIndex}"], model.LeftoverMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"L{rowIndex}"], model.LeftoverMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"O{rowIndex}"], model.Margin);
    }
}
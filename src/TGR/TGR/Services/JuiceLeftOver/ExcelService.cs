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

    Task<ExcelPackage> ExportExcelTeamPerformance(IEnumerable<TeamPerformanceInfo> data,
        GetTeamPerformanceCriteria criteria, IEnumerable<TeamInfo> teams);

    Task<ExcelPackage> ExportExcelGradePerformance(IEnumerable<GradePerformanceInfo> data,
        GetGradePerformanceCriteria criteria);

    Task<ExcelPackage> ExportExcelTraderPerformance(IEnumerable<TraderPerformanceInfo> data,
        GetTraderPerformanceCriteria criteria);
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
        var totalRowIndex = startBodyRowIndex + data.Count();
        var mergedTotalCellAddress = $"A{totalRowIndex}:B{totalRowIndex}";
        BuildTotalRow<BetTypePerformanceInfo, BetTypePerformanceInfoExcelItem>(totalData, totalRowIndex, mapper, UpdateValueSpecificColumnsForBetType, mergedTotalCellAddress);
        ClearTemplate(ExcelConstants.BetTypePerformance);
        return Task.FromResult(ExcelPackage!);
    }

    public Task<ExcelPackage> ExportExcelLeaguePerformance(IEnumerable<LeaguePerformanceInfo> data, GetLeaguePerformanceCriteria criteria)
    {
        LoadTemplate("League", "League.xlsx", ExcelConstants.LeaguePerformance);
        BuildTitle(criteria.FromDate, criteria.ToDate, sheet.Cells["A2:P2"]);
        BuildBodyRows<LeaguePerformanceInfo, LeaguePerformanceInfoExcelItem>(data, startBodyRowIndex, mapper, UpdateValueSpecificColumnsForLeague);
        var totalData = data.DynamicSum();
        var totalRowIndex = startBodyRowIndex + data.Count();
        var mergedTotalCellAddress = $"A{totalRowIndex}:B{totalRowIndex}";
        BuildTotalRow<LeaguePerformanceInfo, LeaguePerformanceInfoExcelItem>(totalData, totalRowIndex, mapper, UpdateValueSpecificColumnsForLeague, mergedTotalCellAddress);
        ClearTemplate(ExcelConstants.LeaguePerformance);
        return Task.FromResult(ExcelPackage!);
    }

    public Task<ExcelPackage> ExportExcelTeamPerformance(IEnumerable<TeamPerformanceInfo> data, GetTeamPerformanceCriteria criteria, IEnumerable<TeamInfo> teams)
    {
        LoadTemplate("Team", "Team.xlsx", ExcelConstants.TeamPerformance);
        var teamsString = ModelsHelper.MakeTeamSelectString(teams, criteria.TeamIds);
        BuildTitle(criteria.FromDate, criteria.ToDate, teamsString, sheet.Cells["A2:R2"]);
        BuildBodyRows<TeamPerformanceInfo, TeamPerformanceInfoExcelItem>(data, startBodyRowIndex, mapper, UpdateValueSpecificColumnsForTeam);
        var totalData = data.DynamicSum();
        var totalRowIndex = startBodyRowIndex + data.Count();
        var mergedTotalCellAddress = $"A{totalRowIndex}:B{totalRowIndex}";
        BuildTotalRow<TeamPerformanceInfo, TeamPerformanceInfoExcelItem>(totalData, totalRowIndex, mapper, UpdateValueSpecificColumnsForTeam, mergedTotalCellAddress);
        sheet!.Cells[$"R{totalRowIndex}"].Value = string.Empty;
        ClearTemplate(ExcelConstants.TeamPerformance);
        return Task.FromResult(ExcelPackage!);
    }

    public Task<ExcelPackage> ExportExcelGradePerformance(IEnumerable<GradePerformanceInfo> data, GetGradePerformanceCriteria criteria)
    {
        LoadTemplate("Grade", "Grade.xlsx", ExcelConstants.GradePerformance);
        BuildTitle(criteria.FromDate, criteria.ToDate, sheet.Cells["A2:R2"]);
        BuildBodyRows<GradePerformanceInfo, GradePerformanceInfoExcelItem>(data, startBodyRowIndex, mapper, UpdateValueSpecificColumnsForGrade);
        var totalData = data.DynamicSum();
        var totalRowIndex = startBodyRowIndex + data.Count();
        var mergedTotalCellAddress = $"A{totalRowIndex}:B{totalRowIndex}";
        BuildTotalRow<GradePerformanceInfo, GradePerformanceInfoExcelItem>(totalData, totalRowIndex, mapper, UpdateValueSpecificColumnsForGrade, mergedTotalCellAddress);
        sheet!.Cells[$"R{totalRowIndex}"].Value = string.Empty;
        ClearTemplate(ExcelConstants.TeamPerformance);
        return Task.FromResult(ExcelPackage!);
    }

    public Task<ExcelPackage> ExportExcelTraderPerformance(IEnumerable<TraderPerformanceInfo> data, GetTraderPerformanceCriteria criteria)
    {
        LoadTemplate("Trader", "Trader.xlsx", ExcelConstants.TraderPerformance);
        BuildTitle(criteria.FromDate, criteria.ToDate, sheet.Cells["A2:S2"]);
        BuildBodyRows<TraderPerformanceInfo, TraderPerformanceInfoExcelItem>(data, startBodyRowIndex, mapper, UpdateValueSpecificColumnsForTrader);
        var totalData = data.DynamicSum();
        var totalRowIndex = startBodyRowIndex + data.Count();
        var mergedTotalCellAddress = $"A{totalRowIndex}:D{totalRowIndex}";
        BuildTotalRow<TraderPerformanceInfo, TraderPerformanceInfoExcelItem>(totalData, totalRowIndex, mapper, UpdateValueSpecificColumnsForTrader, mergedTotalCellAddress);
        sheet!.Cells[$"S{totalRowIndex}"].Value = string.Empty;
        ClearTemplate(ExcelConstants.TeamPerformance);
        return Task.FromResult(ExcelPackage!);
    }

    private void UpdateValueSpecificColumnsForBetType(int rowIndex, BetTypePerformanceInfoExcelItem model)
    {
        UpdateCommonColumns(rowIndex, model, "H", "I", "L", "M", "P");
    }

    private void UpdateValueSpecificColumnsForLeague(int rowIndex, LeaguePerformanceInfoExcelItem model)
    {
        UpdateCommonColumns(rowIndex, model, "G", "H", "K", "L", "O");
    }

    private void UpdateValueSpecificColumnsForTeam(int rowIndex, TeamPerformanceInfoExcelItem model)
    {
        UpdateCommonColumns(rowIndex, model, "H", "I", "L", "M", "P");
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"R{rowIndex}"], model.Score);
    }

    private void UpdateValueSpecificColumnsForGrade(int rowIndex, GradePerformanceInfoExcelItem model)
    {
        UpdateCommonColumns(rowIndex, model, "H", "I", "L", "M", "P");
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"R{rowIndex}"], model.Score);
    }

    private void UpdateValueSpecificColumnsForTrader(int rowIndex, TraderPerformanceInfoExcelItem model)
    {
        UpdateCommonColumns(rowIndex, model, "I", "J", "M", "N", "Q");
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"S{rowIndex}"], model.Score);
    }

    private void UpdateCommonColumns<T>(int rowIndex, T model, string juiceMarginCol, string juiceMarginTotalCol, 
        string leftoverMarginCol, string leftoverMarginTotalCol, string marginCol) where T : class
    {
        var juiceMargin = GetPropertyValue<decimal?>(model, "JuiceMargin");
        var juiceMarginTotal = GetPropertyValue<decimal?>(model, "JuiceMarginTotal");
        var leftoverMargin = GetPropertyValue<decimal?>(model, "LeftoverMargin");
        var leftoverMarginTotal = GetPropertyValue<decimal?>(model, "LeftoverMarginTotal");
        var margin = GetPropertyValue<decimal?>(model, "Margin");

        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"{juiceMarginCol}{rowIndex}"], juiceMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"{juiceMarginTotalCol}{rowIndex}"], juiceMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"{leftoverMarginCol}{rowIndex}"], leftoverMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"{leftoverMarginTotalCol}{rowIndex}"], leftoverMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"{marginCol}{rowIndex}"], margin);
    }

    private T GetPropertyValue<T>(object model, string propertyName)
    {
        var property = model.GetType().GetProperty(propertyName);
        var value = property?.GetValue(model);
        return value is T typedValue ? typedValue : default;
    }
}
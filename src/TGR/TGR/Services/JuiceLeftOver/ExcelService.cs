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

    private void UpdateValueSpecificColumnsForTeam(int rowIndex, TeamPerformanceInfoExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"H{rowIndex}"], model.JuiceMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"I{rowIndex}"], model.JuiceMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"L{rowIndex}"], model.LeftoverMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"M{rowIndex}"], model.LeftoverMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"P{rowIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"R{rowIndex}"], model.Score);
    }

    private void UpdateValueSpecificColumnsForGrade(int rowIndex, GradePerformanceInfoExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"H{rowIndex}"], model.JuiceMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"I{rowIndex}"], model.JuiceMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"L{rowIndex}"], model.LeftoverMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"M{rowIndex}"], model.LeftoverMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"P{rowIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"R{rowIndex}"], model.Score);
    }

    private void UpdateValueSpecificColumnsForTrader(int rowIndex, TraderPerformanceInfoExcelItem model)
    {
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"I{rowIndex}"], model.JuiceMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"J{rowIndex}"], model.JuiceMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"M{rowIndex}"], model.LeftoverMargin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"N{rowIndex}"], model.LeftoverMarginTotal);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"Q{rowIndex}"], model.Margin);
        ModelsHelper.FormatCellWithNumber(sheet!.Cells[$"S{rowIndex}"], model.Score);
    }
}
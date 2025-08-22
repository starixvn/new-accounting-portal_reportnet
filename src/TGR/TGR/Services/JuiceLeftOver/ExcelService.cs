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

    public Task<ExcelPackage> ExportExcelTeamPerformance(IEnumerable<TeamPerformanceInfo> data, GetTeamPerformanceCriteria criteria, IEnumerable<TeamInfo> teams)
    {
        LoadTemplate("Team", "Team.xlsx", ExcelConstants.TeamPerformance);
        var teamsString = ModelsHelper.MakeTeamSelectString(teams, criteria.TeamIds);
        BuildTitle(criteria.FromDate, criteria.ToDate, teamsString, sheet.Cells["A2:R2"]);
        BuildBodyRows<TeamPerformanceInfo, TeamPerformanceInfoExcelItem>(data, startBodyRowIndex, mapper, UpdateValueSpecificColumnsForTeam);
        var totalData = data.DynamicSum();
        var totalRowIndex = startBodyRowIndex + data.Count();
        BuildTotalRow<TeamPerformanceInfo, TeamPerformanceInfoExcelItem>(totalData, totalRowIndex, mapper, UpdateValueSpecificColumnsForTeam);
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
        BuildTotalRow<GradePerformanceInfo, GradePerformanceInfoExcelItem>(totalData, totalRowIndex, mapper, UpdateValueSpecificColumnsForGrade);
        sheet!.Cells[$"R{totalRowIndex}"].Value = string.Empty;
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
}
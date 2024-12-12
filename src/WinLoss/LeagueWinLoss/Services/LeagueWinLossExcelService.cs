using AutoMapper;
using Epplus.Mapper.Extensions;
using LeagueWinLoss.Model;
using OfficeOpenXml;
using Utilities.Configurations;

namespace LeagueWinLoss.Services;

public interface LeagueWinLossExcelService
{
    Task<ExcelPackage> ExportExcelLeagueWinLoss(LeagueWinLossModel data, string title);
}

public class LeagueWinLossExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : LeagueWinLossExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private ExcelWorksheet subItemSheet;

    private ExcelWorksheet grandTotalSheet;

    private const int StartRowIndex = 6;

    private const string ParlaySportName = "Parlay";

    private const string SbAdjustmentSportName = "SB Adjustment";

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcelLeagueWinLoss(LeagueWinLossModel data, string title)
    {
        LoadTemplate("LeagueWinLoss.xlsx");
        BuildTitle(title, "A1:M2");
        BuildBodyRows(data, StartRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/LeagueWinLoss/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
        subItemSheet = ExcelPackage.Workbook.Worksheets["SubItemTemplate"];
        grandTotalSheet = ExcelPackage.Workbook.Worksheets["GrandTotalTemplate"];
        sheet = ExcelPackage.Workbook.Worksheets["League Win Loss"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(LeagueWinLossModel data, int startRowIndex)
    {
        var rowIndex = startRowIndex;
        foreach (var sport in data.SportWinLoss)
        {
            var sportExcelModel = mapper.Map<LeagueWinLossItemExcelModel>(sport.Total);
            sportExcelModel.LeagueName = sport.SportName;
            sheet.ApplyModel(subItemSheet, sportExcelModel, rowIndex++);
            if (sport.SportName is ParlaySportName or SbAdjustmentSportName) continue;
            var leagueIndex = 1;
            foreach (var league in sport.Leagues)
            {
                var template = leagueIndex % 2 == 0 ? evenSheet : oddSheet;
                var leagueExcelModel = mapper.Map<LeagueWinLossItemExcelModel>(league);
                sheet.ApplyModel(template, leagueExcelModel, rowIndex++);
                leagueIndex++;
            }
        }
        BuildTotalRows(data, rowIndex);
    }

    private void BuildTotalRows(LeagueWinLossModel data, int startRowIndex)
    {
        var totalExcelModel = mapper.Map<LeagueWinLossItemExcelModel>(data.Total);
        totalExcelModel.MatchCount = null;
        sheet.ApplyModel(totalSheet, totalExcelModel, startRowIndex++);
        if (IsShowTotal(data.SoccerRM))
        {
            RenderTotalRow(data.SoccerRM, ref startRowIndex);
        }
        if (IsShowTotal(data.SabaSoccerRM))
        {
            RenderTotalRow(data.SabaSoccerRM, ref startRowIndex);
        }
        if (IsShowTotal(data.SabaPinGoalRM))
        {
            RenderTotalRow(data.SabaPinGoalRM, ref startRowIndex);
        }
        if (IsShowTotal(data.OtherSportRM))
        {
            RenderTotalRow(data.OtherSportRM, ref startRowIndex);
        }
        if (IsShowTotal(data.SoccerBARM))
        {
            RenderTotalRow(data.SoccerBARM, ref startRowIndex);
        }
        if (IsShowTotal(data.SabaSoccerBARM))
        {
            RenderTotalRow(data.SabaSoccerBARM, ref startRowIndex);
        }
        if (IsShowTotal(data.SabaPinGoalBARM))
        {
            RenderTotalRow(data.SabaPinGoalBARM, ref startRowIndex);
        }
        if (IsShowTotal(data.OtherSportBARM))
        {
            RenderTotalRow(data.OtherSportBARM, ref startRowIndex);
        }

        if (!IsShowTotal(data.SoccerRM)
            && !IsShowTotal(data.SabaSoccerRM)
            && !IsShowTotal(data.SabaPinGoalRM)
            && !IsShowTotal(data.OtherSportRM)
            && !IsShowTotal(data.SoccerBARM)
            && !IsShowTotal(data.SabaSoccerBARM)
            && !IsShowTotal(data.SabaPinGoalBARM)
            && !IsShowTotal(data.OtherSportBARM)) return;
        RenderTotalRow(data.GrandTotal, ref startRowIndex);
    }

    private void RenderTotalRow(LeagueWinLossItem data,ref int rowIndex)
    {
        var totalExcelModel = mapper.Map<LeagueWinLossItemExcelModel>(data);
        sheet.ApplyModel(grandTotalSheet, totalExcelModel, rowIndex++);
        sheet.Cells[$"A{rowIndex - 1}:G{rowIndex - 1}"].Merge = true;
        sheet.Cells[$"A{rowIndex - 1}:G{rowIndex - 1}"].Value = totalExcelModel.LeagueName;
        sheet.Cells[$"L{rowIndex - 1}:M{rowIndex - 1}"].Merge = true;
        sheet.Cells[$"L{rowIndex - 1}:M{rowIndex - 1}"].Value = null;
    }

    private static bool IsShowTotal(LeagueWinLossItem winloss)
    {
        return winloss.VirtualWinLoss != 0 || winloss.RealIncome != 0 || winloss.VirtualTurnover != 0 || winloss.RealTurnover != 0;
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("SubItemTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("GrandTotalTemplate");
    }
}
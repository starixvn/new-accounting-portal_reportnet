using System.Drawing;
using AutoMapper;
using Epplus.Mapper.Extensions;
using LeagueWinLoss.Model;
using OfficeOpenXml;
using Utilities.Configurations;

namespace LeagueWinLoss.Services;

public interface MatchWinLossExcelService
{
    Task<ExcelPackage> ExportExcel(MatchWinLossModel data, string title, string header);

    Task<ExcelPackage> ExportExcelFromBetTypeStatus(MatchWinLossFromBetTypeStatusModel data, string title, string header);
    Task<ExcelPackage> ExportExcelWithChoices(MatchWinLossWithChoiceModel data, string title, string header);
}

public class MatchWinLossExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : MatchWinLossExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private ExcelWorksheet subItemSheet;

    private ExcelWorksheet grandTotalSheet;

    private const int StartRowIndex = 7;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(MatchWinLossModel data, string title, string header)
    {
        LoadTemplate("LeagueWinLoss_Level.xlsx");
        BuildTitle(title, "A1:L2");
        BuildBodyRows(data, StartRowIndex);
        sheet.Cells["A4:L4"].Value = header;
        sheet.Cells["A5:A6"].Value = "Event";
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    public Task<ExcelPackage> ExportExcelFromBetTypeStatus(MatchWinLossFromBetTypeStatusModel data, string title, string header)
    {
        LoadTemplate("LeagueWinLoss_Level.xlsx");
        BuildTitle(title, "A1:L2");
        BuildBodyRowsForBetTypeFromStatus(data, StartRowIndex);
        sheet.Cells["A4:L4"].Value = header;
        sheet.Cells["A5:A6"].Value = "Event";
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    public Task<ExcelPackage> ExportExcelWithChoices(MatchWinLossWithChoiceModel data, string title, string header)
    {
        LoadTemplate("LeagueWinLoss_Level.xlsx");
        BuildTitle(title, "A1:L2");
        BuildBodyRowsWithChoices(data, StartRowIndex);
        sheet.Cells["A4:L4"].Value = header;
        sheet.Cells["A5:A6"].Value = "Event";
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

    private void BuildBodyRows(MatchWinLossModel data, int startRowIndex)
    {
        var rowIndex = startRowIndex;
        foreach (var match in data.Match)
        {
            var matchExcelModel = mapper.Map<MatchWinLossItemExcelModel>(match.Total);
            sheet.ApplyModel(subItemSheet, matchExcelModel, rowIndex++);
            var matchIndex = 1;
            foreach (var item in match.Matches)
            {
                var template = matchIndex % 2 == 0 ? evenSheet : oddSheet;
                var itemExcelModel = mapper.Map<MatchWinLossItemExcelModel>(item);
                itemExcelModel.MatchEvent = item.LiveIndicator ? "Live" : "Non-Live";
                sheet.ApplyModel(template, itemExcelModel, rowIndex++);
                matchIndex++;
            }
        }
        BuildTotalRows(data, rowIndex);
    }

    private void BuildBodyRowsForBetTypeFromStatus(MatchWinLossFromBetTypeStatusModel data, int startRowIndex)
    {
        foreach (var match in data.Match)
        {
            var template = startRowIndex % 2 == 0 ? evenSheet : oddSheet;
            var itemExcelModel = mapper.Map<MatchWinLossItemExcelModel>(match);
            sheet.ApplyModel(template, itemExcelModel, startRowIndex++);
        }
        var totalExcelModel = mapper.Map<MatchWinLossItemExcelModel>(data.Total);
        sheet.ApplyModel(totalSheet, totalExcelModel, startRowIndex++);
        if (IsShowTotal(data.RM))
        {
            RenderTotalRow(data.RM, ref startRowIndex);
        }
        if (IsShowTotal(data.BARM))
        {
            RenderTotalRow(data.BARM, ref startRowIndex);
        }

        if (!IsShowTotal(data.RM)
            && !IsShowTotal(data.BARM)) return;
        RenderTotalRow(data.GrandTotal, ref startRowIndex);
    }

    private void BuildBodyRowsWithChoices(MatchWinLossWithChoiceModel data, int startRowIndex)
    {
        foreach (var match in data.Match)
        {
            var matchExcelModel = mapper.Map<MatchWinLossItemExcelModel>(match.Total);
            sheet.ApplyModel(subItemSheet, matchExcelModel, startRowIndex++);
            foreach (var liveIndicator in match.LiveIndicators)
            {
                var liveIndicatorExcelModel = mapper.Map<MatchWinLossItemExcelModel>(liveIndicator.Total);
                sheet.ApplyModel(subItemSheet, liveIndicatorExcelModel, startRowIndex++);
                sheet.Cells[$"A{startRowIndex - 1}:L{startRowIndex - 1}"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(212, 237, 251));
                foreach (var betType in liveIndicator.BetTypes)
                {
                    var betTypeExcelModel = mapper.Map<MatchWinLossItemExcelModel>(betType.Total);
                    sheet.ApplyModel(subItemSheet, betTypeExcelModel, startRowIndex++);
                    sheet.Cells[$"A{startRowIndex - 1}:L{startRowIndex - 1}"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(235, 253, 253));
                    foreach (var choice in betType.Choices)
                    {
                        var choiceExcelModel = mapper.Map<MatchWinLossItemExcelModel>(choice);
                        choiceExcelModel.MatchEvent = choice.Choice;
                        sheet.ApplyModel(evenSheet, choiceExcelModel, startRowIndex++);
                    }
                }
            }
        }
        var totalExcelModel = mapper.Map<MatchWinLossItemExcelModel>(data.Total);
        sheet.ApplyModel(totalSheet, totalExcelModel, startRowIndex++);
        if (IsShowTotal(data.RM))
        {
            RenderTotalRow(data.RM, ref startRowIndex);
        }
        if (IsShowTotal(data.BARM))
        {
            RenderTotalRow(data.BARM, ref startRowIndex);
        }

        if (!IsShowTotal(data.RM)
            && !IsShowTotal(data.BARM)) return;
        RenderTotalRow(data.GrandTotal, ref startRowIndex);
    }

    private void BuildTotalRows(MatchWinLossModel data, int startRowIndex)
    {
        var totalExcelModel = mapper.Map<MatchWinLossItemExcelModel>(data.Total);
        sheet.ApplyModel(totalSheet, totalExcelModel, startRowIndex++);
        if (IsShowTotal(data.RM))
        {
            RenderTotalRow(data.RM, ref startRowIndex);
        }
        if (IsShowTotal(data.BARM))
        {
            RenderTotalRow(data.BARM, ref startRowIndex);
        }

        if (!IsShowTotal(data.RM)
            && !IsShowTotal(data.BARM)) return;
        RenderTotalRow(data.GrandTotal, ref startRowIndex);
    }

    private void RenderTotalRow(MatchWinLossByLiveStatus data, ref int rowIndex)
    {
        var totalExcelModel = mapper.Map<MatchWinLossItemExcelModel>(data);
        sheet.ApplyModel(grandTotalSheet, totalExcelModel, rowIndex++);
        sheet.Cells[$"A{rowIndex - 1}:F{rowIndex - 1}"].Merge = true;
        sheet.Cells[$"A{rowIndex - 1}:F{rowIndex - 1}"].Value = totalExcelModel.MatchEvent;
        sheet.Cells[$"K{rowIndex - 1}:L{rowIndex - 1}"].Merge = true;
        sheet.Cells[$"K{rowIndex - 1}:L{rowIndex - 1}"].Value = null;
    }

    private static bool IsShowTotal(MatchWinLossByLiveStatus winloss)
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
using AutoMapper;
using Epplus.Mapper.Extensions;
using Fanex.Excel.Repositories.CustomColors;
using LeagueWinLoss.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Utilities.Configurations;

namespace LeagueWinLoss.Services;

public interface BetTypeWinLossExcelService
{
    Task<ExcelPackage> ExportExcel(BettypeWinLossModel data, string title, string header);

    Task<ExcelPackage> ExportExcelBetTypeByStatus(BetTypeWinLossByStatusModel data, string title, string header);
}

public class BetTypeWinLossExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : BetTypeWinLossExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private ExcelWorksheet subItemSheet;

    private ExcelWorksheet grandTotalSheet;

    private const int StartRowIndex = 7;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(BettypeWinLossModel data, string title, string header)
    {
        LoadTemplate("LeagueWinLoss_Level.xlsx");
        BuildTitle(title, "A1:L2");
        BuildBodyRows(data, StartRowIndex);
        sheet.Cells["A4:L4"].Value = header;
        sheet.Cells["A5:A6"].Value = "Bet Type";
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    public Task<ExcelPackage> ExportExcelBetTypeByStatus(BetTypeWinLossByStatusModel data, string title, string header)
    {
        var rowIndex = StartRowIndex;
        LoadTemplate("LeagueWinLoss_Level.xlsx");
        BuildTitle(title, "A1:L2");
        sheet.Cells["A4:L4"].Value = $"{header} Live";
        sheet.Cells["A5:A6"].Value = "Bet Type";
        ExportForBetTypeLive(data.BetTypeWinLossLive, ref rowIndex);
        sheet.Cells["A4:L6"].Copy(sheet.Cells[$"A{rowIndex + 1}:L{rowIndex + 3}"]);
        sheet.Cells[$"A{rowIndex + 1}:L{rowIndex + 1}"].Value = $"{header} Non-Live";
        sheet.Cells[$"A{rowIndex + 2}:L{rowIndex + 3}"].Value = "Bet Type";
        ExportForBetTypeNonLive(data.BetTypeWinLossNonLive, rowIndex + 4);
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void ExportForBetTypeLive(BetTypeWinLossLiveModel data,ref int rowIndex)
    {
        if (data.BetTypeWinLossLiveGroups.Count > 0)
        {
            BetTypeWinLossItemExcelModel totalExcelModel;
            foreach (var group in data.BetTypeWinLossLiveGroups)
            {
                foreach (var betType in group.BettypeWinLoss)
                {
                    var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
                    var itemExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(betType);
                    sheet.ApplyModel(template, itemExcelModel, rowIndex++);
                }
                totalExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(group.Total);
                sheet.ApplyModel(totalSheet, totalExcelModel, rowIndex++);
            }
            totalExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(data.Total);
            sheet.ApplyModel(totalSheet, totalExcelModel, rowIndex++);
            if (IsShowTotal(data.RM))
            {
                RenderTotalRow(data.RM, ref rowIndex);
            }
            if (IsShowTotal(data.BARM))
            {
                RenderTotalRow(data.BARM, ref rowIndex);
            }

            if (!IsShowTotal(data.RM)
                && !IsShowTotal(data.BARM)) return;
            RenderTotalRow(data.GrandTotal, ref rowIndex);
        }
        else
        {
            sheet.Cells["A7:L7"].Merge = true;
            sheet.Cells["A7:L7"].Value = "There is no data";
            sheet.Cells["A7:L7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
        
    }

    private void ExportForBetTypeNonLive(BetTypeWinLossNonLiveModel data, int rowIndex)
    {
        foreach (var betType in data.BettypeWinLoss)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            var itemExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(betType);
            sheet.ApplyModel(template, itemExcelModel, rowIndex++);
        }
        var totalExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(data.Total);
        sheet.ApplyModel(totalSheet, totalExcelModel, rowIndex++);
        if (IsShowTotal(data.RM))
        {
            RenderTotalRow(data.RM, ref rowIndex);
        }
        if (IsShowTotal(data.BARM))
        {
            RenderTotalRow(data.BARM, ref rowIndex);
        }

        if (!IsShowTotal(data.RM)
            && !IsShowTotal(data.BARM)) return;
        RenderTotalRow(data.GrandTotal, ref rowIndex);
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

    private void BuildBodyRows(BettypeWinLossModel data, int startRowIndex)
    {
        var rowIndex = startRowIndex;
        foreach (var betType in data.BettypeWinLoss)
        {
            var betTypeExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(betType.Total);
            sheet.ApplyModel(subItemSheet, betTypeExcelModel, rowIndex++);
            var betTypeIndex = 1;
            foreach (var item in betType.BetTypes)
            {
                var template = betTypeIndex % 2 == 0 ? evenSheet : oddSheet;
                var itemExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(item);
                itemExcelModel.BettypeName = item.SiteType;
                sheet.ApplyModel(template, itemExcelModel, rowIndex++);
                betTypeIndex++;
            }
        }
        BuildTotalRows(data, rowIndex);
    }

    private void BuildTotalRows(BettypeWinLossModel data, int startRowIndex)
    {
        var totalExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(data.Total);
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

    private void RenderTotalRow(BettypeWinLossBySite data, ref int rowIndex)
    {
        var totalExcelModel = mapper.Map<BetTypeWinLossItemExcelModel>(data);
        sheet.ApplyModel(grandTotalSheet, totalExcelModel, rowIndex++);
        sheet.Cells[$"A{rowIndex - 1}:F{rowIndex - 1}"].Merge = true;
        sheet.Cells[$"A{rowIndex - 1}:F{rowIndex - 1}"].Value = totalExcelModel.BettypeName;
        sheet.Cells[$"K{rowIndex - 1}:L{rowIndex - 1}"].Merge = true;
        sheet.Cells[$"K{rowIndex - 1}:L{rowIndex - 1}"].Value = null;
    }

    private static bool IsShowTotal(BettypeWinLossBySite winloss)
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
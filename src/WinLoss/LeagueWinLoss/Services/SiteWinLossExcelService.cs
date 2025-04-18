namespace LeagueWinLoss.Services;

using AutoMapper;
using Epplus.Mapper.Extensions;
using Model;
using OfficeOpenXml;
using Utilities.Configurations;

public interface SiteWinLossExcelService
{
    Task<ExcelPackage> ExportExcel(SiteWinLossModel data, string title, string header);
}

public class SiteWinLossExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : SiteWinLossExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private ExcelWorksheet grandTotalSheet;

    private const int StartRowIndex = 7;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(SiteWinLossModel data, string title, string header)
    {
        LoadTemplate("LeagueWinLoss_Level.xlsx");
        BuildTitle(title, "A1:L2");
        BuildBodyRows(data, StartRowIndex);
        sheet.Cells["A4:L4"].Value = header;
        sheet.Cells["A5:A6"].Value = "Site";
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
        grandTotalSheet = ExcelPackage.Workbook.Worksheets["GrandTotalTemplate"];
        sheet = ExcelPackage.Workbook.Worksheets["League Win Loss"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(SiteWinLossModel data, int startRowIndex)
    {
        var rowIndex = startRowIndex;
        foreach (var site in data.Sites)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            var itemExcelModel = mapper.Map<SiteWinLossItemExcelModel>(site);
            sheet.ApplyModel(template, itemExcelModel, rowIndex++);
        }
        BuildTotalRows(data, rowIndex);
    }

    private void BuildTotalRows(SiteWinLossModel data, int startRowIndex)
    {
        var totalExcelModel = mapper.Map<SiteWinLossItemExcelModel>(data.Total);
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

    private void RenderTotalRow(SiteWinLossItem data, ref int rowIndex)
    {
        var totalExcelModel = mapper.Map<SiteWinLossItemExcelModel>(data);
        sheet.ApplyModel(grandTotalSheet, totalExcelModel, rowIndex++);
        sheet.Cells[$"A{rowIndex - 1}:F{rowIndex - 1}"].Merge = true;
        sheet.Cells[$"A{rowIndex - 1}:F{rowIndex - 1}"].Value = totalExcelModel.SiteName;
        sheet.Cells[$"K{rowIndex - 1}:L{rowIndex - 1}"].Merge = true;
        sheet.Cells[$"K{rowIndex - 1}:L{rowIndex - 1}"].Value = null;
    }

    private static bool IsShowTotal(SiteWinLossItem winloss)
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
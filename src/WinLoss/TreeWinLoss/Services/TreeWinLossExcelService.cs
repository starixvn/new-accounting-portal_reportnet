using AutoMapper;
using Epplus.Mapper.Extensions;
using Nap_Reports.Utilities.DateTimes;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using TreeWinLoss.Criteria;
using TreeWinLoss.Model;
using Utilities.Configurations;

namespace TreeWinLoss.Services;

public interface TreeWinLossExcelService
{
    Task<ExcelPackage> ExportExcelTreeWinLossByProduct(TreeWinLossByProductCriteria criteria);

    Task<ExcelPackage> ExportExcelTreeWinLossByLevel(TreeWinLossByLevelCriteria criteria);
}

public class TreeWinLossExcelServiceImp(TreeWinLossService service,
    ConfigurationService configurationService,
    IMapper mapper) : TreeWinLossExcelService
{
    private ExcelWorksheet productSheet;

    private ExcelWorksheet productOddSheet;

    private ExcelWorksheet productEvenSheet;

    private ExcelWorksheet productTotalSheet;

    private const int StartRowIndex = 6;

    private ExcelWorksheet levelSheet;

    private ExcelWorksheet levelOddSheet;

    private ExcelWorksheet levelEvenSheet;

    private ExcelWorksheet levelTotalSheet;

    private ExcelWorksheet virtualRateEvenSheet;

    private ExcelWorksheet virtualRateOddSheet;

    private const int MaxUserPerSheet = 2000;

    private const int StartRowIndexOfVirtualRate = 5;

    private ExcelPackage ExcelPackage { get; set; }

    public async Task<ExcelPackage> ExportExcelTreeWinLossByProduct(TreeWinLossByProductCriteria criteria)
    {
        var data = await service.GetTreeWinLossByProduct(criteria);
        var title =
            $"Win Loss By Currency From {criteria.FromDate.ToShortFormatedDateWithSlash().Replace('/', '_')} To {criteria.ToDate.ToShortFormatedDateWithSlash().Replace('/', '_')}";
        LoadTemplateForWinLossByProduct("Tree_Win_Loss_By_Product.xlsx");
        BuildTitle(productSheet, title, "A1:S1");
        var winLosses = mapper.Map<IEnumerable<TreeWinLossItemByProductExcelModel>>(data.WinLosses);
        BuildBodyRow( productSheet, productEvenSheet, productOddSheet, winLosses, StartRowIndex, false);
        var total = mapper.Map<IEnumerable<TreeWinLossItemByProductExcelModel>>(data.Total);
        BuildTotalRowForWinLossByProduct(total, StartRowIndex + data.WinLosses.Count);
        ClearTemplateForWinLossByProduct();
        return ExcelPackage;
    }

    public async Task<ExcelPackage> ExportExcelTreeWinLossByLevel(TreeWinLossByLevelCriteria criteria)
    {
        var title = GetReportTitleByLevel(criteria);
        LoadTemplateForWinLossByLevel("Tree_Win_Loss_By_Level.xlsx");
        var data = await service.GetTreeWinLossByLevel(criteria);
        ExportTreeWinLossInfo(data, title);
        var allVirtualRate = await service.GetAllVirtualRate(data);
        ExportExcelVirtualRateInfo(allVirtualRate, title);
        ClearTemplateForWinLossByLevel();
        ExcelPackage.Workbook.Worksheets.First().Select();
        return ExcelPackage;
    }

    public void ExportTreeWinLossInfo(TreeWinLossModel data, string title)
    {
        BuildTitle(levelSheet, title, "A1:U1");
        var winLosses = mapper.Map<IEnumerable<TreeWinLossItemByLevelExcelModel>>(data.WinLosses);
        BuildBodyRow(levelSheet, levelEvenSheet, levelOddSheet, winLosses, StartRowIndex, true);
        var total = mapper.Map<IEnumerable<TreeWinLossItemByLevelExcelModel>>(data.Total);
        var totalRowIndex = StartRowIndex + data.WinLosses.Count;
        levelSheet.ApplyModel(levelTotalSheet, total.FirstOrDefault(), totalRowIndex);
        levelSheet.Cells[$"A{totalRowIndex}:G{totalRowIndex}"].Merge = true;
        levelSheet.Cells[$"A{totalRowIndex}:G{totalRowIndex}"].Value = "Total";
        ConvertValueFromStringToDecimal(levelSheet.Cells[$"P{totalRowIndex}"]);
        ConvertValueFromStringToDecimal(levelSheet.Cells[$"S{totalRowIndex}"]);
        ConvertValueFromStringToDecimal(levelSheet.Cells[$"T{totalRowIndex}"]);
        levelSheet.Cells[$"U{totalRowIndex}"].Value = string.Empty;
    }

    private void ExportExcelVirtualRateInfo(TreeWinLossVirtualRateModel data, string title)
    {
        var maxUserPerSheet = MaxUserPerSheet;
        var redundancyUsers = data.NumberUserEnabledVirtualRate % maxUserPerSheet;
        var numberSheetBeforeRounding = data.NumberUserEnabledVirtualRate / maxUserPerSheet;
        var numberSheet = redundancyUsers == 0 ? numberSheetBeforeRounding : numberSheetBeforeRounding + 1;
        for (var sheetIndex = 0; sheetIndex < numberSheet; sheetIndex++)
        {
            var virtualRateByUserInSheet = data.SMAVirtualRate.ReportRows.Skip(sheetIndex * maxUserPerSheet).Take(maxUserPerSheet).ToList();
            var sheetName = $"VirtualRate {sheetIndex + 1}";
            var workSheet = ExcelPackage.Workbook.Worksheets.Copy("VirtualRateTemplate", sheetName);
            BuildTitle(workSheet, title, "A1:L1");
            ExportExcelVirtualRateByUser(workSheet, virtualRateByUserInSheet);
        }
    }

    private void ExportExcelVirtualRateByUser(ExcelWorksheet sheet, IEnumerable<SMAVirtualRateModel> data)
    {
        var index = 1;
        var startRowIndex = StartRowIndexOfVirtualRate;
        foreach (var item in data)
        {
            foreach (var virtualRate in item.ReportRows)
            {
                var excelModel = new VirtualRateExcelModel
                {
                    No = index,
                    User = item.UserName,
                    Sport = virtualRate.Sport,
                    Hdp = virtualRate.VirtualRateByBettypeInfo[0].VirtualRate,
                    Ou = virtualRate.VirtualRateByBettypeInfo[1].VirtualRate,
                    Oe = virtualRate.VirtualRateByBettypeInfo[2].VirtualRate,
                    Hdp1 = virtualRate.VirtualRateByBettypeInfo[3].VirtualRate,
                    Ou1 = virtualRate.VirtualRateByBettypeInfo[4].VirtualRate,
                    Oe1 = virtualRate.VirtualRateByBettypeInfo[5].VirtualRate,
                    MoneyLine = virtualRate.VirtualRateByBettypeInfo[6].VirtualRate,
                    MoneyLine1 = virtualRate.VirtualRateByBettypeInfo[7].VirtualRate,
                    SetXGameYWinner = virtualRate.VirtualRateByBettypeInfo[8].VirtualRate
                };
                var sheetTemplate = index % 2 == 0 ? virtualRateOddSheet : virtualRateEvenSheet;
                sheet.ApplyModel(sheetTemplate, excelModel, startRowIndex++);
            }

            var mergedCellsAddress = $"A{startRowIndex - item.ReportRows.Count}:A{startRowIndex - 1}";
            sheet.Cells[mergedCellsAddress].Merge = true;
            sheet.Cells[mergedCellsAddress].Value = index;
            index++;
        }
    }

    private void LoadTemplateForWinLossByProduct(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TreeWinLoss/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        productSheet = ExcelPackage.Workbook.Worksheets["TreeWinLoss"];
        productEvenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        productOddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        productTotalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void LoadTemplateForWinLossByLevel(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TreeWinLoss/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        levelSheet = ExcelPackage.Workbook.Worksheets["TreeWinLoss"];
        levelEvenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        levelOddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        levelTotalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
        virtualRateOddSheet = ExcelPackage.Workbook.Worksheets["OddVRTemplate"];
        virtualRateEvenSheet = ExcelPackage.Workbook.Worksheets["EvenVRTemplate"];
    }

    private void BuildTitle(ExcelWorksheet sheet, string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void ClearTemplateForWinLossByProduct()
    {
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
    }

    private void ClearTemplateForWinLossByLevel()
    {
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("VirtualRateTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddVRTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("EvenVRTemplate");
    }

    private void BuildBodyRow<T>(ExcelWorksheet sheet, ExcelWorksheet evenSheet, ExcelWorksheet oddSheet, IEnumerable<T> list, int startRowIndex, bool isHasNoColumn)
    {
        var index = 1;
        foreach (var item in list)
        {
            var template = startRowIndex % 2 == 0 ? evenSheet : oddSheet;
            sheet.ApplyModel(template, item, startRowIndex);
            if (isHasNoColumn)
            {
                sheet.Cells[$"A{startRowIndex}"].Value = index++;
                ConvertValueFromStringToDecimal(sheet.Cells[$"P{startRowIndex}"]);
                ConvertValueFromStringToDecimal(sheet.Cells[$"S{startRowIndex}"]);
                ConvertValueFromStringToDecimal(sheet.Cells[$"T{startRowIndex}"]);
            }
            else
            {
                ConvertValueFromStringToDecimal(sheet.Cells[$"O{startRowIndex}"]);
                ConvertValueFromStringToDecimal(sheet.Cells[$"R{startRowIndex}"]);
                ConvertValueFromStringToDecimal(sheet.Cells[$"S{startRowIndex}"]);
            }
            startRowIndex++;
        }
    }

    private void BuildTotalRowForWinLossByProduct(IEnumerable<TreeWinLossItemByProductExcelModel> list, int startRowIndex)
    {
        foreach (var item in list)
        {
            productSheet.ApplyModel(productTotalSheet, item, startRowIndex);
            productSheet.Cells[$"A{startRowIndex}:F{startRowIndex}"].Merge = true;
            ConvertValueFromStringToDecimal(productSheet.Cells[$"O{startRowIndex}"]);
            ConvertValueFromStringToDecimal(productSheet.Cells[$"R{startRowIndex}"]);
            ConvertValueFromStringToDecimal(productSheet.Cells[$"S{startRowIndex}"]);
            startRowIndex++;
        }

        productSheet.Cells[$"A{startRowIndex - 4}:F{startRowIndex - 4}"].Value = string.Empty;
    }

    private static void ConvertValueFromStringToDecimal(ExcelRangeBase cell)
    {
        if ((string)cell.Value != "N/A")
        {
            cell.Value = Convert.ToDecimal(cell.Value);
        }
    }

    private static string GetReportTitleByLevel(TreeWinLossByLevelCriteria criteria)
    {
        var title = criteria.RoleId switch
        {
            5 => $"Super Win Loss ({criteria.Name} currency)",
            4 => $"Master Win Loss (Super {criteria.Name})",
            3 => $"Agent Win Loss (Master {criteria.Name})",
            2 => $"Member Win Loss (Agent {criteria.Name})",
            _ => string.Empty
        };

        title +=
            $" - From {criteria.FromDate.ToShortFormatedDateWithSlash().Replace('/', '_')} To {criteria.ToDate.ToShortFormatedDateWithSlash().Replace('/', '_')}";

        return title;
    }
}
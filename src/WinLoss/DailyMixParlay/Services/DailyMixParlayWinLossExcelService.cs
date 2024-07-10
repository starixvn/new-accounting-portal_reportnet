using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyMixParlay.Models;
using NPOI.SS.UserModel;
using Utilities.Configurations;
using System.Linq.Expressions;

namespace DailyMixParlay.Services;

public interface DailyMixParlayWinLossExcelService
{
    ExcelPackage ExportCombinationWinLoss(WinLoss data);

    ExcelPackage ExportProductWinLoss(WinLoss data);
}

public class DailyMixParlayWinLossExcelServiceImp(ConfigurationService configurationService) : DailyMixParlayWinLossExcelService
{
    private ExcelPackage ExcelPackage { get; set; }

    private ExcelWorksheet twoXCombinationsSheet;
    private ExcelWorksheet threeXCombinationsSheet;
    private ExcelWorksheet fourXCombinationsSheet;
    private ExcelWorksheet fiveXCombinationsSheet;
    private ExcelWorksheet sixXCombinationsSheet;
    private ExcelWorksheet subTotalSheet;
    private ExcelWorksheet grandTotalSheet;
    private ExcelWorksheet totalSheet;
    private ExcelWorksheet sheet;

    private int rowIndex = 6;

    public ExcelPackage ExportCombinationWinLoss(WinLoss data)
    {
        var fileExcel = "Daily_Mix_Parlay_Win_Loss_Combination.xlsx";
        LoadTemplate(fileExcel);
        ExportData(data);
        return ExcelPackage;
    }

    public ExcelPackage ExportProductWinLoss(WinLoss data)
    {
        throw new NotImplementedException();
    }

    private void ExportData(WinLoss data)
    {
        foreach (var combinationGroup in data.WinLosses)
        {
            BindFirstMergeValue(combinationGroup.Name, combinationGroup.MergeRowNumber);
            foreach (var liveGroup in combinationGroup.WinLosses)
            {
                BindSecondMergeValue(liveGroup.Name, liveGroup.MergeRowNumber);
                foreach (var item in liveGroup.WinLosses)
                {
                    ExportDataRow(item);
                    rowIndex++;
                }

                ExportTotalDataRow(liveGroup.Total, subTotalSheet, true);
                rowIndex++;
            }
            
            ExportTotalDataRow(combinationGroup.Total, totalSheet, false);
            rowIndex++;
        }
    }

    private void BindFirstMergeValue(string name, int numberMergeRow)
    {
        var index = rowIndex + numberMergeRow - 1;
        switch (name)
        {
            case "2XCombinations":
                twoXCombinationsSheet.Cells["A6:A6"]
                 .Copy(sheet.Cells[$"A{rowIndex}:A{index}"]);
                break;
            case "3XCombinations":
                threeXCombinationsSheet.Cells["A6:A6"]
                 .Copy(sheet.Cells[$"A{rowIndex}:A{index}"]);
                break;
            case "4XCombinations":
                fourXCombinationsSheet.Cells["A6:A6"]
                 .Copy(sheet.Cells[$"A{rowIndex}:A{index}"]);
                break;
            case "5XCombinations":
                fiveXCombinationsSheet.Cells["A6:A6"]
                 .Copy(sheet.Cells[$"A{rowIndex}:A{index}"]);
                break;
            default:
                sixXCombinationsSheet.Cells["A6:A6"]
                 .Copy(sheet.Cells[$"A{rowIndex}:A{index}"]);
                break;
        }

        sheet.Cells[$"A{rowIndex}:A{index}"].Merge = true;
        sheet.Cells[$"A{rowIndex}:A{index}"].Value = name;
    }

    private void BindSecondMergeValue(string name, int numberMergeRow)
    {
        var index = rowIndex + numberMergeRow - 1;
        sheet.Cells[$"B{rowIndex}:B{index}"].Clear();
        sheet.Cells[$"B{rowIndex}:B{index}"].Merge = true;
        sheet.Cells[$"B{rowIndex}:B{index}"].Value = name;
        
    }

    private void ExportDataRow(WinLossItem item)
    {
        twoXCombinationsSheet.Cells["B6:N6"]
            .Copy(sheet.Cells[$"B{rowIndex}:N{rowIndex}"]);
        sheet.Cells[$"C{rowIndex}"].Value = item.Name;
        sheet.Cells[$"D{rowIndex}"].Value = item.MemberWinLoss;
        sheet.Cells[$"E{rowIndex}"].Value = item.MemberTurnover;
        sheet.Cells[$"F{rowIndex}"].Value = item.BetCount;
        sheet.Cells[$"G{rowIndex}"].Value = item.SMAWinLoss;
        sheet.Cells[$"H{rowIndex}"].Value = item.ExtraPT;
        sheet.Cells[$"I{rowIndex}"].Value = item.Commission;
        sheet.Cells[$"J{rowIndex}"].Value = item.RealIncome;
        sheet.Cells[$"K{rowIndex}"].Value = item.RealTurnover;
        sheet.Cells[$"L{rowIndex}"].Value = item.Margin;
        sheet.Cells[$"M{rowIndex}"].Value = item.MemberBonus;
        sheet.Cells[$"N{rowIndex}"].Value = item.HouseBonus;
    }

    private void ExportTotalDataRow(WinLossItem item, ExcelWorksheet totalTemplate, bool isSubTotal)
    {
        if (isSubTotal)
        {
            totalTemplate.Cells["C6:N6"]
                .Copy(sheet.Cells[$"C{rowIndex}:N{rowIndex}"]);
        }
        else
        {
            totalTemplate.Cells["B6:N6"]
                .Copy(sheet.Cells[$"B{rowIndex}:N{rowIndex}"]);
            sheet.Cells[$"B{rowIndex}"].Value = string.Empty;
        }

        sheet.Cells[$"C{rowIndex}"].Value = item.Name;
        sheet.Cells[$"D{rowIndex}"].Value = item.MemberWinLoss;
        sheet.Cells[$"E{rowIndex}"].Value = item.MemberTurnover;
        sheet.Cells[$"F{rowIndex}"].Value = item.BetCount;
        sheet.Cells[$"G{rowIndex}"].Value = item.SMAWinLoss;
        sheet.Cells[$"H{rowIndex}"].Value = item.ExtraPT;
        sheet.Cells[$"I{rowIndex}"].Value = item.Commission;
        sheet.Cells[$"J{rowIndex}"].Value = item.RealIncome;
        sheet.Cells[$"K{rowIndex}"].Value = item.RealTurnover;
        sheet.Cells[$"L{rowIndex}"].Value = item.Margin;
        sheet.Cells[$"M{rowIndex}"].Value = item.MemberBonus;
        sheet.Cells[$"N{rowIndex}"].Value = item.HouseBonus;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/DailyMixParlay/{fileExcel}";
        using (var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read))
        {
            ExcelPackage = new ExcelPackage(stream);
            sheet = ExcelPackage.Workbook.Worksheets.Copy("DailyMixParlayWinLossTemplate", "Daily Mix Parlay Win Loss");
            twoXCombinationsSheet = ExcelPackage.Workbook.Worksheets["2XCombinationsTemplate"];
            threeXCombinationsSheet = ExcelPackage.Workbook.Worksheets["3XCombinationsTemplate"];
            fourXCombinationsSheet = ExcelPackage.Workbook.Worksheets["4XCombinationsTemplate"];
            fiveXCombinationsSheet = ExcelPackage.Workbook.Worksheets["5XCombinationsTemplate"];
            sixXCombinationsSheet = ExcelPackage.Workbook.Worksheets["6XCombinationsTemplate"];
            subTotalSheet = ExcelPackage.Workbook.Worksheets["SubTotalTemplate"];
            grandTotalSheet = ExcelPackage.Workbook.Worksheets["GrandTotalTemplate"];
            totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
        }
    }


}
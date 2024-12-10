using OfficeOpenXml;
using DailyMixParlay.Models;
using Utilities.Configurations;
using DailyMixParlay.Criteria;
using DailyMixParlay.Constant;

namespace DailyMixParlay.Services;

public interface DailyMixParlayWinLossExcelService
{
    ExcelPackage ExportCombinationWinLoss(WinLoss data, GetCombinationWinLossCriteria criteria);

    ExcelPackage ExportProductWinLoss(WinLoss data, GetAdvanceMixParlayCriteria criteria);
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
    private ExcelWorksheet nonLiveSheet;
    private ExcelWorksheet liveSheet;
    private ExcelWorksheet productSubTotalSheet;
    private ExcelWorksheet productTotalSheet;
    private ExcelWorksheet productGrandTotalSheet;
    private ExcelWorksheet productSheet;

    private int rowIndex = 6;
    private int productRowIndex = 5;

    public ExcelPackage ExportCombinationWinLoss(WinLoss data, GetCombinationWinLossCriteria criteria)
    {
        LoadCombinationTemplate("Daily_Mix_Parlay_Win_Loss_Combination.xlsx");
        GetCombinationTitle(criteria);
        ExportCombinationData(data);
        DeleteCombinationTemplate();
        return ExcelPackage;
    }

    public ExcelPackage ExportProductWinLoss(WinLoss data, GetAdvanceMixParlayCriteria criteria)
    {
        LoadProductTemplate("Daily_Mix_Parlay_Win_Loss_Product.xlsx");
        GetProductTitle(criteria);
        ExportProductData(data);
        DeleteProductTemplate();
        return ExcelPackage;
    }

    private void GetCombinationTitle(GetCombinationWinLossCriteria criteria)
    {
        var siteName = GetSiteTypeName(criteria.Site);
        sheet.Cells["A1:N2"].Value = $"Parlay Combination Win Loss by {siteName} - From {criteria.FromDate: MMM dd, yyyy} To {criteria.ToDate: MMM dd, yyyy}";
    }

    private void ExportCombinationData(WinLoss data)
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

        ExportGrandTotalDataRow(data.Total);
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
        twoXCombinationsSheet.Cells["B6:B6"]
            .Copy(sheet.Cells[$"B{rowIndex}:B{index}"]);
        sheet.Cells[$"B{rowIndex}:B{index}"].Merge = true;
        sheet.Cells[$"B{rowIndex}:B{index}"].Value = name;
        
    }

    private void ExportDataRow(WinLossItem item)
    {
        twoXCombinationsSheet.Cells["C6:N6"]
            .Copy(sheet.Cells[$"C{rowIndex}:N{rowIndex}"]);
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

    private void ExportGrandTotalDataRow(WinLossItem item)
    {
        grandTotalSheet.Cells["A6:N6"]
            .Copy(sheet.Cells[$"A{rowIndex}:N{rowIndex}"]);
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
        sheet.Cells[$"A{rowIndex}:C{rowIndex}"].Merge = true;
        sheet.Cells[$"A{rowIndex}:C{rowIndex}"].Value = item.Name;
    }

    private void LoadCombinationTemplate(string fileExcel)
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

    private void DeleteCombinationTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("DailyMixParlayWinLossTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("2XCombinationsTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("3XCombinationsTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("4XCombinationsTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("5XCombinationsTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("6XCombinationsTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("SubTotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("GrandTotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }

    private void LoadProductTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/DailyMixParlay/{fileExcel}";
        using (var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read))
        {
            ExcelPackage = new ExcelPackage(stream);
            productSheet = ExcelPackage.Workbook.Worksheets.Copy("DailyMixParlayWinLossTemplate", "Daily Mix Parlay Win Loss");
            nonLiveSheet = ExcelPackage.Workbook.Worksheets["NonLiveTemplate"];
            liveSheet = ExcelPackage.Workbook.Worksheets["LiveTemplate"];
            productSubTotalSheet = ExcelPackage.Workbook.Worksheets["SubTotalTemplate"];
            productGrandTotalSheet = ExcelPackage.Workbook.Worksheets["GrandTotalTemplate"];
            productTotalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
        }
    }

    private void ExportProductData(WinLoss data)
    {
        foreach (var productGroup in data.WinLosses)
        {
            BindProductFirstMergeValue(productGroup.Name, productGroup.MergeRowNumber);
            foreach (var liveGroup in productGroup.WinLosses)
            {
                BindProductSecondMergeValue(liveGroup.Name, liveGroup.MergeRowNumber);
                foreach (var item in liveGroup.WinLosses)
                {
                    ExportProductDataRow(item);
                    productRowIndex++;
                }

                ExportProductTotalDataRow(liveGroup.Total, productSubTotalSheet, true);
                productRowIndex++;
            }

            ExportProductTotalDataRow(productGroup.Total, productTotalSheet, false);
            productRowIndex++;
        }

        ExportProductGrandTotalDataRow(data.Total);
    }

    private void ExportProductGrandTotalDataRow(WinLossItem item)
    {
        productGrandTotalSheet.Cells["A5:L5"]
            .Copy(productSheet.Cells[$"A{productRowIndex}:L{productRowIndex}"]);
        productSheet.Cells[$"D{productRowIndex}"].Value = item.MemberWinLoss;
        productSheet.Cells[$"E{productRowIndex}"].Value = item.SMAWinLoss;
        productSheet.Cells[$"F{productRowIndex}"].Value = item.ExtraPT;
        productSheet.Cells[$"G{productRowIndex}"].Value = item.Commission;
        productSheet.Cells[$"H{productRowIndex}"].Value = item.RealIncome;
        productSheet.Cells[$"I{productRowIndex}"].Value = item.RealTurnover;
        productSheet.Cells[$"J{productRowIndex}"].Value = item.Margin;
        productSheet.Cells[$"K{productRowIndex}"].Value = item.MemberBonus;
        productSheet.Cells[$"L{productRowIndex}"].Value = item.HouseBonus;
        productSheet.Cells[$"A{productRowIndex}:C{productRowIndex}"].Merge = true;
        productSheet.Cells[$"A{productRowIndex}:C{productRowIndex}"].Value = item.Name;
    }

    private void BindProductFirstMergeValue(string name, int numberMergeRow)
    {
        var index = productRowIndex + numberMergeRow - 1;
        if (name == "Non-Live")
        {
            nonLiveSheet.Cells["A5:A5"]
                .Copy(productSheet.Cells[$"A{productRowIndex}:A{index}"]);
        }
        else
        {
            liveSheet.Cells["A5:A5"]
                .Copy(productSheet.Cells[$"A{productRowIndex}:A{index}"]);
        }

        productSheet.Cells[$"A{productRowIndex}:A{index}"].Merge = true;
        productSheet.Cells[$"A{productRowIndex}:A{index}"].Value = name;
    }

    private void BindProductSecondMergeValue(string name, int numberMergeRow)
    {
        var index = productRowIndex + numberMergeRow - 1;
        nonLiveSheet.Cells["B5:B5"]
            .Copy(productSheet.Cells[$"B{productRowIndex}:B{index}"]);
        productSheet.Cells[$"B{productRowIndex}:B{index}"].Merge = true;
        productSheet.Cells[$"B{productRowIndex}:B{index}"].Value = name;

    }

    private void ExportProductDataRow(WinLossItem item)
    {
        nonLiveSheet.Cells["C5:L5"]
            .Copy(productSheet.Cells[$"C{productRowIndex}:L{productRowIndex}"]);
        productSheet.Cells[$"C{productRowIndex}"].Value = item.Name;
        productSheet.Cells[$"D{productRowIndex}"].Value = item.MemberWinLoss;
        productSheet.Cells[$"E{productRowIndex}"].Value = item.SMAWinLoss;
        productSheet.Cells[$"F{productRowIndex}"].Value = item.ExtraPT;
        productSheet.Cells[$"G{productRowIndex}"].Value = item.Commission;
        productSheet.Cells[$"H{productRowIndex}"].Value = item.RealIncome;
        productSheet.Cells[$"I{productRowIndex}"].Value = item.RealTurnover;
        productSheet.Cells[$"J{productRowIndex}"].Value = item.Margin;
        productSheet.Cells[$"K{productRowIndex}"].Value = item.MemberBonus;
        productSheet.Cells[$"L{productRowIndex}"].Value = item.HouseBonus;
    }

    private void ExportProductTotalDataRow(WinLossItem item, ExcelWorksheet totalTemplate, bool isSubTotal)
    {
        if (isSubTotal)
        {
            totalTemplate.Cells["C5:L5"]
                .Copy(productSheet.Cells[$"C{productRowIndex}:L{productRowIndex}"]);
        }
        else
        {
            totalTemplate.Cells["B5:L5"]
                .Copy(productSheet.Cells[$"B{productRowIndex}:L{productRowIndex}"]);
            productSheet.Cells[$"B{productRowIndex}"].Value = string.Empty;
        }

        productSheet.Cells[$"C{productRowIndex}"].Value = item.Name;
        productSheet.Cells[$"D{productRowIndex}"].Value = item.MemberWinLoss;
        productSheet.Cells[$"E{productRowIndex}"].Value = item.SMAWinLoss;
        productSheet.Cells[$"F{productRowIndex}"].Value = item.ExtraPT;
        productSheet.Cells[$"G{productRowIndex}"].Value = item.Commission;
        productSheet.Cells[$"H{productRowIndex}"].Value = item.RealIncome;
        productSheet.Cells[$"I{productRowIndex}"].Value = item.RealTurnover;
        productSheet.Cells[$"J{productRowIndex}"].Value = item.Margin;
        productSheet.Cells[$"K{productRowIndex}"].Value = item.MemberBonus;
        productSheet.Cells[$"L{productRowIndex}"].Value = item.HouseBonus;
    }

    private void GetProductTitle(GetAdvanceMixParlayCriteria criteria)
    {
        var siteName = GetSiteTypeName(criteria.Site);
        productSheet.Cells["A1:L2"].Value = $"Parlay Win Loss by Product by {siteName} - From {criteria.FromDate: MMM dd, yyyy} To {criteria.ToDate: MMM dd, yyyy}";
    }

    private void DeleteProductTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("DailyMixParlayWinLossTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("NonLiveTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("LiveTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("SubTotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("GrandTotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }

    public static string GetSiteTypeName(int siteType)
    {
        switch (siteType)
        {
            case DailyMixParlayConstant.AllSites:
                return "All Sites";

            case DailyMixParlayConstant.Credit:
                return "Credit";

            case DailyMixParlayConstant.Deposit:
                return "Deposit";

            default:
                return string.Empty;
        }
    }
}
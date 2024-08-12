using NegativeBalance.Criteria;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Configurations;

namespace NegativeBalance.Services;

public interface NegativeBalanceExcelService
{
    Task<ExcelPackage> ExportExcelTreeWinLossByProduct(NegativeBalanceCriteria criteria);
}

public class NegativeBalanceExcelServiceImp(ConfigurationService configurationService) : NegativeBalanceExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet licenseesSheet;

    private const int StartRowIndex = 4;

    private ExcelPackage ExcelPackage { get; set; }


    public async Task<ExcelPackage> ExportExcelTreeWinLossByProduct(NegativeBalanceCriteria criteria)
    {
        LoadTemplate("Licensee-NegativeBalance.xlsx");
        var siteName = (criteria.Site == "All" ?
            "All Licensees" :
            criteria.Site);
        var title = $"Accounting system - Negative Balance Report for {siteName} ";
        BuildTitle(sheet, title, "A1:C1");
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/NegativeBalance/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["NegativeBalance"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        licenseesSheet = ExcelPackage.Workbook.Worksheets["LicenseesTemplate"];
    }

    private void BuildTitle(ExcelWorksheet sheet, string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("LicenseesTemplate");
    }
}
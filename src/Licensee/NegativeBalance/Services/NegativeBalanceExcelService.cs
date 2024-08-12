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


    public Task<ExcelPackage> ExportExcelTreeWinLossByProduct(NegativeBalanceCriteria criteria)
    {
        throw new NotImplementedException();
    }

    private void LoadTemplateForWinLossByProduct(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TreeWinLoss/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["TreeWinLoss"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        licenseesSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }
}
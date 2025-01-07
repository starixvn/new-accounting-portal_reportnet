using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using Utilities.Configurations;

namespace Exchange.Report;

public interface LicenseeExcelBuilder
{
    ExcelPackage Build(LicenseeHttpParameterExcel excelParams, ExchangeExcelModel model);
}

public class LicenseeExcelBuilderImp(ConfigurationService configurationService) : LicenseeExcelBuilder
{
    private ExcelWorksheet exchangeSheet;

    private ExcelWorksheet dataSheet;

    private ExcelWorksheet subTotalSheet;

    private ExcelWorksheet totalSheet;

    private int StartRowIndex = 5;

    private ExcelPackage ExcelPackage { get; set; }

    public ExcelPackage Build(LicenseeHttpParameterExcel excelParams, ExchangeExcelModel model)
    {
        LoadTemplate();
        BuildTitle(excelParams);
        BuildBody(model);
        BuildTotal(model);
        SetNameForFirstHeaderColumn();
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadTemplate()
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/Exchange/LicenseeExchange.xlsx";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        exchangeSheet = ExcelPackage.Workbook.Worksheets.Copy("ExchangeTemplate", "Exchange");
        dataSheet = ExcelPackage.Workbook.Worksheets["DataTemplate"];
        subTotalSheet = ExcelPackage.Workbook.Worksheets["SubTotalTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTitle(LicenseeHttpParameterExcel excelParams)
    {
        var fromDate = $"{excelParams.FromDate: MMM dd, yyyy}";
        var toDate = $"{excelParams.ToDate: MMM dd, yyyy}";
        exchangeSheet.Cells["A1:N2"].Value = $"Exchange WinLoss From {fromDate} to {toDate}";
    }

    private void SetNameForFirstHeaderColumn()
    {
        exchangeSheet.Cells["A3:A4"].Value = "Licensee";
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("ExchangeTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("DataTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("SubTotalTemplate");
    }

    private void BuildBody(ExchangeExcelModel model)
    {
        foreach(var item in model.ExchangeLicensees)
        {
            BuildSubTotalRow(item.Total);
            BuildLicenseeMarket(item.Entities);
        }
    }

    private void BuildSubTotalRow(ExchangeExcelEntity subTotal)
    {
        exchangeSheet.ApplyModel(subTotalSheet, subTotal, StartRowIndex);
        StartRowIndex += 1;
    }

    private void BuildLicenseeMarket(List<ExchangeExcelEntity> markets)
    { 
        foreach (var market in markets)
        {
            exchangeSheet.ApplyModel(dataSheet, market, StartRowIndex);
            exchangeSheet.Cells[$"A{StartRowIndex}:A{StartRowIndex}"].Value = market.MarketType;
            StartRowIndex += 1;
        }
    }

    private void BuildTotal(ExchangeExcelModel model)
    {
        exchangeSheet.ApplyModel(totalSheet, model.GrandTotal, StartRowIndex);
        exchangeSheet.Cells[$"A{StartRowIndex}:A{StartRowIndex}"].Value = "Grand Total";
        StartRowIndex += 1;
            
        foreach (var item in model.MarketTypeTotal)
        {
            exchangeSheet.ApplyModel(totalSheet, item, StartRowIndex);
            exchangeSheet.Cells[$"A{StartRowIndex}:A{StartRowIndex}"].Value = item.MarketType;
            StartRowIndex += 1;
        }
    }
}
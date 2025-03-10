namespace SBCustomerPerformanceByDanger.Report;

using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using Utilities.Configurations;
using Utilities.LinqDynamicSum;

public interface ReportExcelBuilder
{
    ExcelPackage Build(string title, SummaryEntity<CustomerPerformanceByDangerExcelModel> model);
}

public class ReportExcelBuilderImp(ConfigurationService configurationService) : ReportExcelBuilder
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet totalSheet;

    private const int StartRowIndex = 4;

    private ExcelPackage ExcelPackage { get; set; }

    public ExcelPackage Build(string title, SummaryEntity<CustomerPerformanceByDangerExcelModel> model)
    {
        LoadTemplate();
        BuildTitle(title, "A1:P1");
        sheet.ApplyVertical(model.Entities, StartRowIndex);
        BuildTotalRow(model);
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadTemplate()
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/SBCustomerPerformanceByDangers/SBCustomerPerformanceByDangers.xlsx";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets.Copy("BodyTemplate", "Customer Performance");
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTotalRow(SummaryEntity<CustomerPerformanceByDangerExcelModel> model)
    {
        var endRowIndex = StartRowIndex + model.Entities.Count;
        sheet.ApplyModel(totalSheet, model.Total, StartRowIndex + model.Entities.Count);
        var cellAddressOfTotalText = new ExcelAddressBase(endRowIndex, 1, endRowIndex, 7);
        sheet.Cells[cellAddressOfTotalText.Address].Value = "Total";
        sheet.Cells[cellAddressOfTotalText.Address].Merge = true;
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("BodyTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }
}
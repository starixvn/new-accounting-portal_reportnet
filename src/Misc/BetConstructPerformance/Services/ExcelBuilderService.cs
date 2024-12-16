using BetConstructPerformance.Models;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Utilities.Configurations;

namespace BetConstructPerformance.Services;

public interface ExcelBuilderService
{
    ExcelPackage BuildSport(string title, SportExcelModel model);

    ExcelPackage BuildSportDetail(string title, SportDetailExcelModel model);
}

public class ExcelBuilderServiceImp(ConfigurationService configurationService) : ExcelBuilderService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet totalSheet;

    private ExcelPackage ExcelPackage { get; set; }

    private const int StartRowIndex = 4;
      
    public ExcelPackage BuildSport(string title, SportExcelModel model)
    {
        LoadTemplate("Sport.xlsx");
        BuildTitle(title, "A1:I1");
        sheet.ApplyVertical(model.Items, StartRowIndex);
        var startSummaryRowIndex = model.Items.Count() + StartRowIndex;
        sheet.ApplyModel(totalSheet, model.Total, startSummaryRowIndex);
        FormatSheet(model.Items.Count(), $"A{startSummaryRowIndex}", false);
        ClearTemplate();
        return ExcelPackage;
    }

    public ExcelPackage BuildSportDetail(string title, SportDetailExcelModel model)
    {
        LoadTemplate("SportDetail.xlsx");
        BuildTitle(title, "A1:N1");
        sheet.ApplyVertical(model.Items, StartRowIndex);
        var startSummaryRowIndex = model.Items.Count() + StartRowIndex;
        sheet.ApplyModel(totalSheet, model.Total, startSummaryRowIndex);
        var summaryAddress = $"A{startSummaryRowIndex}:F{startSummaryRowIndex}";
        sheet.Cells[summaryAddress].Merge = true;
        FormatSheet(model.Items.Count(), summaryAddress, true);
        ClearTemplate();
        return ExcelPackage;
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $@"{configurationService.CurrentDirectory}/ExcelTemplate/BetConstruct/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Data"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void FormatSheet(int totalItems, string grandTotalCellAddress, bool isDetail)
    {
        for (var i = 0; i < totalItems; i++)
        {
            var rowIndex = StartRowIndex + i;
            var cellAddress = isDetail ? $"A{rowIndex}:N{rowIndex}" : $"A{rowIndex}:I{rowIndex}";
            var backgroundColor = i % 2 == 0 ? Color.White : Color.FromArgb(228, 242, 253);
            sheet.Cells[cellAddress].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[cellAddress].Style.Fill.BackgroundColor.SetColor(backgroundColor);
        }

        sheet.Cells[grandTotalCellAddress].Value = "Grand Total";
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }
}
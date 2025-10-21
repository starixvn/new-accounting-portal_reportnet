using System.Drawing;
using DstPerformance.Models;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Utilities.Configurations;

namespace DstPerformance.Services;

public interface ExcelBuilderService
{
    ExcelPackage BuildSport(string title, SportExcelModel model);
}

public class ExcelBuilderServiceImp(ConfigurationService configurationService) : ExcelBuilderService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet totalSheet;

    private ExcelPackage ExcelPackage { get; set; }

    private const int StartRowIndex = 4;
      
    public ExcelPackage BuildSport(string title, SportExcelModel model)
    {
        LoadTemplate("Report.xlsx");
        sheet.Cells["A1:J1"].Value = title;
        sheet.ApplyVertical(model.Items, StartRowIndex);
        var startSummaryRowIndex = model.Items.Count() + StartRowIndex;
        sheet.ApplyModel(totalSheet, model.Total, model.Items.Count() + StartRowIndex);
        FormatSheet(model.Items.Count(), $"A{startSummaryRowIndex}");
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $@"{configurationService.CurrentDirectory}/ExcelTemplate/DST/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Data"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }

    private void FormatSheet(int totalItems, string grandTotalCellAddress)
    {
        for (var i = 0; i < totalItems; i++)
        {
            var rowIndex = StartRowIndex + i;
            var cellAddress = $"A{rowIndex}:J{rowIndex}";
            var backgroundColor = i % 2 == 0 ? Color.White : Color.FromArgb(228, 242, 253);
            sheet.Cells[cellAddress].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[cellAddress].Style.Fill.BackgroundColor.SetColor(backgroundColor);
        }

        sheet.Cells[grandTotalCellAddress].Value = "Grand Total";
    }
}
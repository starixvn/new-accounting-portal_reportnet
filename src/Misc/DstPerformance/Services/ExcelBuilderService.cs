using DstPerformance.Constants;
using DstPerformance.Models;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Utilities.Configurations;

namespace DstPerformance.Services;

public interface ExcelBuilderService
{
    ExcelPackage BuildExcel(string title, string level, ReportExcelModel model);
}

public class ExcelBuilderServiceImp(ConfigurationService configurationService) : ExcelBuilderService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet totalSheet;

    private ExcelPackage ExcelPackage { get; set; }

    private const int StartRowIndex = 4;

    private const int DateColumnIndex = 1;
      
    public ExcelPackage BuildExcel(string title, string level, ReportExcelModel model)
    {
        LoadTemplate("Report.xlsx");
        sheet.Cells["A1:J1"].Value = title;
        sheet.Cells["B3"].Value = level;
        sheet.ApplyVertical(model.Items, StartRowIndex);
        var startSummaryRowIndex = model.Items.Count() + StartRowIndex;
        sheet.ApplyModel(totalSheet, model.Total, model.Items.Count() + StartRowIndex);
        DeleteRedundantColumns(level);
        var grandTotalCellAddress = level != LevelConstants.BetType
            ? $"A{startSummaryRowIndex}"
            : $"A{startSummaryRowIndex}:B{startSummaryRowIndex}";
        FormatSheet(model.Items.Count(), grandTotalCellAddress, level);
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $@"{configurationService.CurrentDirectory}/ExcelTemplate/DstPerformance/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Data"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }

    private void FormatSheet(int totalItems, string grandTotalCellAddress, string level)
    {
        for (var i = 0; i < totalItems; i++)
        {
            var rowIndex = StartRowIndex + i;
            var cellAddress = $"A{rowIndex}:J{rowIndex}";
            var backgroundColor = i % 2 == 0 ? Color.White : Color.FromArgb(228, 242, 253);
            sheet.Cells[cellAddress].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[cellAddress].Style.Fill.BackgroundColor.SetColor(backgroundColor);
        }

        if (level == LevelConstants.Sport)
        {
            sheet.Column(ExcelConstants.SportColumnIndex).Width = ExcelConstants.SportColumnWidth;
        }
        sheet.Cells[grandTotalCellAddress].Merge = true;
        sheet.Cells[grandTotalCellAddress].Value = "Grand Total";
        sheet.Cells[grandTotalCellAddress].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
    }

    private void DeleteRedundantColumns(string level)
    {
        if (level != LevelConstants.BetType)
        {
            sheet.DeleteColumn(DateColumnIndex);
        }
    }
}
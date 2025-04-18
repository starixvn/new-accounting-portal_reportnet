namespace ParlayWon.Services;

using AutoMapper;
using Epplus.Mapper.Extensions;
using Model;
using OfficeOpenXml;
using Utilities.Configurations;

public interface ParlayWonExportExcelService
{
    Task<ExcelPackage> ExportExcel(ParlayWonTransaction data, string title);
}

public class ParlayWonExportExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : ParlayWonExportExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private const int StartRowIndex = 6;

    private ExcelPackage ExcelPackage { get; set; }


    public Task<ExcelPackage> ExportExcel(ParlayWonTransaction data, string title)
    {
        var excelData = mapper.Map<ParlayWonTransactionExcelModel>(data);
        LoadTemplate("ParlayWon.xlsx");
        BuildTitle(title, "A1:L2");
        BuildBody(excelData.Items, StartRowIndex);
        var totalRowIndex = excelData.Items.Count + StartRowIndex;
        BuildTotalRow(excelData.Total, totalRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string templateName)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/ParlayWon/{templateName}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["ParlayWon"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBody(List<ParlayWonTransactionExcelModel> data, int startRowIndex)
    {
        var rowIndex = startRowIndex;
            
        foreach (var item in data)
        {
            var template = rowIndex % 2 == 0 ? evenSheet : oddSheet;
            sheet.ApplyModel(template, item, rowIndex++);
        }
    }

    private void BuildTotalRow(ParlayWonTransactionExcelModel total, int rowIndex)
    {
        sheet.ApplyModel(totalSheet, total, rowIndex);
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }
}
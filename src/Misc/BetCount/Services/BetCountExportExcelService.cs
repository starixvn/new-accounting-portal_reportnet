using AutoMapper;
using BetCount.Model;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using Utilities.Configurations;

namespace BetCount.Services;

public interface BetCountExportExcelService
{
    Task<ExcelPackage> ExportExcel(TransactionModel data, string title);

}

public class BetCountExportExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : BetCountExportExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet bodySheet;

    private ExcelWorksheet subTotalSheet;

    private ExcelWorksheet totalSheet;

    private const int StartRowIndex = 6;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(TransactionModel data, string title)
    {
        var excelData = mapper.Map<TransactionExcelModel>(data);
        LoadTemplate("BetCount.xlsx");
        BuildTitle(title, "A1:L2");
        var currentRowIndex = BuildBody(excelData.Items, StartRowIndex);
        BuildTotalRow(excelData.Total, currentRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string templateName)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/BetCount/{templateName}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Bet Count"];
        subTotalSheet = ExcelPackage.Workbook.Worksheets["SubTotalTemplate"];
        bodySheet = ExcelPackage.Workbook.Worksheets["BodyTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private int BuildBody(List<TransactionExcelModel> data, int startRowIndex)
    {
        var rowIndex = startRowIndex;

        foreach (var item in data)
        {
            sheet.ApplyModel(subTotalSheet, item.Total, rowIndex++);
            sheet.ApplyVertical(bodySheet, item.Items, rowIndex);
            rowIndex += item.Items.Count;
        }

        return rowIndex;
    }

    private void BuildTotalRow(TransactionExcelModel total, int rowIndex)
    {
        sheet.ApplyModel(totalSheet, total, rowIndex);
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("SubTotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("BodyTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }
}
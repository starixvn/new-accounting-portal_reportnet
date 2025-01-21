using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using SabaPromotion.Models;
using Utilities.Configurations;

namespace SabaPromotion.Services;

public interface ExcelBuilderService
{
    Task<ExcelPackage> Build(string title, SabaPromotionModel model);
}

public class ExcelBuilderServiceImp(ConfigurationService configurationService, IMapper mapper) : ExcelBuilderService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet totalSheet;

    private ExcelPackage ExcelPackage { get; set; }

    private const int StartRowIndex = 4;

    public Task<ExcelPackage> Build(string title, SabaPromotionModel model)
    {
        LoadTemplate("SabaPromotion.xlsx");
        BuildTitle(title, "A1:D1");
        sheet.ApplyVertical(mapper.Map<IEnumerable<SabaPromotionExcelModel>>(model.Items), StartRowIndex);
        var startSummaryRowIndex = model.Items.Count() + StartRowIndex;
        sheet.ApplyModel(totalSheet, model.Total, startSummaryRowIndex);
        BuildTotal(mapper.Map<SabaPromotionExcelModel>(model.Total), startSummaryRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildTotal(SabaPromotionExcelModel model, int index)
    {
        sheet.ApplyModel(totalSheet, model, index);
        var mergedCells = $"A{index}:C{index}";
        sheet.Cells[mergedCells].Merge = true;
        sheet.Cells[mergedCells].Value = "Grand Total";
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $@"{configurationService.CurrentDirectory}/ExcelTemplate/SabaPromotion/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Data"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }
}
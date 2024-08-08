using AutoMapper;
using Epplus.Mapper.Extensions;
using Nap_Reports.Utilities.DateTimes;
using OfficeOpenXml;
using TreeWinLoss.Criteria;
using TreeWinLoss.Model;
using Utilities.Configurations;

namespace TreeWinLoss.Services;

public interface TreeWinLossExcelService
{
    Task<ExcelPackage> ExportExcelTreeWinLossByProduct(TreeWinLossByProductCriteria criteria);

    Task<ExcelPackage> ExportExcelTreeWinLossByLevel(TreeWinLossByLevelCriteria criteria);
}

public class TreeWinLossExcelServiceImp(TreeWinLossService service,
    ConfigurationService configurationService,
    IMapper mapper) : TreeWinLossExcelService
{
    private ExcelWorksheet productSheet;

    private ExcelWorksheet productOddSheet;

    private ExcelWorksheet productEvenSheet;

    private ExcelWorksheet productTotalSheet;

    private const int StartRowIndex = 6;

    private ExcelPackage ExcelPackage { get; set; }

    public async Task<ExcelPackage> ExportExcelTreeWinLossByProduct(TreeWinLossByProductCriteria criteria)
    {
        var data = await service.GetTreeWinLossByProduct(criteria);
        var title =
            $"Win Loss By Currency From {criteria.FromDate.ToShortFormatedDateWithSlash().Replace('/', '_')} To {criteria.ToDate.ToShortFormatedDateWithSlash().Replace('/', '_')}";
        LoadTemplateForWinLossByProduct("Tree_Win_Loss_By_Product.xlsx");
        BuildTitle(title, "A1:S1");
        var winLosses = mapper.Map<IEnumerable<TreeWinLossItemByProductExcelModel>>(data.WinLosses);
        BuildBodyRowForWinLossByProduct(winLosses, StartRowIndex);
        var total = mapper.Map<IEnumerable<TreeWinLossItemByProductExcelModel>>(data.Total);
        BuildTotalRowForWinLossByProduct(total, StartRowIndex + data.WinLosses.Count);
        ClearTemplateForWinLossByProduct();
        return ExcelPackage;
    }

    public async Task<ExcelPackage> ExportExcelTreeWinLossByLevel(TreeWinLossByLevelCriteria criteria)
    {
        var data = await service.GetTreeWinLossByLevel(criteria);
        var allVirtualRate = await service.GetAllVirtualRate(data);
        throw new NotImplementedException();
    }

    private void LoadTemplateForWinLossByProduct(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TreeWinLoss/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        productSheet = ExcelPackage.Workbook.Worksheets["TreeWinLoss"];
        productEvenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        productOddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        productTotalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        productSheet.Cells[cellRange].Value = title;
    }

    private void ClearTemplateForWinLossByProduct()
    {
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
    }

    private void BuildBodyRowForWinLossByProduct(IEnumerable<TreeWinLossItemByProductExcelModel> list, int startRowIndex)
    {
        foreach (var item in list)
        {
            var template = startRowIndex % 2 == 0 ? productEvenSheet : productOddSheet;
            productSheet.ApplyModel(template, item, startRowIndex);
            startRowIndex++;
        }
    }

    private void BuildTotalRowForWinLossByProduct(IEnumerable<TreeWinLossItemByProductExcelModel> list, int startRowIndex)
    {
        foreach (var item in list)
        {
            productSheet.ApplyModel(productTotalSheet, item, startRowIndex);
            productSheet.Cells[$"A{startRowIndex}:F{startRowIndex}"].Merge = true;
            startRowIndex++;
        }

        productSheet.Cells[$"A{startRowIndex - 4}:F{startRowIndex - 4}"].Value = string.Empty;
    }
}
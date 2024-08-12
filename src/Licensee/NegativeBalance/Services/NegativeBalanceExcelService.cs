using AutoMapper;
using Epplus.Mapper.Extensions;
using NegativeBalance.Criteria;
using NegativeBalance.Model;
using OfficeOpenXml;
using Utilities.Configurations;

namespace NegativeBalance.Services;

public interface NegativeBalanceExcelService
{
    Task<ExcelPackage> ExportExcelNegativeBalance(NegativeBalanceCriteria criteria);
}

public class NegativeBalanceExcelServiceImp(ConfigurationService configurationService,
    NegativeBalanceService service,
    IMapper mapper) : NegativeBalanceExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet licenseesSheet;

    private const int StartRowIndex = 4;

    private ExcelPackage ExcelPackage { get; set; }


    public async Task<ExcelPackage> ExportExcelNegativeBalance(NegativeBalanceCriteria criteria)
    {
        LoadTemplate("Licensee-NegativeBalance.xlsx");
        var isAllLicensees = criteria.Site == "All";
        var siteName = isAllLicensees ? "All Licensees" : criteria.Site;
        var title = $"Accounting system - Negative Balance Report for {siteName} ";
        BuildTitle(title, "A1:C1");
        var data = await service.GetNegativeBalance(criteria);
        BuildBody(data.NegativeBalanceUsers, StartRowIndex, isAllLicensees);
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/NegativeBalance/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["NegativeBalance"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        licenseesSheet = ExcelPackage.Workbook.Worksheets["LicenseesTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBody(IEnumerable<SiteNegativeBalanceModel> list, int startRowIndex, bool isAllLicensees)
    {
        var rowIndex = startRowIndex;
        if (isAllLicensees)
        {
            foreach (var item in list)
            {
                var licenseeSiteModel = new NegativeBalanceUserExcelModel
                {
                    UserName = item.LicenseeSite,
                    Balance = null
                };
                sheet.ApplyModel(licenseesSheet, licenseeSiteModel, rowIndex++);
                BuildBodyRow(item.NegativeBalanceUsers, ref rowIndex);
            }
        }
        else
        {
            var negativeBalanceUsers = list.FirstOrDefault()?.NegativeBalanceUsers;
            if (negativeBalanceUsers != null)
                BuildBodyRow(negativeBalanceUsers, ref rowIndex);
        }
        
    }

    private void BuildBodyRow(IEnumerable<NegativeBalanceUser> negativeBalanceUsers, ref int startRowIndex)
    {
        var excelModels = mapper.Map<IEnumerable<NegativeBalanceUserExcelModel>>(negativeBalanceUsers);
        foreach (var negativeBalanceUser in excelModels)
        {
            var template = startRowIndex % 2 == 0 ? evenSheet : oddSheet;
            sheet.ApplyModel(template, negativeBalanceUser, startRowIndex++);
        }
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("LicenseesTemplate");
    }
}
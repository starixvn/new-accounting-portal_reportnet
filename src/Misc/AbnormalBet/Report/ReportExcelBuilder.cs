namespace AbnormalBet.Report;

using Epplus.Mapper.Extensions;
using Model;
using OfficeOpenXml;
using Utilities.Configurations;

public interface ReportExcelBuilder
{
    ExcelPackage Build(string title, IEnumerable<LicenseeExcelModel> licenseeModel);

    ExcelPackage Build(string title, IEnumerable<MemberExcelModel> memberModel);
}
public class ReportExcelBuilderImp(ConfigurationService configurationService) : ReportExcelBuilder
{
    private ExcelWorksheet sheet;


    private const int StartRowIndex = 5;

    private ExcelPackage ExcelPackage { get; set; }

    public ExcelPackage Build(string title, IEnumerable<MemberExcelModel> memberModel)
    {
        LoadMemberTemplate();
        BuildTitle(title, "A1:M2");
        sheet.ApplyVertical(memberModel, StartRowIndex);
        ClearTemplate();
        return ExcelPackage;
    }

    public ExcelPackage Build(string title, IEnumerable<LicenseeExcelModel> licenseeModel)
    {
        LoadLicenseeTemplate();
        BuildTitle(title, "A1:S2");
        sheet.ApplyVertical(licenseeModel, StartRowIndex);
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadMemberTemplate()
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/AbNormalBet/Member.xlsx";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets.Copy("AbnormalBetTemplate", "Abnormal Bet Void");
    }

    private void LoadLicenseeTemplate()
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/AbNormalBet/Licensee.xlsx";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets.Copy("AbnormalBetTemplate", "Abnormal Bet Void");
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("AbnormalBetTemplate");
    }
}
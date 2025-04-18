namespace PeakConcurrentUsers.Services;

using Criteria;
using Epplus.Mapper.Extensions;
using Model;
using OfficeOpenXml;
using Utilities.Configurations;

public interface PeakConcurrentUserExcelService
{
    Task<ExcelPackage> ExportExcel(PeakConcurrentUserCriteria criteria);
}

public class PeakConcurrentUserExcelServiceImp(ConfigurationService configurationService,
    PeakConcurrentUserService service) : PeakConcurrentUserExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet totalSheet;

    private const int StartRowIndex = 4;

    private ExcelPackage ExcelPackage { get; set; }

    public async Task<ExcelPackage> ExportExcel(PeakConcurrentUserCriteria criteria)
    {
        LoadTemplate("PeakConcurrentUser.xlsx");
        var title = $"Peak Concurrent Users Report for {criteria.LicenseeName}";
        BuildTitle(title, "A1:B1");
        var data = await service.GetConcurrentUserList(criteria);
        BuildBody(data.ListConcurrentUser, StartRowIndex);
        var startIndexOfSummary = data.ListConcurrentUser.Count + StartRowIndex;
        BuildSummaryRows(data, startIndexOfSummary);
        ClearTemplate();
        return ExcelPackage;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/PeakConcurrentUser/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["PeakConcurrentUser"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBody(IEnumerable<ConcurrentUser> concurrentUsers, int startRowIndex)
    {
        foreach (var item in concurrentUsers)
        {
            var template = startRowIndex % 2 == 0 ? evenSheet : oddSheet;
            var model = new ConcurrentUserExcelModel
            {
                Date = item.Date,
                TotalUser = Convert.ToDecimal(item.TotalUser)
            };
            sheet.ApplyModel(template, model, startRowIndex++);
        }
    }

    private void BuildSummaryRows(PeakConcurrentUser data, int startRowIndex)
    {
        var totalPeakConcurrentUserModel = new ConcurrentUserExcelModel
        {
            Date = "Total Peak Concurrent Users",
            TotalUser = Convert.ToDecimal(data.Total)
        };
        sheet.ApplyModel(totalSheet, totalPeakConcurrentUserModel, startRowIndex++);
        var totalAveragePeakConcurrentUserModel = new ConcurrentUserExcelModel
        {
            Date = "Average Peak Concurrent Users",
            TotalUser = Convert.ToDecimal(data.AverageUsers)
        };
        sheet.ApplyModel(totalSheet, totalAveragePeakConcurrentUserModel, startRowIndex++);
        var maxPeakConcurrentUserModel = new ConcurrentUserExcelModel
        {
            Date = "Max Peak Concurrent Users",
            TotalUser = Convert.ToDecimal(data.MaxUsers)
        };
        sheet.ApplyModel(totalSheet, maxPeakConcurrentUserModel, startRowIndex++);
        var minPeakConcurrentUserModel = new ConcurrentUserExcelModel
        {
            Date = "Min Peak Concurrent Users",
            TotalUser = Convert.ToDecimal(data.MinUsers)
        };
        sheet.ApplyModel(totalSheet, minPeakConcurrentUserModel, startRowIndex);
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
    }
}
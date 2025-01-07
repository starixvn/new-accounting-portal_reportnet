using OfficeOpenXml;
using AutoMapper;
using Epplus.Mapper.Extensions;
using TotalSMAAndActiveMember.Model;
using Utilities.Configurations;
using TotalSMAAndActiveMember.Constant;

namespace TotalSMAAndActiveMember.Services;

public interface TotalSMAMemberActiveExcelService
{
    Task<ExcelPackage> ExportExcel(IList<SMASummaryModel> data, string title);

    Task<ExcelPackage> ExportExcelForDetail(List<GroupStatusDetailModel> data, int status);
}

public class TotalSMAMemberActiveExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : TotalSMAMemberActiveExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet oddSheet;

    private ExcelWorksheet evenSheet;

    private const int StartRowIndex = 5;

    private const int DetailRowIndex = 10;

    private const int TitleDetailRowIndex = 7;

    private const int CreatedTimeColumn = 8;

    private const int CreatorColumn = 9;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(IList<SMASummaryModel> data, string title)
    {
        LoadTemplate("Main.xlsx", false);
        BuildTitle(title, "A1:D2");
        BuildBodyRows(data, "A1:D2");
        return Task.FromResult(ExcelPackage);
    }

    public Task<ExcelPackage> ExportExcelForDetail(List<GroupStatusDetailModel> data, int status)
    {
        LoadTemplate("Detail.xlsx", true);
        var title = status switch
        {
            (int)StatusConstant.NewCreated => "New Created Details",
            (int)StatusConstant.Suspended => "Suspended Details",
            (int)StatusConstant.Closed => "Closed Details",
            _ => "N/A Details"
        };
        BuildTitle(title, "A1:I2");
        BuildDetailBodyRows(data);
        if (status != (int)StatusConstant.NewCreated)
        {
            sheet.DeleteColumn(CreatorColumn);
            sheet.DeleteColumn(CreatedTimeColumn);
        }
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel, bool isDetail)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TotalSMAAndMemberActiveReport/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Total SMA and Member Active Rep"];
        if (!isDetail) return;
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(IList<SMASummaryModel> data, string titleCellRange)
    {
        sheet.Cells[$"A{StartRowIndex}"].Value = $"{data[0].Super}/{data[0].Master}/{data[0].Agent}";
        sheet.Cells[$"B{StartRowIndex}"].Value = $"{data[1].Super}/{data[1].Master}/{data[1].Agent}";
        sheet.Cells[$"C{StartRowIndex}"].Value = $"{data[2].Super}/{data[2].Master}/{data[2].Agent}";
        sheet.Cells[$"D{StartRowIndex}"].Value = $"{data[3].Super}/{data[3].Master}/{data[3].Agent}";
        var length = data.Count;
        for (var i = 4; i < length; i++)
        {
            sheet.Cells["A10"].CopyStyles(sheet.Cells[DetailRowIndex, i - 3]);
            sheet.Cells[DetailRowIndex, i - 3].Value = data[i].Name;
            sheet.Cells["A11"].CopyStyles(sheet.Cells[DetailRowIndex + 1, i - 3]);
            sheet.Cells[DetailRowIndex + 1, i - 3].Value = $"{data[i].Super}/{data[i].Master}/{data[i].Agent}";
        }

        sheet.Cells[titleCellRange]
            .CopyStyles(sheet.Cells[TitleDetailRowIndex, 1, TitleDetailRowIndex + 1, length - 4]);
        sheet.Cells[TitleDetailRowIndex, 1, TitleDetailRowIndex + 1, length - 4].Merge = true;
        sheet.Cells[TitleDetailRowIndex, 1, TitleDetailRowIndex + 1, length - 4].Value =
            "Member Site (Active)";
    }

    private void BuildDetailBodyRows(List<GroupStatusDetailModel> data)
    {
        var index = 1;
        var rowIndex = StartRowIndex;
        foreach (var item in data)
        {
            var template = index++ % 2 == 0 ? evenSheet : oddSheet;
            foreach (var excelModel in item.ChildItems.Select(mapper.Map<StatusDetailExcelModel>))
            {
                sheet.ApplyModel(template, excelModel, rowIndex++);
            }

            if (item.ChildItems.Count > 1)
            {
                var mergedCell = $"B{rowIndex - item.ChildItems.Count}:B{rowIndex - 1}";
                sheet.Cells[mergedCell].Merge = true;
                sheet.Cells[mergedCell].Value = item.Name;
            }
            else
            {
                sheet.Cells[$"B{rowIndex - 1}"].Value = item.Name;
            }
        }
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
    }
}
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epplus.Mapper.Extensions;
using TotalSMAAndActiveMember.Model;
using Utilities.Configurations;

namespace TotalSMAAndActiveMember.Services;

public interface TotalSMAMemberActiveExcelService
{
    Task<ExcelPackage> ExportExcel(IList<SMASummaryModel> data, string title);
}

public class TotalSMAMemberActiveExcelServiceImp(ConfigurationService configurationService) : TotalSMAMemberActiveExcelService
{
    private ExcelWorksheet sheet;

    private const int SummaryRowIndex = 5;

    private const int DetailRowIndex = 10;

    private const int TitleDetailRowIndex = 7;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(IList<SMASummaryModel> data, string title)
    {
        LoadTemplate("TotalSMAAndMemberActiveReport.xlsx");
        BuildTitle(title, "A1:D2");
        BuildBodyRows(data, "A1:D2");
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/TotalSMAAndMemberActiveReport/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Total SMA and Member Active Report"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBodyRows(IList<SMASummaryModel> data, string titleCellRange)
    {
        sheet.Cells[$"A{SummaryRowIndex}"].Value = $"{data[0].Super}/{data[0].Master}/{data[0].Agent}";
        sheet.Cells[$"B{SummaryRowIndex}"].Value = $"{data[1].Super}/{data[1].Master}/{data[1].Agent}";
        sheet.Cells[$"C{SummaryRowIndex}"].Value = $"{data[2].Super}/{data[2].Master}/{data[2].Agent}";
        sheet.Cells[$"D{SummaryRowIndex}"].Value = $"{data[3].Super}/{data[3].Master}/{data[3].Agent}";
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
}
namespace DuplicateTickets.Services;

using System.Drawing;
using AutoMapper;
using Epplus.Mapper.Extensions;
using Model;
using OfficeOpenXml;
using Utilities.Configurations;

public interface DuplicateTicketsExcelService
{
    Task<ExcelPackage> ExportExcel(DuplicateTicketsModel data, string title);
}

public class DuplicateTicketsExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : DuplicateTicketsExcelService
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet titleSheet;

    private ExcelWorksheet evenSheet;

    private ExcelWorksheet oddSheet;

    private const int StartRowIndex = 4;

    private ExcelPackage ExcelPackage { get; set; }

    public Task<ExcelPackage> ExportExcel(DuplicateTicketsModel data, string title)
    {
        var excelData = mapper.Map<DuplicateTicketsExcelModel>(data);
        LoadTemplate("DuplicateTickets.xlsx");
        BuildTitle(title, "A1:O1");
        BuildBody(excelData.Rows, StartRowIndex);
        ClearTemplate();
        return Task.FromResult(ExcelPackage);
    }

    private void LoadTemplate(string templateName)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/DuplicateTickets/{templateName}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets["Duplicate Tickets"];
        titleSheet = ExcelPackage.Workbook.Worksheets["TitleTemplate"];
        evenSheet = ExcelPackage.Workbook.Worksheets["EvenTemplate"];
        oddSheet = ExcelPackage.Workbook.Worksheets["OddTemplate"];
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void BuildBody(List<DuplicateTicketsByUserNameExcelModel> data, int startRowIndex)
    {
        var rowIndex = startRowIndex;
            
        var groupIndex = 0;
        var ticketIndex = 0;

        foreach (var item in data)
        {
            var firstRowOfGroupIndex = rowIndex;

            if (item.TicketInfo.Any())
            {
                var originalTicketReferenceNumber = item.TicketInfo.FirstOrDefault()?.ReferenceNumber;

                var groupTitle = $"Duplicate Tickets of {originalTicketReferenceNumber}";
                var ticketCellRange = $"A{rowIndex}:O{rowIndex}";

                titleSheet.Cells["A4:O4"].CopyStyles(sheet.Cells[ticketCellRange]);
                sheet.Cells[ticketCellRange].Value = groupTitle;
                sheet.Cells[ticketCellRange].Merge = true;
                rowIndex++;

                var template = groupIndex % 2 == 0 ? evenSheet : oddSheet;

                foreach (var ticketItem in item.TicketInfo)
                {
                    item.No = ++ticketIndex;
                    sheet.ApplyModel(template, item, rowIndex);
                    sheet.ApplyModel(template, ticketItem, rowIndex++);
                    if (ticketItem.ReferenceNumber != originalTicketReferenceNumber) continue;
                    sheet.Cells[$"L{rowIndex - 1}"].Value = "-";
                    sheet.Cells[$"J{rowIndex - 1}"].Style.Font.Color.SetColor(Color.Black);
                }
                    
                sheet.AutoMergeRowsHaveSameValue($"B{firstRowOfGroupIndex}:B{rowIndex - 1}");
                sheet.AutoMergeRowsHaveSameValue($"C{firstRowOfGroupIndex}:C{rowIndex - 1}");
                sheet.AutoMergeRowsHaveSameValue($"D{firstRowOfGroupIndex}:D{rowIndex - 1}");
                sheet.AutoMergeRowsHaveSameValue($"E{firstRowOfGroupIndex}:E{rowIndex - 1}");
                sheet.AutoMergeRowsHaveSameValue($"F{firstRowOfGroupIndex}:F{rowIndex - 1}");
                sheet.AutoMergeRowsHaveSameValue($"G{firstRowOfGroupIndex}:G{rowIndex - 1}");
                sheet.AutoMergeRowsHaveSameValue($"H{firstRowOfGroupIndex}:H{rowIndex - 1}");
            }

            groupIndex++;
        }
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("TitleTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("OddTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("EvenTemplate");
    }
}
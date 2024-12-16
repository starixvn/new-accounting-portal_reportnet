using CashOutPerformance.Constants;
using CashOutPerformance.Models;
using CashOutPerformance.ViewModels;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using Utilities.Configurations;

namespace CashOutPerformance.Services;

public interface TicketsExcelBuilder<T>
{
    ExcelPackage Build(string title, string[] breadCrumb, TicketViewExcelModel<T> model, ExcelType excelType, bool IsShowFullColumn);

    ExcelPackage BuildSportLevel(string title, string[] breadCrumb, SportLevelExcelModel model, bool IsShowFullColumn);
}

public class TicketsExcelBuilderImp<T>(ConfigurationService configurationService) : TicketsExcelBuilder<T>
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet totalSheet;

    private ExcelWorksheet headerSheet;

    private const int StartRowIndex = 6;

    private ExcelPackage ExcelPackage { get; set; }

    private ExcelWorksheet templateSheet;

    public ExcelPackage Build(string title, string[] breadCrumb, TicketViewExcelModel<T> model, ExcelType excelType, bool IsShowFullColumn)
    {
        var fileExcel = "CashOutPerformance.xlsx";
        var cellAddressOfTitle = "A1:AH1";
        var cellAddressOfBreadCrumb = "A3:AH3";
        byte totalLength = 23;
        if (excelType != ExcelType.Match)
        {
            fileExcel = "SimpleCashOutPerformance.xlsx";
            cellAddressOfTitle = "A1:L1";
            cellAddressOfBreadCrumb = "A3:L3";
            totalLength = 1;
        }
        LoadTemplate(fileExcel);
        BuildTitle(title, cellAddressOfTitle);
        BuildBreadCrumb(breadCrumb, cellAddressOfBreadCrumb);
        RenameFirstColumn(excelType);
        sheet.ApplyVertical(model.TicketList, StartRowIndex);
        BuildTotalRow(model, totalLength);
        ClearTemplate();
        RemoveUsernameColumn(IsShowFullColumn);
        return ExcelPackage;
    }

    private void LoadTemplate(string fileExcel)
    {
        var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/CashOutPerformance/{fileExcel}";
        using var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read);
        ExcelPackage = new ExcelPackage(stream);
        sheet = ExcelPackage.Workbook.Worksheets.Copy("BodyTemplate", "Cash Out");
        templateSheet = ExcelPackage.Workbook.Worksheets["BodyTemplate"];
        totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
        headerSheet = ExcelPackage.Workbook.Worksheets["HeaderTemplate"];
    }

    private void BuildTotalRow(TicketViewExcelModel<T> model, byte totalLength)
    {
        var endRowIndex = StartRowIndex + model.Total;
        sheet.ApplyModel(totalSheet, model.TotalTicket, StartRowIndex + model.Total);
        var cellAddressOfTotalText = new ExcelAddressBase(endRowIndex, 1, endRowIndex, totalLength);
        sheet.Cells[cellAddressOfTotalText.Address].Value = "Grand Total";
        sheet.Cells[cellAddressOfTotalText.Address].Merge = true;
    }

    private void BuildTitle(string title, string cellRange)
    {
        sheet.Cells[cellRange].Value = title;
    }

    private void RenameFirstColumn(ExcelType excelType)
    {
        if (excelType != ExcelType.Match)
        {
            sheet.Cells["A5"].Value = Enum.GetName(typeof(ExcelType), excelType)?.Replace("Event", "Live/Non-Live");
        }
    }

    private void BuildBreadCrumb(string[] breadCrumb, string cellAddressOfBreadCrumb)
    {
        sheet.Cells[cellAddressOfBreadCrumb].Value = string.Join(" > ", breadCrumb);
    }

    private void ClearTemplate()
    {
        ExcelPackage.Workbook.Worksheets.Delete("BodyTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        ExcelPackage.Workbook.Worksheets.Delete("HeaderTemplate");
    }

    private void RemoveUsernameColumn(bool IsShowFullColumn)
    {
        const byte usernameColumnIndex = 2;
        if (!IsShowFullColumn)
        {
            sheet.DeleteColumn(usernameColumnIndex);
        }
    }

    public ExcelPackage BuildSportLevel(string title, string[] breadCrumb, SportLevelExcelModel model, bool IsShowFullColumn)
    {
        const string fileExcel = "SimpleCashOutPerformance.xlsx";
        const string cellAddressOfTitle = "A1:L1";
        const string cellAddressOfBreadCrumb = "A3:L3";
        var startRowIndex = 6;
        var updateRowIndex = new List<int>();
    
        LoadTemplate(fileExcel);
        BuildTitle(title, cellAddressOfTitle);
        BuildBreadCrumb(breadCrumb, cellAddressOfBreadCrumb);
            
        foreach(var sportItem in model.Sports)
        {
            updateRowIndex.Add(startRowIndex);
            sheet.ApplyModel(headerSheet, sportItem.SportTotal, startRowIndex);
            startRowIndex += 1;
            foreach(var childItem in sportItem.ChildItems)
            {
                sheet.ApplyModel(templateSheet, childItem, startRowIndex);
                startRowIndex += 1;
            }
        }

        sheet.ApplyModel(totalSheet,model.GrandTotal, startRowIndex);
        sheet.Cells[$"A{startRowIndex}"].Value = "Grand Total";
        sheet.Cells["A5"].Value = "Sport";
        UpdateValueColumnSport(updateRowIndex);
        ClearTemplate();
        RemoveUsernameColumn(IsShowFullColumn);
        RemoveSportColumn();
        return ExcelPackage;
    }

    private void UpdateValueColumnSport(List<int> updateRowIndex)
    {
        foreach(var item in updateRowIndex)
        {
            sheet.Cells[$"A{item}"].Value = sheet.Cells[$"M{item}"].Value;
        }
    }

    private void RemoveSportColumn()
    {
        const int sportColumnIndex = 13;
        sheet.DeleteColumn(sportColumnIndex);
    }
}
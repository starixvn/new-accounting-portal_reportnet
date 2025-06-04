namespace CashOutPerformance.Services;

using AutoMapper;
using Constants;
using Epplus.Mapper.Extensions;
using Models;
using OfficeOpenXml;
using Utilities.Configurations;
using ViewModels;

public interface TicketsExcelBuilder<T>
{
    ExcelPackage Build(string title, string[] breadCrumb, TicketViewExcelModel<T> model, ExcelType excelType, bool isShowFullColumn);

    ExcelPackage BuildSportLevel(string title, string[] breadCrumb, SportLevelExcelModel model, bool isShowFullColumn);

    ExcelPackage BuildBetTypeLevel(string title, string[] breadCrumb,
        TicketViewModel<BetTypeLevelModel> model, string firstColumnText, bool hasChildLevel);
}

public class TicketsExcelBuilderImp<T>(ConfigurationService configurationService, IMapper mapper) : TicketsExcelBuilder<T>
{
    private ExcelWorksheet sheet;

    private ExcelWorksheet totalSheet;

    private ExcelWorksheet headerSheet;

    private const int SportColumnIndex = 13;

    private ExcelPackage ExcelPackage { get; set; }

    private ExcelWorksheet templateSheet;

    public ExcelPackage Build(string title, string[] breadCrumb, TicketViewExcelModel<T> model, ExcelType excelType, bool isShowFullColumn)
    {
        var fileExcel = "CashOutPerformance.xlsx";
        var cellAddressOfTitle = "A1:AH1";
        var cellAddressOfBreadCrumb = "A3:AH3";
        byte totalLength = 24;
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
        sheet.ApplyVertical(model.TicketList, ExcelConstants.StartRowIndex);
        BuildTotalRow(model, totalLength);
        ClearTemplate();
        RemoveUsernameColumn(isShowFullColumn);
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
        var endRowIndex = ExcelConstants.StartRowIndex + model.Total;
        sheet.ApplyModel(totalSheet, model.TotalTicket, endRowIndex);
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

    private void RemoveUsernameColumn(bool isShowFullColumn)
    {
        const byte usernameColumnIndex = 2;
        if (!isShowFullColumn)
        {
            sheet.DeleteColumn(usernameColumnIndex);
        }
    }

    public ExcelPackage BuildSportLevel(string title, string[] breadCrumb, SportLevelExcelModel model, bool isShowFullColumn)
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
        RemoveUsernameColumn(isShowFullColumn);
        RemoveSportColumn();
        return ExcelPackage;
    }

    public ExcelPackage BuildBetTypeLevel(string title, string[] breadCrumb, TicketViewModel<BetTypeLevelModel> model, string firstColumnText, bool hasChildLevel)
    {
        var startRowIndex = ExcelConstants.StartRowIndex;

        LoadTemplate(ExcelConstants.BetTypeDrillDownExcelFile);
        BuildTitle(title, ExcelConstants.CellAddressOfBetTypeDrillDownTitle);
        BuildBreadCrumb(breadCrumb, ExcelConstants.CellAddressOfBetTypeDrillDownBreadCrumb);
        if (hasChildLevel)
        {
            BetTypeLevelExcelModel mapperModel;
            foreach (var item in model.TicketList)
            {
                mapperModel = mapper.Map<BetTypeLevelExcelModel>(item);
                sheet.ApplyModel(headerSheet, mapperModel, startRowIndex);
                startRowIndex += 1;
                foreach (var childItem in item.ChildItems)
                {
                    mapperModel = mapper.Map<BetTypeLevelExcelModel>(childItem);
                    mapperModel.BetTypeName = childItem.OutCome;
                    sheet.ApplyModel(templateSheet, mapperModel, startRowIndex);
                    startRowIndex += 1;
                }
            }
            mapperModel = mapper.Map<BetTypeLevelExcelModel>(model.TotalTicket);
            sheet.ApplyModel(totalSheet, mapperModel, startRowIndex);
            sheet.Cells[$"A{startRowIndex}"].Value = "Grand Total";

        }
        else
        {
            var excelTicketList = mapper.Map<IEnumerable<BetTypeLevelExcelModel>>(model.TicketList);
            sheet.ApplyVertical(excelTicketList, startRowIndex);
            var excelTotalTicket = mapper.Map<BetTypeLevelExcelModel>(model.TotalTicket);
            var totalRowIndex = startRowIndex + model.Total;
            sheet.ApplyModel(totalSheet, excelTotalTicket, totalRowIndex);
            sheet.Cells[$"A{totalRowIndex}"].Value = "Grand Total";
        }

        sheet.Cells["A5"].Value = firstColumnText;
        ClearTemplate();
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
        sheet.DeleteColumn(SportColumnIndex);
    }
}
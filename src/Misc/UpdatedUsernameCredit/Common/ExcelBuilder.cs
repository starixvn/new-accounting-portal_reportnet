namespace UpdatedUsernameCredit.Common;

using Epplus.Mapper.Extensions;
using OfficeOpenXml;

public interface ExcelBuilder
{
    ExcelPackage CreateExcelPackage<T>(
        IEnumerable<T> model,
        ExcelSetting setting)
        where T : class, new();
}

public class ExcelBuilderImp : ExcelBuilder
{
    private ExcelWorksheet statisticSheet;

    private int startRowIndex;

    public ExcelPackage CreateExcelPackage<T>(IEnumerable<T> model, ExcelSetting setting) where T : class, new()
    {
        var excelPackage = LoadTemplate(setting);
        ApplySetting();
        Build(model, setting);
        ClearTemplate(excelPackage);

        return excelPackage;
    }

    private ExcelPackage LoadTemplate(ExcelSetting settings)
    {
        using var stream = new FileStream(settings.TemplatePath, FileMode.Open, FileAccess.Read);
        var excelPackage = new ExcelPackage(stream);
        statisticSheet = excelPackage.Workbook.Worksheets.Copy("Template", settings.SheetName);

        return excelPackage;
    }

    private void ApplySetting()
    {
        statisticSheet.OutLineSummaryBelow = false;
    }

    private static void ClearTemplate(ExcelPackage excelPackage)
    {
        excelPackage.Workbook.Worksheets.Delete("Template");
    }

    private void Build<T>(IEnumerable<T> model, ExcelSetting setting) where T : class, new()
    {
        SetStartRowIndex(setting.StartRowIndex);
        SetSheetName(setting.SheetName);
        ApplyTitle(setting.Title, setting.TitleCellRange);
        ApplyBody(model);
    }

    private void SetStartRowIndex(int rowIndex)
    {
        startRowIndex = rowIndex;
    }

    private void SetSheetName(string sheetName)
    {
        statisticSheet.Name = sheetName;
    }

    private void ApplyTitle(string title, string titleCellRange)
    {
        statisticSheet.Cells[titleCellRange].Value = title;
    }

    private void ApplyBody<T>(IEnumerable<T> statistics) where T : class, new()
    {
        statisticSheet.ApplyVertical(statistics, startRowIndex);
    }
}
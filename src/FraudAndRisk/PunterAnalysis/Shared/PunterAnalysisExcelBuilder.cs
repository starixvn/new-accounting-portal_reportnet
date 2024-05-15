using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Shared
{
    public interface PunterAnalysisExcelBuilder
    {
        ExcelPackage CreateExcelPackage<T>(SummaryEntity<T> model, ExcelSetting settings) where T : class, new();
    }

    public class PunterAnalysisExcelBuilderImp : PunterAnalysisExcelBuilder
    {
        private ExcelWorksheet statisticSheet;

        private ExcelWorksheet totalTemplateSheet;

        private int startRowIndex;

        private int endRowIndex;

        public ExcelPackage CreateExcelPackage<T>(SummaryEntity<T> model, ExcelSetting settings) where T : class, new()
        {
            var excelPackage = LoadTemplate(settings.TemplatePath);
            ApplySetting();
            Build(model, settings);
            ClearTemplate(excelPackage);

            return excelPackage;
        }

        private ExcelPackage LoadTemplate(string templatePath)
        {
            using (var stream = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
            {
                var excelPackage = new ExcelPackage(stream);
                statisticSheet = excelPackage.Workbook.Worksheets.Copy("Template", "Statistic");
                totalTemplateSheet = excelPackage.Workbook.Worksheets["TotalTemplate"];

                return excelPackage;
            }
        }

        private void ApplySetting()
        {
            statisticSheet.OutLineSummaryBelow = false;
        }

        private void ClearTemplate(ExcelPackage excelPackage)
        {
            excelPackage.Workbook.Worksheets.Delete("Template");
            excelPackage.Workbook.Worksheets.Delete(totalTemplateSheet);
        }

        private void Build<T>(SummaryEntity<T> model, ExcelSetting setting) where T : class, new()
        {
            SetStartRowIndex(setting.StartRowIndex);
            SetEndRowIndex(setting.StartRowIndex, model.Entities.Count);
            SetSheetName(setting.SheetName);
            ApplyDescription(setting.Title, setting.TitleCellRange);
            ApplyDescription(setting.Filter, setting.FilterCellRange);
            ApplyBody(model.Entities);
            ApplyFooter(model.Total);
        }

        private void SetStartRowIndex(int rowIndex)
        {
            this.startRowIndex = rowIndex;
        }

        private void SetEndRowIndex(int rowIndex, int numberBodyItems)
        {
            endRowIndex = rowIndex + numberBodyItems;
        }

        private void SetSheetName(string sheetName)
        {
            statisticSheet.Name = sheetName;
        }

        private void ApplyDescription(string description, string descriptionCellRange)
        {
            statisticSheet.Cells[descriptionCellRange].Value = description;
        }

        private void ApplyBody<T>(IEnumerable<T> statistics) where T : class, new()
        {
            statisticSheet.ApplyVertical(statistics, startRowIndex);
        }

        private void ApplyFooter<T>(T total) where T : class, new()
        {
            statisticSheet.ApplyModel(totalTemplateSheet, total, endRowIndex);
            statisticSheet.Cells[$"A{endRowIndex}"].Value = "Total";
        }
    }
}
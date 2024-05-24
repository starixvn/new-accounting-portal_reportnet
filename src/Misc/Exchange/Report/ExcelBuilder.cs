using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using Utilities.Configurations;
using Utilities.LinqDynamicSum;

namespace Exchange.Report
{
    public interface ExcelBuilder
    {
        ExcelPackage Build(HttpParameterExcel excelParams, SummaryEntity<ExchangeExcelEntity> model);
    }

    public class ExcelBuilderImp : ExcelBuilder
    {

        private readonly ConfigurationService configurationService;

        private ExcelWorksheet exchangeSheet;

        private ExcelWorksheet totalSheet;

        private const int StartRowIndex = 5;

        private ExcelPackage ExcelPackage { get; set; }

        public ExcelBuilderImp(ConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public ExcelPackage Build(HttpParameterExcel excelParams, SummaryEntity<ExchangeExcelEntity> model)
        {
            LoadTemplate();
            BuilTitle(excelParams);
            exchangeSheet.ApplyVertical(model.Entities, StartRowIndex);
            exchangeSheet.ApplyModel(totalSheet, model.Total, StartRowIndex + model.Entities.Count);
            SetNameForFirstHeaderColumn(excelParams.DrillLevel);
            ClearTemplate();
            return ExcelPackage;
        }

        private void LoadTemplate()
        {
            var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/Exchange/Exchange.xlsx";
            using (var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read))
            {
                ExcelPackage = new ExcelPackage(stream);
                exchangeSheet = ExcelPackage.Workbook.Worksheets.Copy("ExchangeTemplate", "Exchange");
                totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
            }
        }

        private void BuilTitle(HttpParameterExcel excelParams)
        {
            var fromDate = $"{excelParams.FromDate: MMM dd, yyyy}";
            var toDate = $"{excelParams.ToDate: MMM dd, yyyy}";
            exchangeSheet.Cells["A1:N2"].Value = $"Exchange WinLoss From {fromDate} to {toDate}";
        }

        private void SetNameForFirstHeaderColumn(byte drillDownLevel)
        {
            string name;
            switch (drillDownLevel)
            {
                case 0:
                    name = "Licensee";
                    break;
                case 1:
                    name = "Sport";
                    break;
                default:
                    name = "Event";
                    break;
            }
            exchangeSheet.Cells["A3:A4"].Value = name;
        }

        private void ClearTemplate()
        {
            ExcelPackage.Workbook.Worksheets.Delete("ExchangeTemplate");
            ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        }
    }
}

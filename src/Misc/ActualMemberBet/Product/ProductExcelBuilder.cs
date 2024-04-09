using Epplus.Mapper.Extensions;
using OfficeOpenXml;

namespace ActualMemberBet.Product
{
    using Nap_Reports.Utilities.Configurations;
    using Nap_Reports.Utilities.LinqDynamicSum;

    public interface ProductExcelBuilder
    {
        ExcelPackage Build(SummaryEntity<ProductActualMemberBetExcel> productActualMemberBetExcel);
    }

    public class ProductExcelBuilderImp : ProductExcelBuilder
    {

        private readonly ConfigurationService configurationService;

        private ExcelWorksheet productActualMemberBetSheet;

        private ExcelWorksheet totalSheet;

        private const int StartRowIndex = 5;

        private ExcelPackage ExcelPackage { get; set; }

        public ProductExcelBuilderImp(ConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public ExcelPackage Build(SummaryEntity<ProductActualMemberBetExcel> productActualMemberBetExcel)
        {
            LoadTemplate();
            BuilTitle();
            productActualMemberBetSheet.ApplyVertical(productActualMemberBetExcel.Entities, StartRowIndex);
            productActualMemberBetSheet.ApplyModel(totalSheet, productActualMemberBetExcel.Total, StartRowIndex + productActualMemberBetExcel.Entities.Count);
            ClearTemplate();
            return ExcelPackage;
        }

        private void LoadTemplate()
        {
            var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}\\ExcelTemplate\\ActualMemberBet\\ProductActualMemberBet.xlsx";
            using (var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read))
            {
                ExcelPackage = new ExcelPackage(stream);
                productActualMemberBetSheet = ExcelPackage.Workbook.Worksheets.Copy("ActualMemberBetTemplate", "ActualMemberBet");
                totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
            }
        }

        private void BuilTitle()
        {
            productActualMemberBetSheet.Cells["A1:H1"].Value = "Main";
        }

        private void ClearTemplate()
        {
            ExcelPackage.Workbook.Worksheets.Delete("ActualMemberBetTemplate");
            ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        }
    }
}

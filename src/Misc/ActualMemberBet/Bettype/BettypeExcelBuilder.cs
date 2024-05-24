using Epplus.Mapper.Extensions;
using Utilities.Configurations;
using Utilities.LinqDynamicSum;
using OfficeOpenXml;

namespace ActualMemberBet.Bettype
{
    public interface BettypeExcelBuilder
    {
        ExcelPackage Build(HttpParameterExcel excelParams, SummaryEntity<BetTypeActualMemberBetExcel> bettypeActualMemberBetExcel);
    }

    public class BettypeExcelBuilderImp : BettypeExcelBuilder
    {

        private readonly ConfigurationService configurationService;

        private ExcelWorksheet bettypeActualMemberBetSheet;

        private ExcelWorksheet totalSheet;

        private const int StartRowIndex = 5;

        private ExcelPackage ExcelPackage { get; set; }

        public BettypeExcelBuilderImp(ConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public ExcelPackage Build(HttpParameterExcel excelParams, SummaryEntity<BetTypeActualMemberBetExcel> bettypeActualMemberBetExcel)
        {
            LoadTemplate();
            BuilTitle(excelParams);
            bettypeActualMemberBetSheet.ApplyVertical(bettypeActualMemberBetExcel.Entities, StartRowIndex);
            bettypeActualMemberBetSheet.ApplyModel(totalSheet, bettypeActualMemberBetExcel.Total, StartRowIndex + bettypeActualMemberBetExcel.Entities.Count);
            ClearTemplate();
            return ExcelPackage;
        }

        private void LoadTemplate()
        {
            var templateAbsolutelyPath = $"{configurationService.CurrentDirectory}/ExcelTemplate/ActualMemberBet/BettypeActualMemberBet.xlsx";
            using (var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read))
            {
                ExcelPackage = new ExcelPackage(stream);
                bettypeActualMemberBetSheet = ExcelPackage.Workbook.Worksheets.Copy("ActualMemberBetDetailTemplate", "ActualMemberBet");
                totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
            }
        }

        private void BuilTitle(HttpParameterExcel excelParams)
        {
            bettypeActualMemberBetSheet.Cells["A1:I1"].Value = $"Main > {excelParams.ProductName}";
        }

        private void ClearTemplate()
        {
            ExcelPackage.Workbook.Worksheets.Delete("ActualMemberBetDetailTemplate");
            ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        }
    }
}

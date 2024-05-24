using PunterAnalysis.Shared;
using Utilities.Configurations;

namespace PunterAnalysis.Summary
{
    public interface SummaryExcelSetting
    {
        ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
    }

    public class SummaryExcelSettingFactory : SummaryExcelSetting
    {
        private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/Summary.xlsx";

        private readonly ConfigurationService configurationService;

        public SummaryExcelSettingFactory(ConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams)
        {
            const int startDataRowTemplateIndex = 8;
            return new ExcelSetting
            {
                SheetName = "Punter Analysis",
                StartRowIndex = startDataRowTemplateIndex,
                TemplatePath = $"{configurationService.CurrentDirectory}/{TemplatePath}",
                Title = excelSettingParams.Title,
                TitleCellRange = "A1:E2",
                Filter = excelSettingParams.Filter,
                FilterCellRange = "A3:E5"
            };
        }
    }
}
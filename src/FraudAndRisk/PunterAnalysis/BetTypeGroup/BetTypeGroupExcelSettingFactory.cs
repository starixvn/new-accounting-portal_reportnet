using PunterAnalysis.Shared;
using Utilities.Configurations;

namespace PunterAnalysis.BetTypeGroup
{
    public interface BetTypeGroupExcelSetting
    {
        ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
    }

    public class BetTypeGroupExcelSettingFactory : BetTypeGroupExcelSetting
    {
        private const string TemplatePath = @"ExcelTemplate\PunterAnalysis\BetTypeGroup.xlsx";

        private readonly ConfigurationService configurationService;

        public BetTypeGroupExcelSettingFactory(ConfigurationService configurationService)
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
                TemplatePath = $"{configurationService.CurrentDirectory}\\{TemplatePath}",
                Title = excelSettingParams.Title,
                TitleCellRange = "A1:E2",
                Filter = excelSettingParams.Filter,
                FilterCellRange = "A3:E5"
            };
        }

        
    }
}
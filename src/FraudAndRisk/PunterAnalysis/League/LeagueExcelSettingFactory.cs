using PunterAnalysis.Shared;
using Utilities.Configurations;

namespace PunterAnalysis.League
{
    public interface LeagueExcelSetting
    {
        ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
    }

    public class LeagueExcelSettingFactory : LeagueExcelSetting
    {
        private const string TemplatePath = @"ExcelTemplate\PunterAnalysis\League.xlsx";

        private readonly ConfigurationService configurationService;

        public LeagueExcelSettingFactory(ConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams)
        {
            const int startDataRowTemplateIndex = 10;
            return new ExcelSetting
            {
                SheetName = "Punter Analysis",
                StartRowIndex = startDataRowTemplateIndex,
                TemplatePath = $"{configurationService.CurrentDirectory}\\{TemplatePath}",
                Title = excelSettingParams.Title,
                TitleCellRange = "A1:E2",
                Filter = excelSettingParams.Filter,
                FilterCellRange = "A3:E8"
            };
        }
    }
}
using PunterAnalysis.Shared;
using Utilities.Configurations;

namespace PunterAnalysis.Game
{
    public interface GameExcelSetting
    {
        ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
    }

    public class GameExcelSettingFactory(ConfigurationService configurationService) : GameExcelSetting
    {
        private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/Game.xlsx";

        public ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams)
        {
            const int startDataRowTemplateIndex = 10;
            return new ExcelSetting
            {
                SheetName = "Punter Analysis",
                StartRowIndex = startDataRowTemplateIndex,
                TemplatePath = $"{configurationService.CurrentDirectory}/{TemplatePath}",
                Title = excelSettingParams.Title,
                TitleCellRange = "A1:E2",
                Filter = excelSettingParams.Filter,
                FilterCellRange = "A3:E7"
            };
        }
    }
}
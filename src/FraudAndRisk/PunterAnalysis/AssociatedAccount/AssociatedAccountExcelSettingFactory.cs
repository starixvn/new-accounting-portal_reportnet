using PunterAnalysis.Shared;
using Utilities.Configurations;

namespace PunterAnalysis.AssociatedAccount
{
    public interface AssociatedAccountExcelSetting
    {
        ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
    }

    public class AssociatedAccountExcelSettingFactory : AssociatedAccountExcelSetting
    {
        private const string TemplatePath = @"ExcelTemplate\PunterAnalysis\AssociatedAccount.xlsx";

        private readonly ConfigurationService configurationService;

        public AssociatedAccountExcelSettingFactory(ConfigurationService configurationService)
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
                TitleCellRange = "A1:F2",
                Filter = excelSettingParams.Filter,
                FilterCellRange = "A3:F5"
            };
        }
    }

}

namespace PunterAnalysis.AssociatedAccount;

using Shared;
using Utilities.Configurations;

public interface AssociatedAccountExcelSetting
{
    ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
}

public class AssociatedAccountExcelSettingFactory(ConfigurationService configurationService)
    : AssociatedAccountExcelSetting
{
    private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/AssociatedAccount.xlsx";

    public ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams)
    {
        const int startDataRowTemplateIndex = 8;
        return new ExcelSetting
        {
            SheetName = "Punter Analysis",
            StartRowIndex = startDataRowTemplateIndex,
            TemplatePath = $"{configurationService.CurrentDirectory}/{TemplatePath}",
            Title = excelSettingParams.Title,
            TitleCellRange = "A1:F2",
            Filter = excelSettingParams.Filter,
            FilterCellRange = "A3:F5"
        };
    }
}
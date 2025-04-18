namespace PunterAnalysis.BetTypeGroup;

using Shared;
using Utilities.Configurations;

public interface BetTypeGroupExcelSetting
{
    ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
}

public class BetTypeGroupExcelSettingFactory(ConfigurationService configurationService) : BetTypeGroupExcelSetting
{
    private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/BetTypeGroup.xlsx";

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
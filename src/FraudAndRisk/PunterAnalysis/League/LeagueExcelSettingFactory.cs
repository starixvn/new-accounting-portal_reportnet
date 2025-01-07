using PunterAnalysis.Shared;
using Utilities.Configurations;

namespace PunterAnalysis.League;

public interface LeagueExcelSetting
{
    ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
}

public class LeagueExcelSettingFactory(ConfigurationService configurationService) : LeagueExcelSetting
{
    private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/League.xlsx";

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
            FilterCellRange = "A3:E8"
        };
    }
}
using PunterAnalysis.Shared;
using Utilities.Configurations;

namespace PunterAnalysis.SportType;

public interface SportTypeExcelSetting
{
    ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
}

public class SportTypeExcelSettingFactory(ConfigurationService configurationService) : SportTypeExcelSetting
{
    private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/Sporttype.xlsx";

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
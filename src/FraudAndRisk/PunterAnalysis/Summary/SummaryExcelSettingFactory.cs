namespace PunterAnalysis.Summary;

using Shared;
using Utilities.Configurations;

public interface SummaryExcelSetting
{
    ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
}

public class SummaryExcelSettingFactory(ConfigurationService configurationService) : SummaryExcelSetting
{
    private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/Summary.xlsx";

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
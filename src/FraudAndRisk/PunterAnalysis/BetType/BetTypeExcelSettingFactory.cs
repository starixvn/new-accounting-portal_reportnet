namespace PunterAnalysis.BetType;

using Shared;
using Utilities.Configurations;

public interface BetTypeExcelSetting
{
    ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
}

public class BetTypeExcelSettingFactory(ConfigurationService configurationService) : BetTypeExcelSetting
{
    private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/Bettype.xlsx";

    public ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams)
    {
        return new ExcelSetting
        {
            SheetName = "Punter Analysis",
            StartRowIndex = 9,
            TemplatePath = $"{configurationService.CurrentDirectory}/{TemplatePath}",
            Title = excelSettingParams.Title,
            TitleCellRange = "A1:M2",
            Filter = excelSettingParams.Filter,
            FilterCellRange = "A3:M5"
        };
    }
}
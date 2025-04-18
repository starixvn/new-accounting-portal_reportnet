namespace PunterAnalysis.GameBetType;

using Shared;
using Utilities.Configurations;

public interface GameBetTypeExcelSetting
{
    ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams);
}

public class GameBetTypeExcelSettingFactory(ConfigurationService configurationService) : GameBetTypeExcelSetting
{
    private const string TemplatePath = @"ExcelTemplate/PunterAnalysis/GameBettype.xlsx";

    public ExcelSetting CreateSetting(ExcelSettingParams excelSettingParams)
    {
        return new ExcelSetting
        {
            SheetName = "Punter Analysis",
            StartRowIndex = 12,
            TemplatePath = $"{configurationService.CurrentDirectory}/{TemplatePath}",
            Title = excelSettingParams.Title,
            TitleCellRange = "A1:M2",
            Filter = excelSettingParams.Filter,
            FilterCellRange = "A3:M8"
        };
    }
}
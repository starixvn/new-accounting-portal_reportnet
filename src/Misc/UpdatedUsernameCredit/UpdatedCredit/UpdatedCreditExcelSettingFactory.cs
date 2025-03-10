namespace UpdatedUsernameCredit.UpdatedCredit;

using Common;
using Utilities.Configurations;

public interface UpdatedCreditExcelSetting
{
    ExcelSetting CreateSetting(BaseMessage message);
}

public class UpdatedCreditExcelSettingImp(ConfigurationService configurationService) : UpdatedCreditExcelSetting
{
    private const string TemplatePath = @"ExcelTemplate/UpdatedUsernameAndCredit/UpdatedCredit.xlsx";

    public ExcelSetting CreateSetting(BaseMessage message)
    {
        const int startDataRowTemplateIndex = 5;
        return new ExcelSetting
        {
            SheetName = "Updated Credit",
            StartRowIndex = startDataRowTemplateIndex,
            TemplatePath = $"{configurationService.CurrentDirectory}/{TemplatePath}",
            Title = $"Updated Credit report From {message.FromDate:MMM dd, yyyy} To {message.ToDate:MMM dd, yyyy}",
            TitleCellRange = "A1:O2"
        };
    }
}
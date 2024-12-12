using UpdatedUsernameCredit.Common;
using Utilities.Configurations;

namespace UpdatedUsernameCredit.UpdatedUsername
{
    public interface UpdatedUsernameExcelSetting
    {
        ExcelSetting CreateSetting(BaseMessage message);
    }

    public class UpdatedUsernameExcelSettingImp(ConfigurationService configurationService) : UpdatedUsernameExcelSetting
    {
        private const string TemplatePath = @"ExcelTemplate/UpdatedUsernameAndCredit/UpdatedUsername.xlsx";

        public ExcelSetting CreateSetting(BaseMessage message)
        {
            return new ExcelSetting
            {
                SheetName = "Updated Username",
                StartRowIndex = 5,
                TemplatePath = $"{configurationService.CurrentDirectory}/{TemplatePath}",
                Title = $"Updated Username report From {message.FromDate:MMM dd, yyyy} To {message.ToDate:MMM dd, yyyy}",
                TitleCellRange = "A1:N2"
            };
        }
    }
}
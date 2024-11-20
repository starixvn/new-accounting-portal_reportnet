using UpdatedUsernameCredit.Common;
using UpdatedUsernameCredit.UpdatedCredit;
using UpdatedUsernameCredit.UpdatedUsername;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class UpdatedUsernameAndCreditInjection
    {
        public static void RegisterUpdatedUsernameAndCreditServices(this IServiceCollection service)
        {
            service.AddTransient<UpdatedUsernameService, UpdatedUsernameServiceImp>();
            service.AddTransient<UpdatedCreditService, UpdatedCreditServiceImp>();
            service.AddTransient<LogParseService, LogParseServiceImp>();
            service.AddTransient<ExcelBuilder, ExcelBuilderImp>();
            service.AddTransient<UpdatedCreditExcelSetting, UpdatedCreditExcelSettingImp>();
            service.AddTransient<UpdatedUsernameExcelSetting, UpdatedUsernameExcelSettingImp>();
        }
    }
}

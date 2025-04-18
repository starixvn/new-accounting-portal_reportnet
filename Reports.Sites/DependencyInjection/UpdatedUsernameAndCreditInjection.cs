namespace Reports.Sites.DependencyInjection;

using UpdatedUsernameCredit.Common;
using UpdatedUsernameCredit.UpdatedCredit;
using UpdatedUsernameCredit.UpdatedUsername;

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
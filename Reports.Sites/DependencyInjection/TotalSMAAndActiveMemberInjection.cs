namespace Reports.Sites.DependencyInjection;

using TotalSMAAndActiveMember.Services;

public static class TotalSMAAndActiveMemberInjection
{
    public static void RegisterTotalSMAAndActiveMemberServices(this IServiceCollection service)
    {
        service.AddTransient<TotalSMAMemberActiveService, TotalSMAMemberActiveServiceImp>();
        service.AddTransient<TotalSMAMemberActiveExcelService, TotalSMAMemberActiveExcelServiceImp>();
    }
}
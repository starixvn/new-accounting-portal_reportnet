using TotalSMAAndActiveMember.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class TotalSMAAndActiveMemberInjection
    {
        public static void RegisterTotalSMAAndActiveMemberServices(this IServiceCollection service)
        {
            service.AddTransient<TotalSMAMemberActiveService, TotalSMAMemberActiveServiceImp>();
        }
    }
}

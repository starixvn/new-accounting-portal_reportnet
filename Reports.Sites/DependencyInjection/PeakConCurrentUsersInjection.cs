using PeakConcurrentUsers.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class PeakConCurrentUsersInjection
    {
        public static void RegisterPeakConCurrentUsersServices(this IServiceCollection service)
        {
            service.AddTransient<PeakConcurrentUserService, PeakConcurrentUserServiceImp>();
            service.AddTransient<PeakConcurrentUserExcelService, PeakConcurrentUserExcelServiceImp>();
        }
    }
}

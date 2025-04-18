namespace Reports.Sites.DependencyInjection;

using PeakConcurrentUsers.Services;

public static class PeakConCurrentUsersInjection
{
    public static void RegisterPeakConCurrentUsersServices(this IServiceCollection service)
    {
        service.AddTransient<PeakConcurrentUserService, PeakConcurrentUserServiceImp>();
        service.AddTransient<PeakConcurrentUserExcelService, PeakConcurrentUserExcelServiceImp>();
    }
}
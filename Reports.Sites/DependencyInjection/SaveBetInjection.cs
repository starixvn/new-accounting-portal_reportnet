using SaveBetReport.Services;

namespace Reports.Sites.DependencyInjection;

public static class SaveBetInjection
{
    public static void RegisterSaveBetServices(this IServiceCollection service)
    {
        service.AddTransient<SaveBetReportService, SaveBetReportServiceImp>();
    }
}
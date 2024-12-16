using SaveBetReport.Services;

namespace Nap_Reports.Sites.DependencyInjection;

public static class SaveBetInjection
{
    public static void RegisterSaveBetServices(this IServiceCollection service)
    {
        service.AddTransient<SaveBetReportService, SaveBetReportServiceImp>();
    }
}
namespace Reports.Sites.DependencyInjection;

using SaveBetReport.Services;

public static class SaveBetInjection
{
    public static void RegisterSaveBetServices(this IServiceCollection service)
    {
        service.AddTransient<SaveBetReportService, SaveBetReportServiceImp>();
    }
}
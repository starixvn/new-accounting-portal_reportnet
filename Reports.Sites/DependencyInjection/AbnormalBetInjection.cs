namespace Reports.Sites.DependencyInjection;

using AbnormalBet.Filter;
using AbnormalBet.IpLocation;
using AbnormalBet.Report;

public static class AbnormalBetInjection
{
    public static void RegisterAbnormalBetServices(this IServiceCollection service)
    {
        service.AddTransient<FilterService, FilterServiceImp>();
        service.AddTransient<ReportService, ReportServiceImp>();
        service.AddTransient<ReportExcelBuilder, ReportExcelBuilderImp>();
        service.AddTransient<LocationService, LocationServiceImp>();
    }
}
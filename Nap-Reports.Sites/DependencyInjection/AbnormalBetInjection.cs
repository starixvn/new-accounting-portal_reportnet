using AbnormalBet.Filter;
using AbnormalBet.IpLocation;
using AbnormalBet.Report;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class AbnormalBetInjection
    {
        public static void RegisterAbnormalbetServices(this IServiceCollection service)
        {
            service.AddTransient<FilterService, FilterServiceImp>();
            service.AddTransient<ReportService, ReportServiceImp>();
            service.AddTransient<ReportExcelBuilder, ReportExcelBuilderImp>();
            service.AddTransient<LocationService, LocationServiceImp>();
        }
    }
}

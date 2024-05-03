using SBCustomerPerformanceByDanger.Filter;
using SBCustomerPerformanceByDanger.Report;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class SBCustomerPerformanceByDangerInjection
    {
        public static void RegisterSBCustomerPerformanceByDangerServices(this IServiceCollection service)
        {
            service.AddTransient<FilterService, FilterServiceImp>();
            service.AddTransient<ReportService, ReportServiceImp>();
            service.AddTransient<ReportExcelBuilder, ReportExcelBuilderImp>();
        }
    }
}


using CompanyWinLoss.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class CompanyWinLossInjection
    {
        public static void RegisterCompanyWinLossServices(this IServiceCollection service)
        {
            service.AddTransient<CompanyWinLossService, CompanyWinLossServiceImp>();
        }
    }
}

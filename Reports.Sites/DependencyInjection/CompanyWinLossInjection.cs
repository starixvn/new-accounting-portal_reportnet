
namespace Reports.Sites.DependencyInjection;

using CompanyWinLoss.Services;

public static class CompanyWinLossInjection
{
    public static void RegisterCompanyWinLossServices(this IServiceCollection service)
    {
        service.AddTransient<CompanyWinLossService, CompanyWinLossServiceImp>();
    }
}
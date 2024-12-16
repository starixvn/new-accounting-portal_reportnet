
using CompanyBalanceDifference.Services;

namespace Reports.Sites.DependencyInjection;

public static class CompanyBalanceDifferenceInjection
{
    public static void RegisterCompanyBalanceDifferenceServices(this IServiceCollection service)
    {
        service.AddTransient<CompanyBalanceDifferenceService, CompanyBalanceDifferenceServiceImp>();
    }
}
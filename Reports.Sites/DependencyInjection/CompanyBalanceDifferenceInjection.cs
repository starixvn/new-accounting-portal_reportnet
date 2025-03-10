
namespace Reports.Sites.DependencyInjection;

using CompanyBalanceDifference.Services;

public static class CompanyBalanceDifferenceInjection
{
    public static void RegisterCompanyBalanceDifferenceServices(this IServiceCollection service)
    {
        service.AddTransient<CompanyBalanceDifferenceService, CompanyBalanceDifferenceServiceImp>();
    }
}
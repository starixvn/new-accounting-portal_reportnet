namespace Reports.Sites.DependencyInjection;

using Exchange.Report;

public static class ExchangeInjection
{
    public static void RegisterExchangeServices(this IServiceCollection service)
    {
        service.AddTransient<Service, ServiceImp>();
        service.AddTransient<ExcelBuilder, ExcelBuilderImp>();
        service.AddTransient<LicenseeExcelBuilder, LicenseeExcelBuilderImp>();
    }
}
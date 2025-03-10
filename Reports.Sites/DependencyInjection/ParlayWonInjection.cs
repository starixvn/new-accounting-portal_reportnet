namespace Reports.Sites.DependencyInjection;

using ParlayWon.Services;

public static class ParlayWonInjection
{
    public static void RegisterParlayWonServices(this IServiceCollection service)
    {
        service.AddTransient<ParlayWonService, ParlayWonServiceImp>();
        service.AddTransient<ParlayWonExportExcelService, ParlayWonExportExcelServiceImp>();
    }
}
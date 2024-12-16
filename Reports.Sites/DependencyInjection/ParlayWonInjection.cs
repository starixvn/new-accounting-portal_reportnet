using ParlayWon.Services;

namespace Reports.Sites.DependencyInjection;

public static class ParlayWonInjection
{
    public static void RegisterParlayWonServices(this IServiceCollection service)
    {
        service.AddTransient<ParlayWonService, ParlayWonServiceImp>();
        service.AddTransient<ParlayWonExportExcelService, ParlayWonExportExcelServiceImp>();
    }
}
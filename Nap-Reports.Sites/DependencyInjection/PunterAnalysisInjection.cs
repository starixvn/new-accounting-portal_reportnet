using Olympus.FPSAPI.Client;
using PunterAnalysis.AssociatedAccount;
using PunterAnalysis.BetCount;
using PunterAnalysis.BetType;
using PunterAnalysis.BetTypeGroup;
using PunterAnalysis.Game;
using PunterAnalysis.GameBetType;
using PunterAnalysis.League;
using PunterAnalysis.Shared;
using PunterAnalysis.SportType;
using PunterAnalysis.Summary;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class PunterAnalysisInjection
    {
        public static void RegisterPunterAnalysisServices(this IServiceCollection service)
        {
            service.AddTransient<PunterAnalysisService, PunterAnalysisServiceImp>();
            service.AddTransient<DateTimeService, DateTimeServiceImp>();
            service.AddTransient<LeagueServiceFactory, LeagueServiceFactoryImp>();
            service.AddTransient<FraudServiceWrapper, FraudServiceWrapperImp>();
            service.AddTransient<FraudServiceWrapperNew, FraudServiceWrapperNewImp>();
            service.AddTransient<PunterAnalysisExcelBuilder, PunterAnalysisExcelBuilderImp>();
            service.AddTransient<BetTypeExcelSetting, BetTypeExcelSettingFactory>();
            service.AddTransient<SportTypeExcelSetting, SportTypeExcelSettingFactory>();
            service.AddTransient<BetCountExcelSetting, BetCountExcelSettingFactory>();
            service.AddTransient<SummaryExcelSetting, SummaryExcelSettingFactory>();
            service.AddTransient<LeagueExcelSetting, LeagueExcelSettingFactory>();
            service.AddTransient<AssociatedAccountExcelSetting, AssociatedAccountExcelSettingFactory>();
            service.AddTransient<LeagueExcelBuilder, LeagueExcelBuilderImp>();
            service.AddTransient<BetCountService, BetCountServiceImp>();
            service.AddTransient<AssociatedFacade, AssociatedFacadeImp>();
            service.AddTransient<GameExcelSetting, GameExcelSettingFactory>();
            service.AddTransient<GameBetTypeExcelSetting, GameBetTypeExcelSettingFactory>();
            service.AddTransient<BetTypeGroupExcelSetting, BetTypeGroupExcelSettingFactory>();
            service.AddTransient<SummaryService, SummaryServiceImp>();
        }
    }
}

using Fanex.Caching;
using Fanex.Caching.Memory;
using Fanex.Data.Repository;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.Client.Services;
using Fanex.NAP.Client.Session;
using Fanex.RemotingRef;
using Nap_Reports.Utilities.BalanceUpServices;
using Nap_Reports.Utilities.Configurations;
using Nap_Reports.Utilities.PermissionFilter;
using SBPerformance;
using SMAWinLoss;
using System.Reflection;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class GlobalInjection
    {
        public static void RegisterGlobalServices(this IServiceCollection service)
        {
            service.AddTransient<IRemotingRefService, RemotingRefService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IGroupService, GroupService>();
            service.AddTransient<ISiteService, SiteService>();
            service.AddTransient<ISecurityService, SecurityService>();
            service.AddTransient<INapSession, NapSession>();
            service.AddTransient<INapSettings, NapSettings>();
            service.AddSingleton<ICache>(_ => new MemoryCache(new MemoryCacheOptions()));

            service.AddTransient<IDynamicRepository, DynamicRepository>();
            service.AddSingleton<ICacheService>(new CacheService());

            service.AddTransient<PermissionFilterService, PermissionFilterServiceImp>();
            service.AddTransient<BalanceUpService, BalanceUpServiceImp>();
            service.AddTransient<ConfigurationService, ConfigurationServiceImp>();

            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SMAWinLossAttribute).GetTypeInfo().Assembly));
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SBPerformanceAttribute).GetTypeInfo().Assembly));
        }
    }
}

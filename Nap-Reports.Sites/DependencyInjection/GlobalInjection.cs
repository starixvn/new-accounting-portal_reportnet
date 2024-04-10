using Fanex.Caching;
using Fanex.Caching.Memory;
using Fanex.Data.Repository;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.Client.Services;
using Fanex.NAP.Client.Session;
using Fanex.RemotingRef;
using Utilities.BalanceUpServices;
using Utilities.Configurations;
using Utilities.PermissionFilter;

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
        }
    }
}

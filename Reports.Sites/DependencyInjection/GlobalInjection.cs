namespace Reports.Sites.DependencyInjection;

using Fanex.Caching;
using Fanex.Caching.Memory;
using Fanex.Data.Repository;
using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Services;
using Fanex.RemotingRef;
using Utilities.BalanceUpServices;
using Utilities.Configurations;
using Utilities.Currencies;
using Utilities.CustomerTrackingSystem;
using Utilities.PermissionFilter;

public static class GlobalInjection
{
    public static void AddGlobalServices(this IServiceCollection service)
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
        service.AddTransient<CustomerTrackingService, CustomerTrackingServiceImp>();
        service.AddTransient<CurrenciesService, CurrenciesServiceImp>();
    }
}
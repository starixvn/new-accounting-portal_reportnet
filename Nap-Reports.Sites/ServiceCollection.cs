using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.RemotingRef;
using Fanex.Result.Core.Base;
using Fanex.Result.Core.RemotingRef;
using Microsoft.AspNetCore.Authentication.Cookies;
using Nap_Reports.Sites.DependencyInjection;
using SharpRaven;

namespace Nap_Reports.Sites
{
    public static class ServiceCollection
    {
        public static void RegisterSentry(this IServiceCollection services, IConfiguration configuration)
        {
            LogManager
                .SetDefaultLogCategory(configuration.GetSection("CategorySentryLog").Value)
                .Use(new List<ILogEngine>
                {
                new SentryLogging(new SentryEngineOptions
                {
                    Dsn = new Dsn(configuration.GetSection("SentryClientKey").Value)
                })
                });
        }

        public static void RegisterRemotingRef(this IServiceCollection services, IConfiguration configuration)
        {
            RefDataTypeContainer.ConnectTo(new Uri(configuration["Fanex.RemotingRef.Service.Url"]))
                .FromApplication("Nap-Reports");
        }

        public static void RegisterResultCore(this IServiceCollection services, IConfiguration configuration)
        {
            ResultCoreManager.RegisterRefDataHelper(new RefDataHelper());
        }

        public static void RegisterInternalServices(this IServiceCollection services)
        {
            services.RegisterHouseWinLossServices();
            services.RegisterSMAWinLossServices();
            services.RegisterSBPerformanceServices();
            services.RegisterAbnormalbetServices();
            services.RegisterActualMemberBetServices();
            services.RegisterExchangeServices();
            services.RegisterCashOutPerformanceServices();
            services.RegisterUpdatedUsernameAndCreditServices();
            services.RegisterSBCustomerPerformanceByDangerServices();
            services.RegisterPunterAnalysisServices();
            services.RegisterBetListServices();
        }

        public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = configuration.GetSection("AuthSettings:LoginPath").Value;
                    options.LogoutPath = configuration.GetSection("AuthSettings:LogoutPath").Value;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("AuthSettings:CookieTimeOut").Value));
                    options.Cookie = new CookieBuilder
                    {
                        Name = "Nap-Reports.Cookie",
                        SecurePolicy = CookieSecurePolicy.SameAsRequest
                    };

                    options.Events.OnSigningOut = context =>
                    {
                        context.Response.Redirect(options.LoginPath);
                        return Task.CompletedTask;
                    };
                });

            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("AuthSettings:SessionTimeOut").Value));
                options.Cookie.Name = "Nap-Reports.Session";
            });
        }
    }
}

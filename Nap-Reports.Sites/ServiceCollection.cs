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
            services.RegisterDailyMixParlayServices();
            services.RegisterResultAnalysisServices();
            services.RegisterLegendManagementServices();
            services.RegisterTreeWinLossServices();
            services.RegisterNegativeBalanceServices();
            services.RegisterPeakConCurrentUsersServices();
            services.RegisterCompanyWinLossServices();
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

        public static void ProcessWebOptimizer(this IServiceCollection services)
        {
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddCssBundle("/style/fanex.result.css", 
                    "/Content/Fanex.Result/fanex.result.css", 
                    "/Content/Fanex.Result/fanex.result-customized.css");
                
                pipeline.AddCssBundle("/style/tipsy.css",
                   "/scripts/_lib/Tooltip/tipsy.css");

                pipeline.AddCssBundle("/style/jquery-custom.css",
                   "/themes/base/jquery/jquery-ui-1.9.1.custom.css");

                pipeline.AddCssBundle("/style/reset.css",
                  "/themes/reset.css");

                pipeline.AddCssBundle("/style/scrollbars.css",
               "/scripts/_lib/CustomScrollbar/Styles/scrollbars.css");

                pipeline.AddCssBundle("/style/fanex-grid.css",
                "/scripts/_lib/Fanex.Grid/Fanex.Grid.css");

                pipeline.AddCssBundle("/style/fanex-grid-paging.css",
                 "/scripts/_lib/Fanex.Grid/Fanex.Pagination.css");

                pipeline.AddCssBundle("/style/msgbox.css",
                "/scripts/_lib/msgbox/msgbox-0.0.1.css");

                pipeline.AddCssBundle("/style/jquery-multiselect.css",
                "/scripts/_lib/MultipleSelect/jquery.multiselect.css");

                pipeline.AddCssBundle("/style/nex-date-picker.css",
                "/scripts/_lib/DatePicker/jquery.nexDatePicker.css");

                pipeline.AddCssBundle("/style/month-picker.css",
               "/scripts/_lib/MonthPicker/monPickr-0.0.3.css");
                
                pipeline.AddLessBundle("/style/month-picker-custom.css",
                "/scripts/_lib/MonthPicker/monPickr-0.0.3-custom.less");

                pipeline.AddJavaScriptBundle("/scripts/nap-report-common.js",
                    "/scripts/_lib/jquery-1.8.2.js",
                    "/scripts/_lib/jquery-ui-1.9.1.js",
                    "/scripts/_lib/BlockUI/jquery.blockUI.1.33.js",
                    "/scripts/_lib/FormatLibs/NumberFormat/numeral.js",
                    "/scripts/_lib/Fanex.Grid/Fanex.Grid.js",
                    "/scripts/_lib/Fanex.Grid/Fanex.Pagination.js",
                    "/scripts/_lib/Tooltip/jquery.tipsy.js",
                    "/scripts/NapAccReports.js");

                pipeline.AddJavaScriptBundle("/scripts/msg-box.js",
                   "/scripts/_lib/msgbox/msgbox-0.0.1.js");

                pipeline.AddJavaScriptBundle("/scripts/jquery-multiselect.js",
                  "/scripts/_lib/MultipleSelect/jquery.multiselect.js");

                pipeline.AddJavaScriptBundle("/scripts/nex-date-picker.js",
                 "/scripts/_lib/DatePicker/jquery.nexDatePicker.js");

                pipeline.AddJavaScriptBundle("/scripts/do-t.js",
                "/scripts/_lib/TemplateRender/doT/doT.js");

                pipeline.AddJavaScriptBundle("/scripts/month-picker.js",
                "/scripts/_lib/MonthPicker/monPickr-0.0.3.js");

                pipeline.AddLessBundle(
                    "/daily-mix-parlay-winloss/styles/DailyMixParlayWinLoss.css", 
                    "/daily-mix-parlay-winloss/styles/DailyMixParlayWinLoss.less");
                pipeline.AddLessBundle(
                    "/daily-mix-parlay-2hlink/styles/DailyMixParlayWinLoss.css",
                    "/daily-mix-parlay-2hlink/styles/DailyMixParlayWinLoss.less");
                pipeline.AddLessBundle(
                    "/styles/month-picker-custom.css",
                    "/scripts/_lib/MonthPicker/monPickr-0.0.3-custom.less");
                pipeline.AddLessBundle(
                   "/themes/base/style.css",
                   "/themes/base/style.less");

                pipeline.AddJavaScriptBundle("/scripts/jquery.fileDownload.js",
                    "/scripts/_lib/jquery.fileDownload.js");

                pipeline.AddLessBundle(
                   "/tree-win-loss/styles/TreeWinLoss.css",
                   "/tree-win-loss/styles/TreeWinLoss.less");

                pipeline.AddLessBundle(
                  "/negative-balance/styles/negative-balance.css",
                  "/negative-balance/styles/NegativeBalance.less");

                pipeline.AddLessBundle(
                  "/peak-concurrent-user/styles/peak-concurrent-user.css",
                  "/peak-concurrent-user/styles/PeakConcurrentUser.less");

                pipeline.AddLessBundle(
                "/company-win-loss/styles/index.css",
                "/company-win-loss/styles/Index.less");
            });

        }
    }
}

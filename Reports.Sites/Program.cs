using Fanex.Data;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.NAP.Client.Attributes;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Sites;
using Nap_Reports.Sites.App_Start;
using Nap_Reports.Sites.DependencyInjection;
using Nap_Reports.Sites.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ONEbook.UM.AspNetCore.Filters;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Limits.MaxRequestLineSize = 302768;
        });

        services.RegisterAuthentication(configuration);
        services.RegisterGlobalServices();
        OnebookUMInjection.RegisterServices(services, configuration);
        services.Initialize();
        services.RegisterMediatR();
        services.RegisterInternalServices();

        services.ProcessWebOptimizer();

        // Add services to the container.
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new ServiceFilterAttribute(typeof(CheckUMFilterAttribute)));
            options.Filters.Add(new SessionAuthorizeAttribute());
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        services.AddHttpContextAccessor();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddRemotingRef(configuration);
        services.AddResultCore(configuration);
        services.AddFanexData(configuration);
        services.AddNapClient(configuration);
        services.AddFanexLogSentry(configuration);
       
        var app = builder.Build();
        
        app.Use((context, next) =>
        {
            context.Request.PathBase = new PathString(configuration.GetSection("PathBase").Value);
            return next();
        });

        app.UseDeveloperExceptionPage();
        app.UseWebOptimizer();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseSession();
        app.UseRouting();
       
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "universalreports",
            pattern: "UniversalReports/{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });

        app.MapControllerRoute(
            name: "main",
            pattern: "Main/{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });

        app.MapControllerRoute(
            name: "balance",
            pattern: "Balance/{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });

        app.MapControllerRoute(
            name: "winloss",
            pattern: "WinLoss/{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });

        app.MapControllerRoute(
            name: "licensee",
            pattern: "Licensee/{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });

        app.MapControllerRoute(
            name: "misc",
            pattern: "Misc/{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });

        app.MapControllerRoute(
            name: "special",
            pattern: "Special/{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });

        app.MapControllerRoute(
            name: "legendmanagement",
            pattern: "LegendManagement/{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.ConfigureExceptionHandler();

        app.Run();
    }
}
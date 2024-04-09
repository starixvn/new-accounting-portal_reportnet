using Fanex.Data;
using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.SignIn.Attribute;
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

        services.RegisterAuthentication(configuration);
        services.RegisterGlobalServices();
        OnebookUMInjection.RegisterServices(services, configuration);
        AutoMapperConfiguration.Initialize(services);
        services.RegisterMediAtR();
        services.RegisterInternalServices();

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
        services.RegisterRemotingRef(configuration);
        services.AddFanexData(configuration);
        services.RegisterSentry(configuration);

        var app = builder.Build();

        NapClientManager
           .UseConfig(app.Services.GetService<INapSettings>())
           .UseSession(app.Services.GetService<INapSession>());

        app.Use((context, next) =>
        {
            context.Request.PathBase = new PathString(configuration.GetSection("PathBase").Value);
            return next();
        });

        app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseSession();
        app.UseRouting();
       
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.ConfigureExceptionHandler();

        app.Run();
    }
}
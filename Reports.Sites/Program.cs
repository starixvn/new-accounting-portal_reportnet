using Fanex.Caching;
using Fanex.Data;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.NAP.Client;
using Fanex.NAP.Client.Attributes;
using Microsoft.AspNetCore.Mvc;
using Reports.Sites;
using Reports.Sites.DependencyInjection;
using Reports.Sites.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ONEbook.UM.DependencyInjection;
using ONEbook.UM.Filters;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestLineSize = 302768;
});

services.AddAuthentication(configuration);
services.AddGlobalServices();
services.AddRemotingRef(configuration);
services.AddResultCore(configuration);
services.AddFanexData(configuration);
services.AddNapClient(configuration);
services.AddOneBookUmForWeb(configuration);
services.AddFanexLogSentry(configuration);
services.AddMemoryCache(configuration.GetSection("fanex.caching"));
services.AddAllAutoMapper();
services.AddAllMediatR();
services.AddInternalServices();
services.AddAllWebOptimizer();
services.AddPasswordMaskActionFilter();

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

var app = builder.Build();

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

await app.RunAsync();
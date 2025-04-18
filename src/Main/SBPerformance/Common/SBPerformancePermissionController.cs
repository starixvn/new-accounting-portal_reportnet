namespace SBPerformance.Common;

using Fanex.NAP.Client;
using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/sb-performance/check-full-view")]
public class SBPerformancePermissionController(
    IUserService userService,
    INapSession napSession,
    INapSettings napSettings)
    : ControllerBase
{
    [Route("")]
    public IActionResult FullView()
    {
        var permissions = userService.GetPermissions(
                napSession.UserId,
                napSettings.SiteId,
                "SBPerformanceMonitor")
            .Where(p => p.Policy >= (int)Constant.Policy.Read)
            .Select(p => p.Key);

        return Ok(permissions.Contains("FullView"));
    }
}
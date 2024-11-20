using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace SBPerformance.Common
{
    [Route("api/sb-performance/check-full-view")]
    public class SBPerformancePermissionController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly INapSession napSession;

        private readonly INapSettings napSettings;

        public SBPerformancePermissionController(IUserService userService, INapSession napSession, INapSettings napSettings)
        {
            this.userService = userService;
            this.napSession = napSession;
            this.napSettings = napSettings;
        }

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
}


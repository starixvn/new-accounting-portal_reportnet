using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashOutPerformance.Common
{
    [Route("api/cash-out-performance/common")]
    [ApiController]
    public class CashOutPerformanceCommonApiController(
        IUserService userService,
        INapSession napSession,
        INapSettings napSettings)
        : ControllerBase
    {
        [Route("check-full-view")]
        public IActionResult FullView()
        {
            var permissions = userService.GetPermissions(
                napSession.UserId,
                napSettings.SiteId,
                "CashOutPerformance",
                "LIVE")
                .Where(p => p.Policy >= (int)Constant.Policy.Read)
                .Select(p => p.Key);
            return Ok(permissions.Contains("CashOut_FullView"));
        }
    }
}
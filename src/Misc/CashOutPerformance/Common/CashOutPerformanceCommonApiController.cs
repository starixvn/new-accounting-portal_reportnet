using Fanex.NAP.Client.Configuration;
using Fanex.NAP.Client.Helpers.Business;
using Fanex.NAP.Client.Helpers.Web;
using Fanex.NAP.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashOutPerformance.Common
{
    [Route("api/cash-out-performance/common")]
    [ApiController]
    public class CashOutPerformanceCommonApiController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly INapSession napSession;

        private readonly INapSettings napSettings;

        public CashOutPerformanceCommonApiController(IUserService userService, INapSession napSession, INapSettings napSettings)
        {
            this.userService = userService;
            this.napSession = napSession;
            this.napSettings = napSettings;
    }

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
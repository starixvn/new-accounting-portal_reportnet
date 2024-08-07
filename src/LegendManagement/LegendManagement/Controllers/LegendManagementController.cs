using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using LegendManagement.Criteria;
using LegendManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Utilities;
using Nap_Reports.Utilities.Controller;

namespace LegendManagement.Controllers;

public class LegendManagementController(LegendManagementService legendManagementService) : BaseController
{
    [HttpGet]
    [NapAuth(FunctionKey = AccReportFunctionKeys.LegendManagement, Policy = Constant.Policy.Read)]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetReportLegend([FromBody]GetReportLegendCriteria criteria)
    {
        var legendContent = legendManagementService.GetReportLegend(criteria);
        return Json(legendContent);
    }
}
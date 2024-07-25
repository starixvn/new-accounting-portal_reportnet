using LegendManagement.Criteria;
using LegendManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace LegendManagement.Controllers;

public class LegendManagementController(LegendManagementService legendManagementService) : Controller
{
    [HttpPost]
    public IActionResult GetReportLegend(GetReportLegendCriteria criteria)
    {
        var legendContent = legendManagementService.GetReportLegend(criteria);
        return new JsonResult(legendContent);
    }
}
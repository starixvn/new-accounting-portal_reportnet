using LegendManagement.Criteria;
using LegendManagement.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LegendManagement.Controllers;

[Route("LegendManagement")]
[ApiController]
public class LegendManagementApiController(LegendManagementService legendManagementService, IHostingEnvironment env) : ControllerBase
{
    [Route("GetReportLegend")]
    public IActionResult GetReportLegend(GetReportLegendCriteria criteria)
    {
        var legendContent = legendManagementService.GetReportLegend(criteria);
        return Ok(legendContent);
    }

    [Route("GetMenuList")]
    public IActionResult GetMenuList(GetSystemMenuCriteria criteria)
    {
        var result = legendManagementService.GetMenuList(criteria);
        return Ok(result);
    }

    [Route("MakeInlineImages")]
    public IActionResult MakeInlineImages(List<string> imagesPath)
    {
        var webRoot = env.WebRootPath;
        var imageFolder = Path.Combine(webRoot, "legend-management/uploaded-image");
        var result = legendManagementService.MakeInlineImages(imagesPath, imageFolder);
        return Ok(result);
    }

    [Route("SaveReportLegend")]
    public IActionResult SaveReportLegend(SaveReportLegendCriteria criteria)
    {
        var result = legendManagementService.SaveReportLegend(criteria);
        return Ok(result);
    }
}
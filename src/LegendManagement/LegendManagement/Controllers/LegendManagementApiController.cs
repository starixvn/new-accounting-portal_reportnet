namespace LegendManagement.Controllers;

using Criteria;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

[Route("LegendManagement")]
[ApiController]
public class LegendManagementApiController(LegendManagementService legendManagementService, IWebHostEnvironment env) : ControllerBase
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

    [Route("UploadImage")]
    public IActionResult UploadImage([FromForm] IFormFile file)
    {
        var webRoot = env.WebRootPath;
        var imageFolder = Path.Combine(webRoot, "legend-management/uploaded-image");
        var criteria = new UploadFileCriteria
        {
            File = file,
            SavePath = imageFolder,
            FileName = file.FileName
        };
        var result = legendManagementService.UploadImage(criteria);
        return Ok(result);
    }

    [Route("SaveReportLegendMapping")]
    public IActionResult SaveReportLegendMapping(SaveReportLegendMappingCriteria criteria)
    {
        var result = legendManagementService.SaveReportLegendMapping(criteria);
        return Ok(result);
    }

    [Route("GetInputedLegendReport")]
    public IActionResult GetInputedLegendReport(GetInputedLegendReportCriteria criteria)
    {
        var result = legendManagementService.GetInputedLegendReport(criteria);
        return Ok(result);
    }
}
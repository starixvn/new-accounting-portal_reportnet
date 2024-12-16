using Margin.Criteria;
using Margin.Services;
using Microsoft.AspNetCore.Mvc;

namespace Margin.Controller;

[Route("api/margin-win-loss")]
[ApiController]
public class MarginWinLossApiController(MarginWinLossService service, MarginWinLossExcelService excelService) : ControllerBase
{
    [Route("data")]
    public async Task<IActionResult> GetDataMarginWinLoss(MarginWinLossCriteria criteria)
    {
        criteria ??= new MarginWinLossCriteria();
            
        var result = await service.GetMarginWinLoss(criteria);

        return Ok(result);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportExcel([FromQuery] MarginWinLossCriteria criteria)
    {
        var data = await service.GetMarginWinLoss(criteria);
        const string title = "Margin Report";
        var excelPackage = await excelService.ExportExcel(data, title);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        const string fileDownloadName = "MarginReport.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
    }
}
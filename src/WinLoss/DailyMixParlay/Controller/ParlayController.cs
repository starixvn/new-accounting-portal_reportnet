using DailyMixParlay.Criteria;
using DailyMixParlay.Services;
using DailyMixParlayWinLoss.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyMixParlay.Controller
{
    [Route("api/parlay")]

    [ApiController]
    public class ParlayController(DailyMixParlayWinLossService service, DailyMixParlayWinLossExcelService excelService) : ControllerBase
    {
        [Route("combination")]
        public async Task<IActionResult> GetParlayCombination(GetCombinationWinLossCriteria criteria)
        {
            var result = await service.GetCombinationWinLossReport(criteria);
            return Ok(result);
        }

        [Route("product")]
        public async Task<IActionResult> GetParlayProduct(GetAdvanceMixParlayCriteria criteria)
        {
            var result = await service.GetAdvanceMixParlayReport(criteria);
            return Ok(result);
        }

        [Route("excel-combination")]
        [HttpPost]
        public async Task<IActionResult> TicketsExcel(GetCombinationWinLossCriteria criteria)
        {
            var data = await service.GetCombinationWinLossReport(criteria);
            var excelPackage = excelService.ExportCombinationWinLoss(data, criteria);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Daily_Mix_Parlay_Win_Loss.xlsx");
        }

        [Route("excel-product")]
        [HttpPost]
        public async Task<IActionResult> ProductTicketsExcel(GetAdvanceMixParlayCriteria criteria)
        {
            var data = await service.GetAdvanceMixParlayReport(criteria);
            var excelPackage = excelService.ExportProductWinLoss(data, criteria);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Daily_Mix_Parlay_Win_Loss.xlsx");
        }
    }
}

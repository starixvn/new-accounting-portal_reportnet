using DailyMixParlay.Criteria;
using DailyMixParlay.Services;
using DailyMixParlayWinLoss.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;

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
            var excelPackage = excelService.ExportCombinationWinLoss(data);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Daily_Mix_Parlay_Win_Loss.xlsx");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TraderPerformance.Criteria;
using TraderPerformance.Services;

namespace TraderPerformance.Controller
{
    [Route("api/trader-performance-soccer")]
    [ApiController]
    public class TraderPerformanceSoccerApiController(TraderPerformanceSoccerService service) : ControllerBase
    {
        [Route("main")]
        public async Task<IActionResult> GetTraderPerformanceSoccer(ReportCriteria criteria)
        {
            var result = await service.GetReport(criteria);

            return Ok(result);
        }

        [Route("detail")]
        public async Task<IActionResult> GetTraderPerformanceSoccerDetail(DetailReportCriteria criteria)
        {
            var result = await service.GetDetailReport(criteria);

            return Ok(result);
        }

        [Route("main-sort")]
        public async Task<IActionResult> SortTraderPerformanceSoccer(ReportCriteria criteria)
        {
            var result = await service.SortReportData(criteria);

            return Ok(result);
        }

        [Route("detail-sort")]
        public async Task<IActionResult> SortTraderPerformanceSoccerDetail(DetailReportCriteria criteria)
        {
            var result = await service.SortDetailReportData(criteria);

            return Ok(result);
        }
    }
}

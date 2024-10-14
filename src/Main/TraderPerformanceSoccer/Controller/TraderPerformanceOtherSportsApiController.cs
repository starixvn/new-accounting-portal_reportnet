using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderPerformance.Criteria;
using TraderPerformance.Services;

namespace TraderPerformance.Controller
{
    [Route("api/trader-performance-other-sports")]
    [ApiController]
    public class TraderPerformanceOtherSportsApiController(TraderPerformanceOtherSportsService service) : ControllerBase
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

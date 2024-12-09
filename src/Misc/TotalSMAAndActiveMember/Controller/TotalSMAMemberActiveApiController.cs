using Microsoft.AspNetCore.Mvc;
using TotalSMAAndActiveMember.Criteria;
using TotalSMAAndActiveMember.Model;
using TotalSMAAndActiveMember.Services;
using Utilities.DateTimes;

namespace TotalSMAAndActiveMember.Controller
{
    [Route("api/total-sma-active-member")]
    [ApiController]
    public class TotalSMAMemberActiveApiController(TotalSMAMemberActiveService service, TotalSMAMemberActiveExcelService excelService) : ControllerBase
    {
        [Route("sites")]
        public async Task<IActionResult> GetSites()
        {
            var result = await service.GetSites();

            return Ok(result);
        }

        [Route("summary")]
        public async Task<IActionResult> GetSummaryReport(TotalSMAMemberActiveCriteria criteria)
        {
            var result = await service.GetReport(criteria);

            return Ok(result);
        }

        [Route("export-summary")]
        public async Task<IActionResult> ExportSummaryReport([FromQuery] TotalSMAMemberActiveCriteria criteria)
        {
            var result = await service.GetReport(criteria);
            var title =
                $"Total SMA and Member Active Report from {criteria.FromDate.ToShortFormatedDate()} to {criteria.ToDate.ToShortFormatedDate()}";
            var excelPackage = await excelService.ExportExcel(result, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"Total_SMA_and_Member_Active_Report_From_{criteria.FromDate: MM_dd_yyyy}_To_{criteria.ToDate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }

        [Route("detail")]
        public async Task<IActionResult> GetDetailReport(StatusDetailsCriteria criteria)
        {
            var result = await service.GetStatusDetails(criteria);

            return Ok(new StatusDetailViewModel { Report = result, TotalRow = criteria.TotalRow });
        }
    }
}

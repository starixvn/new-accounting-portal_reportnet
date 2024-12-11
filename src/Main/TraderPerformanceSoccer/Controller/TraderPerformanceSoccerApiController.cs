using Microsoft.AspNetCore.Mvc;
using TraderPerformance.Criteria;
using TraderPerformance.Services;

namespace TraderPerformance.Controller
{
    [Route("api/trader-performance-soccer")]
    [ApiController]
    public class TraderPerformanceSoccerApiController(TraderPerformanceSoccerService service,
        TraderPerformanceSoccerExcelService excelService,
        TraderPerformanceSumPHExcelService sumPHExcelService) : ControllerBase
    {
        [Route("main")]
        public async Task<IActionResult> GetTraderPerformanceSoccer(ReportCriteria criteria)
        {
            var result = await service.GetReport(criteria);

            return Ok(result);
        }

        [HttpGet("export-main")]
        public async Task<IActionResult> ExportExcelTraderPerformanceSoccer([FromQuery] ReportCriteria criteria)
        {
            var data = await service.GetReport(criteria);
            var title =
                $"Trader Performance For SOCCER SPORT - From Date: {criteria.Fromdate: MMM dd, yyyy} To Date: {criteria.Todate: MMM dd, yyyy}";
            var excelPackage = await excelService.ExportExcel(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"TraderPerformance_Soccer_From_Date_{criteria.Fromdate: MM_dd_yyyy}_To_Date_{criteria.Todate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }

        [Route("detail")]
        public async Task<IActionResult> GetTraderPerformanceSoccerDetail(DetailReportCriteria criteria)
        {
            var result = await service.GetDetailReport(criteria);

            return Ok(result);
        }

        [HttpGet("export-detail")]
        public async Task<IActionResult> ExportExcelTraderPerformanceSoccerDetail([FromQuery] DetailReportCriteria criteria)
        {
            var data = await service.GetDetailReport(criteria);
            var title =
                $"Trader Performance For SOCCER SPORT Detail - From Date: {criteria.Fromdate: MMM dd, yyyy} To Date: {criteria.Todate: MMM dd, yyyy}";
            var excelPackage = await excelService.ExportExcelForDetail(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"TraderPerformance_Soccer_Detail_From_Date_{criteria.Fromdate: MM_dd_yyyy}_To_Date_{criteria.Todate: MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
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

        [HttpGet("export-sum-ph")]
        public async Task<IActionResult> ExportExcelTraderPerformanceSumPH([FromQuery] SummaryPHCriteria criteria)
        {
            var data = await service.GetSummaryPH(criteria);
            var title =
                $"Trader Performance For SOCCER PH - From Date: {criteria.Fromdate:MMM dd, yyyy} To Date: {criteria.Todate:MMM dd, yyyy}";
            var excelPackage = await sumPHExcelService.ExportExcel(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"TraderPerformance_SoccerPH_From_Date_{criteria.Fromdate:MM_dd_yyyy}_To_Date_{criteria.Todate:MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }

        [HttpGet("export-sum-ph-detail")]
        public async Task<IActionResult> ExportExcelTraderPerformanceSumPHForDetail([FromQuery] DetailPHCriteria criteria)
        {
            var data = await service.GetSummaryPH(criteria);
            var title =
                $"Trader Performance For SOCCER PH Detail - From Date: {criteria.Fromdate:MMM dd, yyyy} To Date: {criteria.Todate:MMM dd, yyyy}";
            var excelPackage = await sumPHExcelService.ExportExcelForDetail(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileDownloadName = $"TraderPerformance_SoccerPH_Detail_From_Date_{criteria.Fromdate:MM_dd_yyyy}_To_Date_{criteria.Todate:MM_dd_yyyy}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
        }
    }
}

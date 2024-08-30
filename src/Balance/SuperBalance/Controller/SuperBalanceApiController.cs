using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Utilities.Constants;
using Nap_Reports.Utilities.Services;
using SuperBalance.Criteria;
using SuperBalance.Service;
using SuperBalance.Services;

namespace SuperBalance.Controller
{
    [Route("api/super-balance")]
    [ApiController]
    public class SuperBalanceApiController(SuperBalanceService service, SuperBalanceExcelService excelService) : ControllerBase
    {
        [Route("super")]
        public async Task<IActionResult> GetSuperBalance(SuperBalanceCriteria criteria)
        {
            var result = await service.GetSuperBalance(criteria);
            return Ok(result);
        }

        [HttpGet("export-super")]
        public async Task<IActionResult> ExportExcelSuperBalance([FromQuery]SuperBalanceCriteria criteria)
        {
            var data = await service.GetSuperBalance(criteria);
            var title =
                $"{ReportService.GetRoleName(RoleConstants.Super)} {ReportService.GetBalanceType(criteria.BalanceType)} Balance";
            var excelPackage = await excelService.ExportExcelSuperBalance(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{title.Replace(" ", "")}.xlsx");
        }

        [Route("super-downline")]
        public async Task<IActionResult> GetSuperBalanceDownLine(SuperDownlineCriteria criteria)
        {
            var result = await service.GetSuperBalanceDownLine(criteria);
            return Ok(result);
        }

        [HttpGet("export-super-downline")]
        public async Task<IActionResult> ExportExcelSuperBalanceDownLine([FromQuery] SuperDownlineCriteria criteria)
        {
            var data = await service.GetSuperBalanceDownLine(criteria);
            var title =
                $"{ReportService.GetRoleName(RoleConstants.Master)} {ReportService.GetBalanceType(criteria.BalanceType)} Balance";
            var excelPackage = await excelService.ExportExcelSuperBalance(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{title.Replace(" ", "")}.xlsx");
        }

        [Route("master-downline")]
        public async Task<IActionResult> GetMasterBalanceDownLine(MasterDownlineCriteria criteria)
        {
            var result = await service.GetMasterDownLine(criteria);
            return Ok(result);
        }

        [HttpGet("export-master-downline")]
        public async Task<IActionResult> ExportExcelMasterBalanceDownLine([FromQuery] MasterDownlineCriteria criteria)
        {
            var data = await service.GetMasterDownLine(criteria);
            var title =
                $"{ReportService.GetRoleName(RoleConstants.Agent)} {ReportService.GetBalanceType(criteria.BalanceType)} Balance";
            var excelPackage = await excelService.ExportExcelSuperBalance(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{title.Replace(" ", "")}.xlsx");
        }

        [Route("agent-downline")]
        public async Task<IActionResult> GetAgentBalanceDownLine(AgentDownlineCriteria criteria)
        {
            var result = await service.GetAgentDownLine(criteria);
            return Ok(result);
        }

        [HttpGet("export-agent-downline")]
        public async Task<IActionResult> ExportExcelAgentBalanceDownLine([FromQuery] AgentDownlineCriteria criteria)
        {
            var data = await service.GetAgentDownLine(criteria);
            var title =
                $"{ReportService.GetRoleName(RoleConstants.Member)} {ReportService.GetBalanceType(criteria.BalanceType)} Balance";
            var excelPackage = await excelService.ExportExcelSuperBalance(data, title);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{title.Replace(" ", "")}.xlsx");
        }

        [Route("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var result = await service.GetCurrencies();
            return Ok(result);
        }

        [Route("alphabet")]
        public IActionResult GetListAlphabet()
        {
            var result = service.GetListAlphabet();
            return Ok(result);
        }
    }
}

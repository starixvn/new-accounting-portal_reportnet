using DirectBalance.Criteria;
using DirectBalance.Services;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Utilities.Services;

namespace DirectBalance.Controller
{
    [Route("api/direct-balance")]
    [ApiController]
    public class DirectBalanceApiController(DirectBalanceService service, DirectBalanceExcelService excelService) : ControllerBase
    {
        [Route("main")]
        public async Task<IActionResult> GetDirectBalance(DirectBalanceCriteria criteria)
        {
            var result = await service.GetDirectBalance(criteria);
            
            return Ok(result);
        }

        [HttpGet("export-main")]
        public async Task<IActionResult> ExportExcelSuperBalance([FromQuery] DirectBalanceCriteria criteria)
        {
            var data = await service.GetDirectBalance(criteria);
            var roleName = ReportService.GetRoleName(criteria.RoleID);
            var excelPackage = await excelService.ExportExcelDirectBalance(data, roleName);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"DirectBalance_{roleName}.xlsx");
        }

        [Route("master-downline")]
        public async Task<IActionResult> GetMasterDownline(MasterDownlineCriteria criteria)
        {
            var result = await service.GetDirectBalanceMasterDownLine(criteria);

            return Ok(result);
        }

        [HttpGet("export-master-downline")]
        public async Task<IActionResult> ExportExcelMasterDownline([FromQuery] MasterDownlineCriteria criteria)
        {
            var data = await service.GetDirectBalanceMasterDownLine(criteria);
            var excelPackage = await excelService.ExportExcelDirectBalance(data, "Agent");
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"DirectBalance_Agent.xlsx");
        }

        [Route("agent-downline")]
        public async Task<IActionResult> GetDirectBalanceAgentDownline(AgentDownlineCriteria criteria)
        {
            var result = await service.GetDirectBalanceAgentDownLine(criteria);

            return Ok(result);
        }

        [HttpGet("export-agent-downline")]
        public async Task<IActionResult> ExportExcelAgentDownline([FromQuery] AgentDownlineCriteria criteria)
        {
            var data = await service.GetDirectBalanceAgentDownLine(criteria);
            var excelPackage = await excelService.ExportExcelDirectBalance(data, "Member");
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"DirectBalance_Member.xlsx");
        }

        [Route("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var result = await service.GetCurrencies();
            return Ok(result);
        }
    }
}

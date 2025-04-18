namespace NegativeBalance.Controller;

using Criteria;
using Microsoft.AspNetCore.Mvc;
using Services;

[Route("api/negative-balance")]

[ApiController]

public class NegativeBalanceApiController(NegativeBalanceService service, NegativeBalanceExcelService excelService) : ControllerBase
{
    [Route("data")]
    public async Task<IActionResult> GetNegativeBalance(NegativeBalanceCriteria criteria)
    {
        var result = await service.GetNegativeBalance(criteria);
        return Ok(result);
    }

    [Route("excel")]
    public async Task<IActionResult> ExportExcel(NegativeBalanceCriteria criteria)
    {
        var excelPackage = await excelService.ExportExcelNegativeBalance(criteria);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = "Licensee-NegativeBalance" +
                       (criteria.Site == "All" ?
                           "_All_Licensees" :
                           "_" + criteria.Site);
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}
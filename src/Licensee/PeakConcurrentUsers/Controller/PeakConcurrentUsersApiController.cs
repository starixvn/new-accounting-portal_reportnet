namespace PeakConcurrentUsers.Controller;

using Criteria;
using Microsoft.AspNetCore.Mvc;
using Services;

[Route("api/peak-concurrent-user")]

[ApiController]

public class PeakConcurrentUsersApiController(PeakConcurrentUserService service, PeakConcurrentUserExcelService excelService) : ControllerBase
{
    [Route("data")]
    public async Task<IActionResult> GetPeakConcurrentUsers(PeakConcurrentUserCriteria criteria)
    {
        var result = await service.GetConcurrentUserList(criteria);
        return Ok(result);
    }

    [HttpGet]
    [Route("excel")]
    public async Task<IActionResult> ExportExcel([FromQuery]PeakConcurrentUserCriteria criteria)
    {
        var excelPackage = await excelService.ExportExcel(criteria);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Licensee-PeakConcurrentUser_For_{criteria.LicenseeName}.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}
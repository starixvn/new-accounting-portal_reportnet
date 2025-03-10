namespace DuplicateTickets.Controller;

using Criteria;
using Microsoft.AspNetCore.Mvc;
using Services;

[Route("api/duplicate-tickets")]
[ApiController]
public class DuplicateTicketsApiController(DuplicateTicketsService service, DuplicateTicketsExcelService excelService) : ControllerBase
{
    [Route("report")]
    public async Task<IActionResult> GetDuplicateTicketReport(DuplicateTicketsCriteria criteria)
    {
        var result = await service.GetDuplicateTicketByUserName(criteria);
        return Ok(result);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportExcel([FromQuery]DuplicateTicketsCriteria criteria)
    {
        var data = await service.GetDuplicateTicketByUserName(criteria);
        var title = $"Duplicate Tickets On {criteria.Date: MMM dd, yyyy}";
        var excelPackage = await excelService.ExportExcel(data, title);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileDownloadName = $"Duplicate_Tickets_On_{criteria.Date: MM_dd_yyyy}.xlsx";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
    }
}
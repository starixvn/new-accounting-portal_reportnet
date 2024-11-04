using DuplicateTickets.Criteria;
using DuplicateTickets.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuplicateTickets.Controller
{
    [Route("api/duplicate-tickets")]
    [ApiController]
    public class DuplicateTicketsApiController(DuplicateTicketsService service) : ControllerBase
    {
        [Route("report")]
        public async Task<IActionResult> GetDuplicateTicketReport(DuplicateTicketsCriteria criteria)
        {
            var result = await service.GetDuplicateTicketByUserName(criteria);
            return Ok(result);
        }
    }
}

using DailyCreatedCustomers.Criteria;
using DailyCreatedCustomers.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyCreatedCustomers.Controller
{
    [Route("api/daily-created-customer")]
    [ApiController]
    public class DailyCreatedCustomersApiController(DailyCreatedCustomersService service) : ControllerBase
    {
        [Route("summary")]
        public async Task<IActionResult> GetSummaryReport(DailyCreatedCustomersCriteria criteria)
        {
            var result = await service.GetReport(criteria);

            return Ok(result);
        }

        [Route("detail")]
        public async Task<IActionResult> GetDetailReport(DailyCreatedCustomersDetailsCriteria criteria)
        {
            var result = await service.GetDetailReport(criteria);

            return Ok(result);
        }
    }
}

using CompanyBalanceDifference.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyBalanceDifference.Controller;

[Route("api/company-balance-difference")]
[ApiController]
public class CompanyBalanceDifferenceApiController(CompanyBalanceDifferenceService service) : ControllerBase
{
    [Route("data")]
    public async Task<IActionResult> GetCompanyBalanceDifference()
    {
        var result =  await service.GetCompanyBalanceDifference();

        return Ok(result);
    }
}
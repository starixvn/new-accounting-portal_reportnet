namespace CompanyBalanceDifference.Controller;

using Microsoft.AspNetCore.Mvc;
using Services;

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
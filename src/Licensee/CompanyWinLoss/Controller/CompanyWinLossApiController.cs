using CompanyWinLoss.Criteria;
using CompanyWinLoss.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWinLoss.Controller
{
    [Route("api/company-win-loss")]

    [ApiController]
    public class CompanyWinLossApiController(CompanyWinLossService service) : ControllerBase
    {
        [Route("currencies-by-licensee")]
        public async Task<IActionResult> GetCurrenciesByLicensee(CurrenciesByLicenseeCriteria criteria)
        {
            var result = await service.GetCurrenciesByLicensee(criteria);
            return Ok(result);
        }

        [Route("product-by-licensee")]
        public async Task<IActionResult> GetProductListByLicense(ProductByLicenseCriteria criteria)
        {
            var result = await service.GetProductListByLicense(criteria);
            return Ok(result);
        }

        [Route("all-licensee")]
        public async Task<IActionResult> GetLicensees()
        {
            var result = await service.GetLicensee();
            return Ok(result);
        }

        [Route("data")]
        public async Task<IActionResult> GetCompanyWinLoss(CompanyWinLossCriteria criteria)
        {
            var result = await service.GetCompanyWinLoss(criteria);
            return Ok(result);
        }
    }
}

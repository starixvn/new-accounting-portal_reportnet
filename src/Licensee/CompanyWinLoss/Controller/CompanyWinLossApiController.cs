using CompanyWinLoss.Criteria;
using CompanyWinLoss.Services;
using Fanex.Excel;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Utilities.DateTimes;

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

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> ExportExcel([FromQuery]CompanyWinLossCriteria criteria)
        {
            var gridOptions = await service.GetFullReportFromCache(criteria);
            var excel = new FanexExcel(gridOptions);
            var workBook = excel.ToExcel2003("Company Win Loss");
            var memoryStream = new MemoryStream();
            workBook.Write(memoryStream);
            var fileName =
                $"CompanyWinLoss_From_{criteria.FromDate.ToShortFormatedDateWithSlash().Replace('/', '_')}_To_{criteria.ToDate.ToShortFormatedDateWithSlash().Replace('/', '_')}.xls";
            return File(memoryStream.ToArray(), "application/vnd.ms-excel", fileName);
        }
    }
}

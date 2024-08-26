using Microsoft.AspNetCore.Mvc;

namespace Nap_Reports.Utilities.Currencies
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrenciesController(CurrenciesService service) : ControllerBase
    {
        [Route("data")]
        public async Task<IActionResult> GetCurrencies()
        {
            var data = await service.GetCurrencies();
            
            return Ok(data);
        }
    }
}

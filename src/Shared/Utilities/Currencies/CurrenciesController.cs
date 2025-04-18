namespace Utilities.Currencies;

using Microsoft.AspNetCore.Mvc;

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
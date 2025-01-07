using Microsoft.AspNetCore.Mvc;
using SabaPromotion.Criteria;
using SabaPromotion.Services;

namespace SabaPromotion.Controllers;

[Route("api/saba-promotion")]
[ApiController]
public class SabaPromotionApiController(SabaPromotionService service): ControllerBase
{
    [Route("data")]
    public async Task<IActionResult> Data(SabaPromotionCriteria criteria)
    {
        var viewModel = await service.GetSabaPromotion(criteria);
        return Ok(viewModel);
    }

    [Route("promotion-type")]
    [HttpPost]
    public async Task<IActionResult> PromotionTypes()
    {
        var viewModel = await service.GetPromotionTypes();
        return Ok(viewModel);
    }
}
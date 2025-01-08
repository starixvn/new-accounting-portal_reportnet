using Microsoft.AspNetCore.Mvc;
using SabaPromotion.Criteria;
using SabaPromotion.Services;
using Utilities.ExcelResult;

namespace SabaPromotion.Controllers;

[Route("api/saba-promotion")]
[ApiController]
public class SabaPromotionApiController(SabaPromotionService service, ExcelBuilderService excelService) : ControllerBase
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

    [Route("excel")]
    [HttpPost]
    public async Task<IActionResult> Excel(SabaPromotionCriteria criteria)
    {
        var data = await service.GetSabaPromotion(criteria);
        var title = $"SABA Promotion Report From {criteria.FromDate:MMM dd, yyyy} To {criteria.ToDate:MMM dd, yyyy}";
        var excelPackage = await excelService.Build(title, data);
        return new EpplusExcelResult(excelPackage);
    }
}
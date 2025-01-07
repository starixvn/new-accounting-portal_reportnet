namespace HouseWinLoss.MetricGaming.Site;

using AutoMapper;
using Common.ColumnDecorators;
using Common.Criteria;
using Common.SiteWinLoss;
using Configuration;
using Microsoft.AspNetCore.Mvc;

[Route("api/house-win-loss-by-products/metric-gaming-superlive/site")]
[ApiController]
public class MetricGamingSiteController(
    WinLossService service,
    GridOptionsBuilder builder,
    GridOptionColumnDecoratorFull decorator,
    IMapper mapper)
    : SiteWinLossController(service, builder, decorator)
{
    [Route("")]
    public async Task<IActionResult> Json(QueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalThirdPartySiteCriteria>(queryModel);
        return await Json(criteria, queryModel.SiteType);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalThirdPartySiteCriteria>(queryModel);
        return await Excel(criteria, queryModel.SiteType, queryModel.Title, Constants.SheetName);
    }
}
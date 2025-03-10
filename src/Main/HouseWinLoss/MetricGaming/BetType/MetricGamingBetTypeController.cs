namespace HouseWinLoss.MetricGaming.BetType;

using AutoMapper;
using Common.BasicWinLoss;
using Common.ColumnDecorators;
using Common.Criteria;
using Microsoft.AspNetCore.Mvc;

[Route("api/house-win-loss-by-products/metric-gaming-superlive/bet-type")]
[ApiController]
public class MetricGamingBetTypeController(
    BasicWinLossService service,
    BasicGridOptionsBuilder builder,
    GridOptionColumnDecoratorFull decorator,
    IMapper mapper)
    : BasicWinLossController(service, builder, decorator)
{
    [Route("")]
    public async Task<IActionResult> Json(QueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalThirdPartyBetTypeCriteria>(queryModel);
        const int mainColumnWidth = 220;
        return await Json(criteria, "Bet Type", mainColumnWidth);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalThirdPartyBetTypeCriteria>(queryModel);
        const int mainColumnWidth = 220;
        return await Excel(criteria, "Bet Type", queryModel.Title, mainColumnWidth);
    }
}
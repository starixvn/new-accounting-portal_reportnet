namespace HouseWinLoss.OtherSports.BetType;

using AutoMapper;
using Common.BasicWinLoss;
using Common.ColumnDecorators;
using Microsoft.AspNetCore.Mvc;

[Route("api/house-win-loss-by-products/other-sports/bet-type")]
[ApiController]
public class OtherSportsBetTypeController(
    BasicWinLossService service,
    BasicGridOptionsBuilder builder,
    GridOptionColumnDecoratorFull decorator,
    IMapper mapper)
    : BasicWinLossController(service, builder, decorator)
{
    [Route("")]
    public async Task<IActionResult> Json(QueryModel queryModel)
    {
        var criteria = mapper.Map<Criteria>(queryModel);
        return await Json(criteria, "Bet Type");
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var criteria = mapper.Map<Criteria>(queryModel);
        return await Excel(criteria, "Bet Type", queryModel.Title);
    }
}
using AutoMapper;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.ColumnDecorators;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.OtherSports.SportType;

[Route("api/house-win-loss-by-products/other-sports/sport-type")]
[ApiController]
public class OtherSportsSportTypeController(
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
        return await Json(criteria, "Sport Type");
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var criteria = mapper.Map<Criteria>(queryModel);
        return await Excel(criteria, "Sport Type", queryModel.Title);
    }
}
namespace HouseWinLoss.Soccer.MajorMinor;

using AutoMapper;
using Common.BasicWinLoss;
using Common.ColumnDecorators;
using Microsoft.AspNetCore.Mvc;

[Route("api/house-win-loss-by-products/soccer/major-minor")]
[ApiController]
public class SoccerMajorMinorController(
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
        return await Json(criteria, "Major/Minor");
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var criteria = mapper.Map<Criteria>(queryModel);
        return await Excel(criteria, "Major/Minor", queryModel.Title);
    }
}
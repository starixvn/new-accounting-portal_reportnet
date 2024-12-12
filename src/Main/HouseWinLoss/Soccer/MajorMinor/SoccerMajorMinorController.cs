using AutoMapper;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.ColumnDecorators;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.Soccer.MajorMinor
{
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
}
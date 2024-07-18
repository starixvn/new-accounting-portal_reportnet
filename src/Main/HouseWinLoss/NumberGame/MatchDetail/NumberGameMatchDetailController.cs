using AutoMapper;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.MatchDetailByCode;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.NumberGame.MatchDetail
{
    [Route("api/house-win-loss-by-products/number-game/match-detail")]
    [ApiController]
    public class NumberGameMatchDetailController(
        WinLossService service,
        GridOptionsBuilder builder,
        MatchGridOptionColumnDecoratorByCode decorator,
        IMapper mapper)
        : MatchDetailByCodeController(service, builder, decorator)
    {
        [HttpPost, Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<NumberGameDrilldownCriteria>(queryModel);
            return await Json(criteria);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<NumberGameDrilldownCriteria>(queryModel);
            return await Excel(criteria, queryModel.Title);
        }
    }
}
using AutoMapper;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.MatchDetailByCode;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.Racing.MatchDetail
{
    [Route("api/house-win-loss-by-products/racing/match-detail")]
    [ApiController]
    public class RacingMatchDetailController(
        WinLossService service,
        GridOptionsBuilder builder,
        MatchGridOptionColumnDecoratorByCode decorator,
        IMapper mapper)
        : MatchDetailByCodeController(service, builder, decorator)
    {
        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
            return await Json(criteria);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
            return await Excel(criteria, queryModel.Title);
        }
    }
}
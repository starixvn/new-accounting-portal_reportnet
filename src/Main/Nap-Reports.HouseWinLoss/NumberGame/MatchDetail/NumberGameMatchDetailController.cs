using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.MatchDetailByCode;

namespace Nap_Reports.HouseWinLoss.NumberGame.MatchDetail
{
    [Route("api/house-win-loss-by-products/number-game/match-detail")]
    public class NumberGameMatchDetailController : MatchDetailByCodeController
    {
        private readonly IMapper mapper;

        public NumberGameMatchDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorByCode decorator, IMapper mapper)
            : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
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
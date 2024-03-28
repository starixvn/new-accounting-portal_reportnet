using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;

namespace Nap_Reports.HouseWinLoss.MetricGaming.BetType
{
    [Route("api/house-win-loss-by-products/metric-gaming-superlive/bet-type")]
    public class MetricGamingBetTypeController : BasicWinLossController
    {
        private readonly IMapper mapper;

        public MetricGamingBetTypeController(
            BasicWinLossService service,
            BasicGridOptionsBuilder builder,
            GridOptionColumnDecoratorFull decorator,
            IMapper mapper)
         : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

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
}
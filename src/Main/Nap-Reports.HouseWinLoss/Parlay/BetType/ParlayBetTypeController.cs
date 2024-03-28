using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;

namespace Nap_Reports.HouseWinLoss.Parlay.BetType
{
    [Route("api/house-win-loss-by-products/parlay/bet-type")]
    public class ParlayBetTypeController : BasicWinLossController
    {
        private readonly IMapper mapper;

        public ParlayBetTypeController(BasicWinLossService service, BasicGridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator, IMapper mapper)
         : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

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
}
using AutoMapper;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.BetBuilder.SportType
{
    [Route("api/house-win-loss-by-products/bet-builder/sport-type")]
    [ApiController]
    public class BetBuilderSportTypeController : BasicWinLossController
    {
        private readonly IMapper mapper;

        public BetBuilderSportTypeController(
            BasicWinLossService service,
            BasicGridOptionsBuilder builder,
            GridOptionColumnDecoratorFull decorator,
            IMapper mapper)
        : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
        public Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSecondLevelCriteria>(queryModel);
            return Json(criteria, "Sport Type");
        }

        [Route("excel")]
        public Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSecondLevelCriteria>(queryModel);
            return Excel(criteria, "Sport Type", queryModel.Title);
        }
    }
}
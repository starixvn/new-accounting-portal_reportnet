using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.MatchDetail;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.BetBuilder.MatchDetail
{
    [Route("api/house-win-loss-by-products/bet-builder/match-detail")]
    [ApiController]
    public class BetBuilderMatchDetailController : MatchDetailController
    {
        private readonly IMapper mapper;
        public BetBuilderMatchDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorFull decorator, IMapper mapper)
            : base(service, builder, decorator)
        {
            this.mapper = mapper;   
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
            return Ok(criteria);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
            return await Excel(criteria, queryModel.Title, Constants.SheetName);
        }
    }
}
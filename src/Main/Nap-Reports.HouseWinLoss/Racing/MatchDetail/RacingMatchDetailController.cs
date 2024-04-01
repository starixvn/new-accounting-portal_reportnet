using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.MatchDetailByCode;

namespace Nap_Reports.HouseWinLoss.Racing.MatchDetail
{
    [Route("api/house-win-loss-by-products/racing/match-detail")]
    [ApiController]
    public class RacingMatchDetailController : MatchDetailByCodeController
    {
        private readonly IMapper mapper;

        public RacingMatchDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorByCode decorator, IMapper mapper)
            : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

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
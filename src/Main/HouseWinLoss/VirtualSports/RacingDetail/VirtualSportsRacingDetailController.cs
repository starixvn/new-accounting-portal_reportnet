using AutoMapper;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.MatchDetailByCode;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.VirtualSports.RacingDetail
{
    [Route("api/house-win-loss-by-products/virtual-sports/racing-match-detail")]
    [ApiController]
    public class VirtualSportsRacingDetailController : MatchDetailByCodeController
    {
        private readonly IMapper mapper;

        public VirtualSportsRacingDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorByCode decorator, IMapper mapper)
            : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
        public Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
            return Json(criteria);
        }

        [Route("excel")]
        public Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
            return Excel(criteria, queryModel.Title);
        }
    }
}
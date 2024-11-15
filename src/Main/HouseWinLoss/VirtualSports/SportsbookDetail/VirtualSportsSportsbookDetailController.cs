namespace HouseWinLoss.VirtualSports.SportsbookDetail
{
    using AutoMapper;
    using HouseWinLoss.Common.ColumnDecorators;
    using HouseWinLoss.Common.Criteria;
    using HouseWinLoss.Common.MatchDetail;
    using HouseWinLoss.Configuration;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/house-win-loss-by-products/virtual-sports/sportsbook-match-detail")]
    [ApiController]
    public class VirtualSportsSportsbookDetailController : MatchDetailController
    {
        private readonly IMapper mapper;

        public VirtualSportsSportsbookDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorBasic decorator, IMapper mapper)
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
            return await Excel(criteria, queryModel.Title, Constants.SheetName);
        }
    }
}
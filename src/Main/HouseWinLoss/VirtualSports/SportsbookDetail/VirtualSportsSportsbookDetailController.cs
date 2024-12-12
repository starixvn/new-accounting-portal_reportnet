namespace HouseWinLoss.VirtualSports.SportsbookDetail
{
    using AutoMapper;
    using Common.ColumnDecorators;
    using Common.Criteria;
    using Common.MatchDetail;
    using Configuration;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/house-win-loss-by-products/virtual-sports/sportsbook-match-detail")]
    [ApiController]
    public class VirtualSportsSportsbookDetailController(
        WinLossService service,
        GridOptionsBuilder builder,
        MatchGridOptionColumnDecoratorBasic decorator,
        IMapper mapper)
        : MatchDetailController(service, builder, decorator)
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
            return await Excel(criteria, queryModel.Title, Constants.SheetName);
        }
    }
}
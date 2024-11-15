namespace HouseWinLoss.MetricGaming.MatchDetail
{
    using AutoMapper;
    using HouseWinLoss.Common.ColumnDecorators;
    using HouseWinLoss.Common.Criteria;
    using HouseWinLoss.Common.MatchDetail;
    using HouseWinLoss.Configuration;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/house-win-loss-by-products/metric-gaming-superlive/match-detail")]
    [ApiController]
    public class MetricGamingMatchDetailController : MatchDetailController
    {
        private readonly IMapper mapper;    
        public MetricGamingMatchDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorFull decorator, IMapper mapper)
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
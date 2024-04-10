using AutoMapper;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.MatchDetail;
using HouseWinLoss.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.Sportsbook.OutrightMatchDetail
{
    [Route("api/house-win-loss-by-products/sportsbook/outright-match-detail")]
    [ApiController]
    public class SportsbookOutrightMatchDetailController : MatchDetailController
    {
        private readonly IMapper mapper;

        public SportsbookOutrightMatchDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorOutright decorator, IMapper mapper)
            : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSportbookMatchDetailCriteria>(queryModel);
            return await Json(criteria);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSportbookMatchDetailCriteria>(queryModel);
            return await Excel(criteria, queryModel.Title, Constants.SheetName);
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.MatchDetail;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.Sportsbook.MatchDetail
{
    [Route("api/house-win-loss-by-products/sportsbook/match-detail")]
    [ApiController]
    public class SportsbookMatchDetailController : MatchDetailController
    {
        private readonly IMapper mapper;
        public SportsbookMatchDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorFull decorator, IMapper mapper)
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
using AutoMapper;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.SiteWinLoss;
using HouseWinLoss.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.Site
{
    [Route("api/house-win-loss-by-products/soccer-minor/site")]
    [ApiController]
    public class SoccerMinorSiteController : SiteWinLossController
    {
        private readonly IMapper mapper; 

        public SoccerMinorSiteController(WinLossService service, GridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator, IMapper mapper)
        : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSportbookSiteCriteria>(queryModel);
            return await Json(criteria, queryModel.SiteType);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSportbookSiteCriteria>(queryModel);
            return await Excel(criteria, queryModel.SiteType, queryModel.Title, Constants.SoccerMinorBetTypesSheetName);
        }
    }
}
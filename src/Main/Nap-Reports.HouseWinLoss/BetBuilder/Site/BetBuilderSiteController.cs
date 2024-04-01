using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.SiteWinLoss;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.BetBuilder.Site
{
    [Route("api/house-win-loss-by-products/bet-builder/site")]
    [ApiController]
    public class BetBuilderSiteController : SiteWinLossController
    {
        private readonly IMapper mapper;

        public BetBuilderSiteController(WinLossService service, GridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator, IMapper mapper)
        : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
        public Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartySiteCriteria>(queryModel);
            return Json(criteria, queryModel.SiteType);
        }

        [Route("excel")]
        public Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartySiteCriteria>(queryModel);
            return Excel(criteria, queryModel.SiteType, queryModel.Title, Constants.SheetName);
        }
    }
}
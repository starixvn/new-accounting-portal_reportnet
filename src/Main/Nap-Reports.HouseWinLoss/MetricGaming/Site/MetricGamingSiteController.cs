using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.SiteWinLoss;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.MetricGaming.Site
{
    [Route("api/house-win-loss-by-products/metric-gaming-superlive/site")]
    [ApiController]
    public class MetricGamingSiteController : SiteWinLossController
    {
        private readonly IMapper mapper;

        public MetricGamingSiteController(WinLossService service, GridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator, IMapper mapper)
            : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartySiteCriteria>(queryModel);
            return await Json(criteria, queryModel.SiteType);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartySiteCriteria>(queryModel);
            return await Excel(criteria, queryModel.SiteType, queryModel.Title, Constants.SheetName);
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.MatchDetail;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.VirtualSportsBetradar.MatchDetail
{
    [Route("api/house-win-loss-by-products/virtual-sports-betradar/match-detail")]
    [ApiController]
    public class VirtualSportsBetradarMatchDetailController : MatchDetailController
    {
        private readonly IMapper mapper;
        public VirtualSportsBetradarMatchDetailController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecoratorBasic decorator, IMapper mapper)
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
            return Excel(criteria, queryModel.Title, Constants.SheetName);
        }
    }
}
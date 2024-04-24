using AutoMapper;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.MatchDetail;
using HouseWinLoss.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.VirtualSportsBetradar.MatchDetail
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
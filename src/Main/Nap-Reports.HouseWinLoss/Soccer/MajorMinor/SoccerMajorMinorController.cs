using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;

namespace Nap_Reports.HouseWinLoss.Soccer.MajorMinor
{
    [Route("api/house-win-loss-by-products/soccer/major-minor")]
    public class SoccerMajorMinorController : BasicWinLossController
    {
        private readonly IMapper mapper;

        public SoccerMajorMinorController(
            BasicWinLossService service,
            BasicGridOptionsBuilder builder,
            GridOptionColumnDecoratorFull decorator,
            IMapper mapper)
        : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
        public Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            return Json(criteria, "Major/Minor");
        }

        [Route("excel")]
        public Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            return Excel(criteria, "Major/Minor", queryModel.Title);
        }
    }
}
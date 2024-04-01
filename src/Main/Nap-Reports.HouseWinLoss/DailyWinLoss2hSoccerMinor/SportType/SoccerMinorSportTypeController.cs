using AutoMapper;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.SportType
{
    using Microsoft.AspNetCore.Mvc;
    using Nap_Reports.Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/soccer-minor/sport-type")]
    [ApiController]
    public class SoccerMinorSportTypeController : BasicWinLossController
    {
        private readonly IMapper mapper;

        public SoccerMinorSportTypeController(BasicWinLossService service, BasicGridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator, IMapper mapper)
         : base(service, builder, decorator)
        {
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            return await Json(criteria, "Sport Type");
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            var gridOptions = await GetGridOptions(criteria, "Sport Type", queryModel.Title);
            return new FanexExcelResult(gridOptions, Constants.SoccerMinorBetTypesSheetName);
        }
    }
}
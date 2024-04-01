using AutoMapper;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.CashOut.LiveNonLive
{
    using Microsoft.AspNetCore.Mvc;
    using Nap_Reports.Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/cash-out/live-non-live")]
    [ApiController]
    public class CashOutLiveNonLiveController : ControllerBase
    {
        private readonly CashOutLiveNonLiveWinLossService service;

        private readonly BasicGridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public CashOutLiveNonLiveController(
            CashOutLiveNonLiveWinLossService service,
            BasicGridOptionsBuilder builder,
            GridOptionColumnDecoratorBasic decorator, 
            IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var gridOptions = BuildGridOptions(queryModel);

            return Ok(gridOptions);
        }

        private async Task<IEnumerable<GridOptions>> BuildGridOptions(QueryModel queryModel)
        {
            
            var criteria = mapper.Map<UniversalSecondLevelCriteria>(queryModel);
            var winlosses = (await service.Query(criteria)).ToArray();

            var majorMinorGridOptions = builder.Build(winlosses.First(), "Sport Type");
            const int mainColumnWidth = 220;
            var majorMinorDecorationGridOptions =  decorator.Build(majorMinorGridOptions, "Sport Type", mainColumnWidth);

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), "Live/Non-Live");
            var liveNonLiveDecoratGridOptions = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live", mainColumnWidth);

            return new GridOptions[] { majorMinorDecorationGridOptions , liveNonLiveDecoratGridOptions };
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSecondLevelCriteria>(queryModel);
            var winlosses = (await service.Query(criteria)).ToArray();

            var majorMinorGridOptions = builder.Build(winlosses.First(), queryModel.BetTypeTitle);
            const int mainColumnWidth = 220;
            majorMinorGridOptions = decorator.Build(majorMinorGridOptions, "Sport Type", mainColumnWidth);

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), queryModel.LiveNonLiveTitle);
            liveNonLiveGridOptions = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live", mainColumnWidth);

            return new FanexExcelResult(
                new List<GridOptions> { majorMinorGridOptions, liveNonLiveGridOptions },
                Constants.SheetName);
        }
    }
}
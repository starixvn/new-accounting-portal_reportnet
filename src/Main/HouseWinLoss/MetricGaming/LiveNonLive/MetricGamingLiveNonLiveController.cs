namespace HouseWinLoss.MetricGaming.LiveNonLive
{
    using AutoMapper;
    using Fanex.Excel.Models;
    using HouseWinLoss.Common.BasicWinLoss;
    using HouseWinLoss.Common.ColumnDecorators;
    using HouseWinLoss.Common.Criteria;
    using HouseWinLoss.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/metric-gaming-superlive/live-non-live")]
    [ApiController]
    public class MetricGamingLiveNonLiveController : ControllerBase
    {
        private readonly MetricGamingLiveNonLiveWinLossService service;

        private readonly BasicGridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;    

        public MetricGamingLiveNonLiveController(
            MetricGamingLiveNonLiveWinLossService service,
            BasicGridOptionsBuilder builder,
            GridOptionColumnDecoratorFull decorator,
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

            var majorMinorGridOptions = builder.Build(winlosses.First(), "Bet Type");
            const int mainColumnWidth = 220;
            var majorMinorGridOptionsDecoration = decorator.Build(majorMinorGridOptions, "Bet Type", mainColumnWidth);

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), "Live/Non-Live");
            var liveNonLiveGridOptionsDecoration = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live", mainColumnWidth);

            return new GridOptions[] { majorMinorGridOptionsDecoration, liveNonLiveGridOptionsDecoration };
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSecondLevelCriteria>(queryModel);
            var winlosses = (await service.Query(criteria)).ToArray();

            var majorMinorGridOptions = builder.Build(winlosses.First(), queryModel.BetTypeTitle);
            const int mainColumnWidth = 220;
            majorMinorGridOptions = decorator.Build(majorMinorGridOptions, "Bet Type", mainColumnWidth);

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), queryModel.LiveNonLiveTitle);
            liveNonLiveGridOptions = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live", mainColumnWidth);

            return new FanexExcelResult(
                new List<GridOptions> { majorMinorGridOptions, liveNonLiveGridOptions },
                Constants.SheetName);
        }
    }
}
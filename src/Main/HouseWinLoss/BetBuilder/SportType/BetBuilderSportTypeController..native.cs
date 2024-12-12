namespace HouseWinLoss.BetBuilder.SportType
{
    using AutoMapper;
    using Fanex.Excel.Models;
    using Common.BasicWinLoss;
    using Common.ColumnDecorators;
    using Common.Criteria;
    using Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/bet-builder/sport-type")]
    [ApiController]
    public class BetBuilderSportTypeController(BetBuilderSportTypeWinLossService service,
        BasicGridOptionsBuilder builder,
        GridOptionColumnDecoratorFull decorator,
        IMapper mapper) : ControllerBase
    {
        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var gridOptions = await BuildGridOptions(queryModel);

            return Ok(gridOptions);
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
                [majorMinorGridOptions, liveNonLiveGridOptions],
                Constants.SheetName);
        }

        private async Task<IEnumerable<GridOptions>> BuildGridOptions(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSecondLevelCriteria>(queryModel);
            var winlosses = (await service.Query(criteria)).ToArray();

            var majorMinorGridOptions = builder.Build(winlosses.First(), "Sport Type");
            const int mainColumnWidth = 220;
            var majorMinorDecorationGridOptions = decorator.Build(majorMinorGridOptions, "Sport Type", mainColumnWidth);

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), "Live/Non-Live");
            var liveNonLiveDecorateGridOptions = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live", mainColumnWidth);

            return [majorMinorDecorationGridOptions, liveNonLiveDecorateGridOptions];
        }
    }
}
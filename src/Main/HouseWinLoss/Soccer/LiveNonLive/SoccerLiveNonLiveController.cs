using AutoMapper;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Configuration;

namespace HouseWinLoss.Soccer.LiveNonLive
{
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/soccer/live-nonlive")]
    [ApiController]
    public class SoccerLiveNonLiveController : ControllerBase
    {
        private readonly SoccerLiveNonLiveWinLossService service;

        private readonly BasicGridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public SoccerLiveNonLiveController(
            SoccerLiveNonLiveWinLossService service,
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
            var gridOptions = await BuildGridOptions(queryModel);
            return Ok(gridOptions);
        }

        private async Task<IEnumerable<GridOptions>> BuildGridOptions(QueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            var winlosses = (await service.Query(criteria)).ToArray();

            var majorMinorGridOptions = builder.Build(winlosses.First(), "Major/Minor");
            var majorMinorGridOptionsDecoration = decorator.Build(majorMinorGridOptions, "Major/Minor");

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), "Live/Non-Live");
            var liveNonLiveGridOptionsDecoration = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live");

            return new GridOptions[] { majorMinorGridOptionsDecoration, liveNonLiveGridOptionsDecoration };
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            var winlosses = (await service.Query(criteria)).ToArray();

            var majorMinorGridOptions = builder.Build(winlosses.First(), queryModel.MajorMinorTitle);
            majorMinorGridOptions = decorator.Build(majorMinorGridOptions, "Major/Minor");

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), queryModel.LiveNonLiveTitle);
            liveNonLiveGridOptions = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live");

            return new FanexExcelResult(
                new List<GridOptions> { majorMinorGridOptions, liveNonLiveGridOptions },
                Constants.SheetName);
        }
    }
}
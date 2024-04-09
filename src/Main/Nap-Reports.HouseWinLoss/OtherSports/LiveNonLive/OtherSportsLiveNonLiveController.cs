using AutoMapper;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.OtherSports.LiveNonLive
{
    using Microsoft.AspNetCore.Mvc;
    using Nap_Reports.Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/other-sports/live-non-live")]
    [ApiController]
    public class OtherSportsLiveNonLiveController : ControllerBase
    {
        private readonly OtherSportsLiveNonLiveWinLossService service;

        private readonly BasicGridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;    

        public OtherSportsLiveNonLiveController(
            OtherSportsLiveNonLiveWinLossService service,
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

            var majorMinorGridOptions = builder.Build(winlosses.First(), "Sport Type");
            var majorMinorGridOptionsDecoration = decorator.Build(majorMinorGridOptions, "Sport Type");

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), "Live/Non-Live");
            var liveNonLiveGridOptionsDecoration = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live");

            return new GridOptions[] { majorMinorGridOptionsDecoration, liveNonLiveGridOptionsDecoration };
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            var winlosses = (await service.Query(criteria)).ToArray();

            var majorMinorGridOptions = builder.Build(winlosses.First(), queryModel.SportTypeTitle);
            majorMinorGridOptions = decorator.Build(majorMinorGridOptions, "Sport Type");

            var liveNonLiveGridOptions = builder.Build(winlosses.Last(), queryModel.LiveNonLiveTitle);
            liveNonLiveGridOptions = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live");

            return new FanexExcelResult(
                new List<GridOptions> { majorMinorGridOptions, liveNonLiveGridOptions },
                Constants.SheetName);
        }
    }
}
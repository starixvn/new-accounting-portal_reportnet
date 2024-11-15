namespace HouseWinLoss.NG2.BetType
{
    using AutoMapper;
    using Fanex.Excel.Models;
    using HouseWinLoss.Common.BasicWinLoss;
    using HouseWinLoss.Common.ColumnDecorators;
    using HouseWinLoss.Common.Criteria;
    using HouseWinLoss.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    [ApiController]
    public class NG2BetTypeController : ControllerBase
    {
        private readonly BasicWinLossService service;

        private readonly BasicGridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public NG2BetTypeController(
           BasicWinLossService service,
           BasicGridOptionsBuilder builder,
           GridOptionColumnDecorator decorator,
           IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        [Route("api/house-win-loss-by-products/ng2-live-non-live/excel")]
        public async Task<IActionResult> ExcelLiveNonLive(ExcelLiveNonLiveQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartyBetTypeCriteria>(queryModel);
            var winlosses = await QueryLiveNonLiveBetType(criteria);

            var nonLiveGridOptions = builder.Build(winlosses[0], queryModel.NonLiveTitle);
            nonLiveGridOptions = decorator.Build(nonLiveGridOptions, "Bet Type");

            var liveGridOptions = builder.Build(winlosses[1], queryModel.LiveTitle);
            liveGridOptions = decorator.Build(liveGridOptions, "Bet Type");

            return new FanexExcelResult(new List<GridOptions> { nonLiveGridOptions, liveGridOptions }, Constants.SheetName);
        }

        private async Task<BasicWinLossModel[]> QueryLiveNonLiveBetType(UniversalThirdPartyBetTypeCriteria criteria)
        {
            var liveCriteria = criteria.Clone();
            liveCriteria.IsLive = 1;

            var nonLiveCriteria = criteria.Clone();
            nonLiveCriteria.IsLive = 0;

            var tasks = new[]
            {
                 Task.Run(async () => await service.Query(nonLiveCriteria)),
                 Task.Run(async () => await service.Query(liveCriteria))
            };

            return await Task.WhenAll(tasks);
        }
    }
}
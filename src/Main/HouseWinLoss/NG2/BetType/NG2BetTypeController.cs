namespace HouseWinLoss.NG2.BetType
{
    using AutoMapper;
    using Common.BasicWinLoss;
    using Common.ColumnDecorators;
    using Common.Criteria;
    using Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    [ApiController]
    public class NG2BetTypeController(
        BasicWinLossService service,
        BasicGridOptionsBuilder builder,
        GridOptionColumnDecorator decorator,
        IMapper mapper)
        : ControllerBase
    {
        [Route("api/house-win-loss-by-products/ng2-live-non-live/excel")]
        public async Task<IActionResult> ExcelLiveNonLive(ExcelLiveNonLiveQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalThirdPartyBetTypeCriteria>(queryModel);
            var winlosses = await QueryLiveNonLiveBetType(criteria);

            var nonLiveGridOptions = builder.Build(winlosses[0], queryModel.NonLiveTitle);
            nonLiveGridOptions = decorator.Build(nonLiveGridOptions, "Bet Type");

            var liveGridOptions = builder.Build(winlosses[1], queryModel.LiveTitle);
            liveGridOptions = decorator.Build(liveGridOptions, "Bet Type");

            return new FanexExcelResult([nonLiveGridOptions, liveGridOptions], Constants.SheetName);
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
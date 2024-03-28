using AutoMapper;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType
{
    using Microsoft.AspNetCore.Mvc;
    using Nap_Reports.Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/soccer-minor/bet-type-full")]
    public class SoccerMinorBetTypeFullController : ControllerBase
    {
        private readonly BasicWinLossService service;

        private readonly BasicGridOptionsBuilder builder;

        private readonly GridOptionColumnDecoratorFull decorator;

        private readonly IMapper mapper;

        public SoccerMinorBetTypeFullController(
            BasicWinLossService service,
            BasicGridOptionsBuilder builder,
            GridOptionColumnDecoratorFull decorator,
            IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            const int allLiveAndNonLive = 2;
            criteria.IsLive = allLiveAndNonLive;
            var sumGridOption = await GetGridOptions(criteria, queryModel.BetTypeFullTitle);

            criteria.IsLive = 1;
            var liveGridOption = await GetGridOptions(criteria, queryModel.BetTypeLiveTitle);

            criteria.IsLive = 0;
            var noneLiveGridOption = await GetGridOptions(criteria, queryModel.BetTypeLiveTitle);

            return new FanexExcelResult(
                new List<GridOptions> { sumGridOption, liveGridOption, noneLiveGridOption },
                Constants.SoccerMinorBetTypesSheetName);
        }

        private async Task<GridOptions> GetGridOptions(WinLossCriteria criteria, string title)
        {
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, title);
            const int mainColumnWidth = 210;
            return decorator.Build(gridOptions, "Bet Type", mainColumnWidth);
        }
    }
}

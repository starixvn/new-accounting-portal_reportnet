namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType;

using AutoMapper;
using Fanex.Excel.Models;
using Common.BasicWinLoss;
using Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/soccer-minor/bet-type-full")]
[ApiController]
public class SoccerMinorBetTypeFullController(
    BasicWinLossService service,
    BasicGridOptionsBuilder builder,
    GridOptionColumnDecoratorFull decorator,
    IMapper mapper)
    : ControllerBase
{
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
            [sumGridOption, liveGridOption, noneLiveGridOption],
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
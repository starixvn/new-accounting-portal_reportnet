namespace HouseWinLoss.CashOut.LiveNonLive;

using AutoMapper;
using Fanex.Excel.Models;
using Common.BasicWinLoss;
using Common.ColumnDecorators;
using Common.Criteria;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/cash-out/live-non-live")]
[ApiController]
public class CashOutLiveNonLiveController(
    CashOutLiveNonLiveWinLossService service,
    BasicGridOptionsBuilder builder,
    GridOptionColumnDecoratorBasic decorator,
    IMapper mapper)
    : ControllerBase
{
    private readonly GridOptionColumnDecorator decorator = decorator;

    [Route("")]
    public IActionResult Json(QueryModel queryModel)
    {
        var gridOptions = BuildGridOptions(queryModel);

        return Ok(gridOptions);
    }

    private async Task<IEnumerable<GridOptions>> BuildGridOptions(QueryModel queryModel)
    {
            
        var criteria = mapper.Map<UniversalSecondLevelCriteria>(queryModel);
        var winlosses = (await service.Query(criteria)).ToArray();

        var majorMinorGridOptions = builder.Build(winlosses[0], "Sport Type");
        const int mainColumnWidth = 220;
        var majorMinorDecorationGridOptions =  decorator.Build(majorMinorGridOptions, "Sport Type", mainColumnWidth);

        var liveNonLiveGridOptions = builder.Build(winlosses[1], "Live/Non-Live");
        var liveNonLiveDecorateGridOptions = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live", mainColumnWidth);

        return [majorMinorDecorationGridOptions , liveNonLiveDecorateGridOptions];
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalSecondLevelCriteria>(queryModel);
        var winlosses = (await service.Query(criteria)).ToArray();

        var majorMinorGridOptions = builder.Build(winlosses[0], queryModel.BetTypeTitle);
        const int mainColumnWidth = 220;
        majorMinorGridOptions = decorator.Build(majorMinorGridOptions, "Sport Type", mainColumnWidth);

        var liveNonLiveGridOptions = builder.Build(winlosses[1], queryModel.LiveNonLiveTitle);
        liveNonLiveGridOptions = decorator.Build(liveNonLiveGridOptions, "Live/Non-Live", mainColumnWidth);

        return new FanexExcelResult(
            [majorMinorGridOptions, liveNonLiveGridOptions],
            Constants.SheetName);
    }
}
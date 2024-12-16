namespace HouseWinLoss.OtherSports.LiveNonLive;

using AutoMapper;
using Fanex.Excel.Models;
using Common.BasicWinLoss;
using Common.ColumnDecorators;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/other-sports/live-non-live")]
[ApiController]
public class OtherSportsLiveNonLiveController(
    OtherSportsLiveNonLiveWinLossService service,
    BasicGridOptionsBuilder builder,
    GridOptionColumnDecoratorFull decorator,
    IMapper mapper)
    : ControllerBase
{
    private readonly GridOptionColumnDecorator decorator = decorator;

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

        return [majorMinorGridOptionsDecoration, liveNonLiveGridOptionsDecoration];
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
            [majorMinorGridOptions, liveNonLiveGridOptions],
            Constants.SheetName);
    }
}
namespace HouseWinLoss.Common.MatchDetail;

using ColumnDecorators;
using Criteria;
using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

public class MatchDetailController(
    WinLossService service,
    GridOptionsBuilder builder,
    MatchGridOptionColumnDecorator decorator)
    : ControllerBase
{
    protected async Task<IActionResult> Json(WinLossCriteria criteria)
    {
        var gridOptions = await GetGridOptions(criteria, string.Empty);
        return Ok(gridOptions);
    }

    protected async Task<IActionResult> Excel(WinLossCriteria criteria, string title, string sheetName)
    {
        var gridOptions = await GetGridOptions(criteria, title);
        return new FanexExcelResult(gridOptions, sheetName);
    }

    private async Task<GridOptions> GetGridOptions(WinLossCriteria criteria, string title)
    {
        var winlosses = await service.Query(criteria);
        var gridOptions = builder.Build(winlosses, title);
        
        return decorator.Build(gridOptions, string.Empty, string.Empty);
    }
}
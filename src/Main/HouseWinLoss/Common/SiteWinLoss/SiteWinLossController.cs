namespace HouseWinLoss.Common.SiteWinLoss;

using ColumnDecorators;
using Criteria;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

public class SiteWinLossController(
    WinLossService service,
    GridOptionsBuilder builder,
    GridOptionColumnDecorator decorator)
    : ControllerBase
{
    protected async Task<IActionResult> Json(WinLossCriteria criteria, string site)
    {
        var winlosses = await service.Query(criteria);
        var gridOptions = builder.Build(winlosses, string.Empty);
        gridOptions = decorator.Build(gridOptions, site);

        return Ok(gridOptions);
    }

    protected async Task<IActionResult> Excel(WinLossCriteria criteria, string site, string title, string sheetName)
    {
        var winlosses = await service.Query(criteria);
        var gridOptions = builder.Build(winlosses, title);
        gridOptions = decorator.Build(gridOptions, site);

        return new FanexExcelResult(gridOptions, sheetName);
    }
}
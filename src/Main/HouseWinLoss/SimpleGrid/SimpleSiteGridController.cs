namespace HouseWinLoss.SimpleGrid;

using Common.ColumnDecorators;
using Common.SiteWinLoss;
using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/simple-site-grid")]
[ApiController]
public class SimpleSiteGridController(
    SimpleCriteriaFactory modelFactory,
    WinLossService service,
    GridOptionsBuilder builder,
    GridOptionColumnDecorator decorator)
    : ControllerBase
{
    [Route("")]
    public async Task<IActionResult> Json(Dictionary<string, object> queryModel)
    {
        var gridOptions = await GetGridOptions(queryModel);

        return Ok(gridOptions);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(Dictionary<string, object> queryModel)
    {
        var gridOptions = await GetGridOptions(queryModel);

        return new FanexExcelResult(gridOptions, queryModel["sheetName"].ToString());
    }

    private async Task<GridOptions> GetGridOptions(Dictionary<string, object> queryModel)
    {
        var criteria = modelFactory.CreateCriteria(queryModel);
        var decorationModel = modelFactory.CreateGridDecorationModel(queryModel);
        var winlosses = await service.Query(criteria);
        var gridOptions = builder.Build(winlosses, decorationModel.Title);
        return decorator.Build(gridOptions, decorationModel.MainColumnText, decorationModel.MainColumnWidth);
    }
}
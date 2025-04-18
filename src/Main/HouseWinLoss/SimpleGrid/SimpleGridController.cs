namespace HouseWinLoss.SimpleGrid;

using Common.BasicWinLoss;
using Common.ColumnDecorators;
using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/simple-grid")]
[ApiController]
public class SimpleGridController(
    SimpleCriteriaFactory modelFactory,
    BasicWinLossService basicWinLossService,
    BasicGridOptionsBuilder builder,
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
        var winlosses = await basicWinLossService.Query(criteria);
        var decorationModel = modelFactory.CreateGridDecorationModel(queryModel);
        var gridOptions = builder.Build(winlosses, decorationModel.Title);
        return decorator.Build(gridOptions, decorationModel.MainColumnText, decorationModel.MainColumnWidth);
    }
}
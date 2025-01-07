using Fanex.Excel.Models;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.SiteWinLoss;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

namespace HouseWinLoss.SimpleGrid;

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
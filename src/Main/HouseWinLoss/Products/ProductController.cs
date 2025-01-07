namespace HouseWinLoss.Products;

using Common.ColumnDecorators;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/product")]
[ApiController]
public class ProductController(Service service, GridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator)
    : ControllerBase
{
    [HttpPost, Route("")]
    public async Task<IActionResult> Json(QueryModel queryModel)
    {
        var winlosses = await service.Query(queryModel);
        var gridOptions = builder.Build(winlosses, "Main");
        gridOptions = decorator.Build(gridOptions, "Products");

        return Ok(gridOptions);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var winlosses = await service.Query(queryModel);
        var gridOptions = builder.Build(winlosses, queryModel.Title);
        gridOptions = decorator.Build(gridOptions, "Products");
        return new FanexExcelResult(gridOptions, Constants.SheetName);
    }
}
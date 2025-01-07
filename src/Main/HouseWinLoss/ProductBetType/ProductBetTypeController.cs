namespace HouseWinLoss.ProductBetType;

using Common.ColumnDecorators;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/product-bettype")]
[ApiController]
public class ProductBetTypeController(
    Service service,
    GridOptionsBuilder builder,
    GridOptionColumnDecoratorFull decorator)
    : ControllerBase
{
    [Route("")]
    public async Task<IActionResult> Json(QueryModel queryModel)
    {
        var winlosses = await service.Query(queryModel);
        var gridOptions = builder.Build(winlosses, "Bet Type");
        gridOptions = decorator.Build(gridOptions, "Bet Type");

        return Ok(gridOptions);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var winlosses = await service.Query(queryModel);
        var gridOptions = builder.Build(winlosses, queryModel.Title);
        gridOptions = decorator.Build(gridOptions, "Bet Type");
        return new FanexExcelResult(gridOptions, Constants.SheetName);
    }
}
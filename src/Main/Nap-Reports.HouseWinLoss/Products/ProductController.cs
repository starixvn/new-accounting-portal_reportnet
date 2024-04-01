using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Configuration;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.Products
{
    [Route("api/house-win-loss-by-products/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Service service;

        private readonly GridOptionsBuilder builder;

        private readonly GridOptionColumnDecoratorFull decorator;

        public ProductController(Service service, GridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
        }

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
}
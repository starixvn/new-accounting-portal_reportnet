using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Configuration;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.ProductBetType
{
    [Route("api/house-win-loss-by-products/product-bettype")]
    [ApiController]
    public class ProductBetTypeController : ControllerBase
    {
        private readonly Service service;

        private readonly GridOptionsBuilder builder;

        private readonly GridOptionColumnDecoratorFull decorator;

        public ProductBetTypeController(Service service, GridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
        }

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
}

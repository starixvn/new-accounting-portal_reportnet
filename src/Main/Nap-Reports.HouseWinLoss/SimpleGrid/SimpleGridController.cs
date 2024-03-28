using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.SimpleGrid
{
    [Route("api/house-win-loss-by-products/simple-grid")]
    public class SimpleGridController : ControllerBase
    {
        private readonly SimpleCriteriaFactory modelFactory;

        private readonly BasicWinLossService basicWinLossService;

        private readonly BasicGridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        public SimpleGridController(
            SimpleCriteriaFactory modelFactory,
            BasicWinLossService basicWinLossService,
            BasicGridOptionsBuilder builder,
            GridOptionColumnDecorator decorator)
        {
            this.modelFactory = modelFactory;
            this.basicWinLossService = basicWinLossService;
            this.builder = builder;
            this.decorator = decorator;
        }

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
}

using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.SiteWinLoss;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.SimpleGrid
{
    [Route("api/house-win-loss-by-products/simple-site-grid")]
    [ApiController]
    public class SimpleSiteGridController : ControllerBase
    {
        private readonly SimpleCriteriaFactory modelFactory;

        private readonly WinLossService service;

        private readonly GridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        public SimpleSiteGridController(
            SimpleCriteriaFactory modelFactory,
            WinLossService service,
            GridOptionsBuilder builder,
            GridOptionColumnDecorator decorator)
        {
            this.modelFactory = modelFactory;
            this.service = service;
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
            var decorationModel = modelFactory.CreateGridDecorationModel(queryModel);
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, decorationModel.Title);
            return decorator.Build(gridOptions, decorationModel.MainColumnText, decorationModel.MainColumnWidth);
        }
    }
}

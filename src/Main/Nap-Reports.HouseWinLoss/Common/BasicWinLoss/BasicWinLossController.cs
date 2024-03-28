using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Configuration;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.Common.BasicWinLoss
{
    public class BasicWinLossController : ControllerBase
    {
        public BasicWinLossController(
            BasicWinLossService service,
            BasicGridOptionsBuilder builder,
            GridOptionColumnDecorator decorator)
        {
            Service = service;
            Builder = builder;
            Decorator = decorator;
        }

        protected BasicWinLossService Service { get; }

        protected BasicGridOptionsBuilder Builder { get; }

        protected GridOptionColumnDecorator Decorator { get; }

        protected async Task<IActionResult> Json(WinLossCriteria criteria, string mainColumnText, int mainColumnWidth = 180)
        {
            var gridOptions = await GetGridOptions(criteria, mainColumnText, string.Empty, mainColumnWidth);
            return Ok(gridOptions);
        }

        [Route("excel")]
        protected async Task<IActionResult> Excel(WinLossCriteria criteria, string mainColumnText, string title, int mainColumnWidth = 180)
        {
            var gridOptions = await GetGridOptions(criteria, mainColumnText, title, mainColumnWidth);
            return new FanexExcelResult(gridOptions, Constants.SheetName);
        }

        protected async Task<GridOptions> GetGridOptions(WinLossCriteria criteria, string mainColumnText, string title = "", int mainColumnWidth = 180)
        {
            var winlosses = await Service.Query(criteria);
            var gridOptions = Builder.Build(winlosses, title);
            return Decorator.Build(gridOptions, mainColumnText, mainColumnWidth);
        }
    }
}
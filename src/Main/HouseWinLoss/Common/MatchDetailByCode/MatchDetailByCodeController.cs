namespace HouseWinLoss.Common.MatchDetailByCode
{
    using Fanex.Excel.Models;
    using ColumnDecorators;
    using Criteria;
    using Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    public class MatchDetailByCodeController(
        WinLossService service,
        GridOptionsBuilder builder,
        MatchGridOptionColumnDecorator decorator)
        : ControllerBase
    {
        public async Task<IActionResult> Json(WinLossCriteria criteria)
        {
            var gridOptions = await GetGridOptions(criteria, string.Empty);
            return Ok(gridOptions);
        }

        protected async Task<IActionResult> Excel(WinLossCriteria criteria, string title)
        {
            var gridOptions = await GetGridOptions(criteria, title);
            return new FanexExcelResult(gridOptions, Constants.SheetName);
        }

        private async Task<GridOptions> GetGridOptions(WinLossCriteria criteria, string title)
        {
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, title);

            return decorator.Build(gridOptions);
        }
    }
}
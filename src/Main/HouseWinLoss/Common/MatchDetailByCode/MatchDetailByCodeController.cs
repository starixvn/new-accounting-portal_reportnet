using Fanex.Excel.Models;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Configuration;

namespace HouseWinLoss.Common.MatchDetailByCode
{
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    public class MatchDetailByCodeController : ControllerBase
    {
        private readonly WinLossService service;

        private readonly GridOptionsBuilder builder;

        private readonly MatchGridOptionColumnDecorator decorator;

        public MatchDetailByCodeController(WinLossService service, GridOptionsBuilder builder, MatchGridOptionColumnDecorator decorator)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
        }

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
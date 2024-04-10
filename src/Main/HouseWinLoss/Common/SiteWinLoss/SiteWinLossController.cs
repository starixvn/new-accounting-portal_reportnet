using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

namespace HouseWinLoss.Common.SiteWinLoss
{
    public class SiteWinLossController : ControllerBase
    {
        private readonly WinLossService service;

        private readonly GridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        public SiteWinLossController(WinLossService service, GridOptionsBuilder builder, GridOptionColumnDecorator decorator)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
        }

        protected async Task<IActionResult> Json(WinLossCriteria criteria, string site)
        {
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, string.Empty);
            gridOptions = decorator.Build(gridOptions, site);

            return Ok(gridOptions);
        }

        protected async Task<IActionResult> Excel(WinLossCriteria criteria, string site, string title, string sheetName)
        {
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, title);
            gridOptions = decorator.Build(gridOptions, site);

            return new FanexExcelResult(gridOptions, sheetName);
        }
    }
}
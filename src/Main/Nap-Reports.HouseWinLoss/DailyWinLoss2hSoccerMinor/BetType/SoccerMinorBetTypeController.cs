using AutoMapper;
using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.Models;
using Nap_Reports.HouseWinLoss.Configuration;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType
{
    [Route("api/house-win-loss-by-products/soccer-minor/bet-type")]
    public class SoccerMinorBetTypeController : ControllerBase
    {
        private readonly BasicWinLossService service;

        private readonly BasicGridOptionsBuilder builder;

        private readonly BettypeGridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public SoccerMinorBetTypeController(
            BasicWinLossService service,
            BasicGridOptionsBuilder builder,
            BettypeGridOptionColumnDecorator decorator,
            IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            const int mainColumnWidth = 210;
            var gridOptions = await GetGridOptions(criteria, "Bet Type", queryModel.SortColumnName, queryModel.SortDirection, string.Empty, mainColumnWidth);
            return Ok(gridOptions);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            const int mainColumnWidth = 210;
            var gridOptions = await GetGridOptions(criteria, "Bet Type", queryModel.SortColumnName, queryModel.SortDirection, queryModel.BetTypeFullTitle, mainColumnWidth);
            return new FanexExcelResult(gridOptions, Constants.SheetName);
        }

        private async Task<GridOptions> GetGridOptions(Criteria criteria, string mainColumnText, string sortColumnName, string sortDirection, string title = "", int mainColumnWidth = 180)
        {
            var winlosses = await service.Query(criteria);

            if (string.IsNullOrEmpty(sortColumnName))
            {
                sortColumnName = "RealIncome";
            }

            if (string.IsNullOrEmpty(sortDirection))
            {
                sortDirection = "desc";
            }

            SortData(winlosses, sortDirection, sortColumnName);

            var gridOptions = builder.Build(winlosses, title);
            return decorator.Build(gridOptions, mainColumnText, sortColumnName, sortDirection, mainColumnWidth);
        }

        private static void SortData(BasicWinLossModel winlosses, string sortDirection, string sortColumnName)
        {
            var propInfo = typeof(WinLoss).GetProperty(sortColumnName);
            var isAsc = string.Equals(sortDirection, SortingInformation.Ascending, StringComparison.OrdinalIgnoreCase);
            winlosses.SiteWinLossSummaries = isAsc ?
            winlosses
                .SiteWinLossSummaries
                .OrderBy(s => propInfo.GetValue(s.Total, null))
                .ToList() :
            winlosses
                 .SiteWinLossSummaries
                .OrderByDescending(s => propInfo.GetValue(s.Total, null))
                .ToList();
        }
    }
}
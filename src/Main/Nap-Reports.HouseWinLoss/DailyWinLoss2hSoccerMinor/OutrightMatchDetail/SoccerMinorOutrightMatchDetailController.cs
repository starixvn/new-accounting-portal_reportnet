using AutoMapper;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.MatchDetail;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.OutrightMatchDetail
{
    using Microsoft.AspNetCore.Mvc;
    using Nap_Reports.Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/soccer-minor/outright-match-detail")]
    public class SoccerMinorOutrightMatchDetailController : ControllerBase
    {
        private readonly WinLossService service;

        private readonly GridOptionsBuilder builder;

        private readonly SoccerMinorMatchGridOptionColumnDecoratorOutright decorator;

        private readonly MatchSortingService sortingService;

        private readonly IMapper mapper;

        public SoccerMinorOutrightMatchDetailController(
            WinLossService service,
            GridOptionsBuilder builder,
            SoccerMinorMatchGridOptionColumnDecoratorOutright decorator,
            MatchSortingService sortingService,
            IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
            this.sortingService = sortingService;
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSportbookMatchDetailCriteria>(queryModel);
            var gridOptions = await GetGridOptions(criteria, string.Empty, queryModel.SortColumnName, queryModel.SortDirection);
            return Ok(gridOptions);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<UniversalSportbookMatchDetailCriteria>(queryModel);
            var gridOptions = await GetGridOptions(criteria, queryModel.Title, queryModel.SortColumnName, queryModel.SortDirection);
            return new FanexExcelResult(gridOptions, Constants.SoccerMinorBetTypesSheetName);
        }

        private async Task<GridOptions> GetGridOptions(WinLossCriteria criteria, string title, string sortColumnName, string sortDirection)
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

            winlosses = sortingService.SortData(winlosses, sortDirection, sortColumnName);

            var gridOptions = builder.Build(winlosses, title);
            return decorator.Build(gridOptions, sortColumnName, sortDirection);
        }
    }
}
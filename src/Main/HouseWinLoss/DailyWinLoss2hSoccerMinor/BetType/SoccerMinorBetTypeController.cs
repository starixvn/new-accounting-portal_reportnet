namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType;

using AutoMapper;
using Fanex.Excel.Models;
using Common.BasicWinLoss;
using Common.Models;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/soccer-minor/bet-type")]
[ApiController]
public class SoccerMinorBetTypeController(
    BasicWinLossService service,
    BasicGridOptionsBuilder builder,
    BettypeGridOptionColumnDecorator decorator,
    IMapper mapper)
    : ControllerBase
{
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
        if (propInfo == null)
        {
            return;
        }

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
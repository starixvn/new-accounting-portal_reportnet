namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.MatchDetail;

using AutoMapper;
using Common.Criteria;
using Common.MatchDetail;
using Configuration;
using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/soccer-minor/match-detail")]
[ApiController]
public class SoccerMinorMatchDetailController(
    WinLossService service,
    GridOptionsBuilder builder,
    SoccerMinorMatchGridOptionColumnDecorator decorator,
    MatchSortingService sortingService,
    IMapper mapper)
    : ControllerBase
{
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
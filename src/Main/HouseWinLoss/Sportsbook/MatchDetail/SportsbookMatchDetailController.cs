namespace HouseWinLoss.Sportsbook.MatchDetail;

using AutoMapper;
using Common.ColumnDecorators;
using Common.Criteria;
using Common.MatchDetail;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;
using Fanex.Excel.Models;
using DailyWinLoss2hSoccerMinor;

[Route("api/house-win-loss-by-products/sportsbook/match-detail")]
[ApiController]
public class SportsbookMatchDetailController(
    WinLossService service,
    GridOptionsBuilder builder,
    MatchGridOptionColumnDecoratorFull decorator,
    MatchSortingService sortingService,
    IMapper mapper)
    : ControllerBase
{
    [Route("")]
    public async Task<IActionResult> Json(QueryModel queryModel)
    {
        var gridOptions = await GetGridOptions(queryModel, string.Empty);
        return Ok(gridOptions);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var gridOptions = await GetGridOptions(queryModel, queryModel.Title);
        return new FanexExcelResult(gridOptions, Constants.SheetName);
    }

    private async Task<GridOptions> GetGridOptions(QueryModel queryModel, string title)
    {
        var criteria = mapper.Map<UniversalSportbookMatchDetailCriteria>(queryModel);
        var winlosses = await service.Query(criteria);
        
        var sortColumnName = queryModel.SortColumnName;
        var sortDirection= queryModel.SortDirection;

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
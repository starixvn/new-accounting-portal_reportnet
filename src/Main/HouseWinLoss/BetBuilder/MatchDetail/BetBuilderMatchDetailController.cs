namespace HouseWinLoss.BetBuilder.MatchDetail;

using AutoMapper;
using Common.ColumnDecorators;
using Common.Criteria;
using HouseWinLoss.Common.MatchDetail;
using Configuration;
using DailyWinLoss2hSoccerMinor;
using Microsoft.AspNetCore.Mvc;
using Fanex.Excel.Models;
using Utilities.ExcelResult;

[Route("api/house-win-loss-by-products/bet-builder/match-detail")]
[ApiController]
public class BetBuilderMatchDetailController(
    WinLossService service,
    GridOptionsBuilder builder,
    MatchGridOptionColumnDecoratorFull decorator,
    MatchSortingService sortingService,
    IMapper mapper)
    : ControllerBase()
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
        var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
        var winlosses = await service.Query(criteria);
        var sortColumnName = queryModel.SortColumnName;
        var sortDirection = queryModel.SortDirection;

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
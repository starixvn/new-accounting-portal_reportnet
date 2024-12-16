namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.Site;

using AutoMapper;
using Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using Common.SiteWinLoss;
using Configuration;
using Microsoft.AspNetCore.Mvc;

[Route("api/house-win-loss-by-products/soccer-minor/site")]
[ApiController]
public class SoccerMinorSiteController(
    WinLossService service,
    GridOptionsBuilder builder,
    GridOptionColumnDecoratorFull decorator,
    IMapper mapper)
    : SiteWinLossController(service, builder, decorator)
{
    [Route("")]
    public async Task<IActionResult> Json(QueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalSportbookSiteCriteria>(queryModel);
        return await Json(criteria, queryModel.SiteType);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalSportbookSiteCriteria>(queryModel);
        return await Excel(criteria, queryModel.SiteType, queryModel.Title, Constants.SoccerMinorBetTypesSheetName);
    }
}
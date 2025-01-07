using AutoMapper;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.MatchDetail;
using HouseWinLoss.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace HouseWinLoss.VirtualSportsBetradar.MatchDetail;

[Route("api/house-win-loss-by-products/virtual-sports-betradar/special-match-detail")]
[ApiController]
public class VirtualSportsBetradarSpecialMatchDetailController(
    WinLossService service,
    GridOptionsBuilder builder,
    IMapper mapper,
    MatchGridOptionColumnDecoratorVirtualSport decorator)
    : MatchDetailController(service, builder, decorator)
{
    [Route("")]
    public async Task<IActionResult> Json(QueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
        return await Json(criteria);
    }

    [Route("excel")]
    public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
    {
        var criteria = mapper.Map<UniversalThirdPartyMatchDetailCriteria>(queryModel);
        return await Excel(criteria, queryModel.Title, Constants.SheetName);
    }
}
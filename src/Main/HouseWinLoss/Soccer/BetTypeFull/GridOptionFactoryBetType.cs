using AutoMapper;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Soccer.BetType;

namespace HouseWinLoss.Soccer.BetTypeFull;

public class GridOptionFactoryBetType(
    BasicWinLossService basicWinLossService,
    BasicGridOptionsBuilder basicGridOptionsBuilder,
    GridOptionColumnDecorator decorator,
    IMapper mapper)
    : GridOptionFactory
{
    public async Task<GridOptions> CreateGridOption(ExcelQueryModel queryModel, string title, int isLive)
    {
        var criteria = mapper.Map<BetTypeCriteria>(queryModel);
        criteria.IsLive = isLive;

        var winlosses = await basicWinLossService.Query(criteria);

        var gridOptions = basicGridOptionsBuilder.Build(winlosses, title);

        return decorator.Build(gridOptions, "Bet Type");
    }
}
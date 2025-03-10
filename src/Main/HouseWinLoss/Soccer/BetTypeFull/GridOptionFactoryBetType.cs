namespace HouseWinLoss.Soccer.BetTypeFull;

using AutoMapper;
using BetType;
using Common.BasicWinLoss;
using Common.ColumnDecorators;
using Fanex.Excel.Models;

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
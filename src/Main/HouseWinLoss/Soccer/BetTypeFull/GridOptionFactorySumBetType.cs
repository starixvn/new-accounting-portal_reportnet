using AutoMapper;
using Fanex.Excel.Models;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Soccer.SumBetType;

namespace HouseWinLoss.Soccer.BetTypeFull;

public class GridOptionFactorySumBetType(
    SumBetTypeWinLossService sumBetTypeWinLossService,
    SumBetTypeGridOptionsBuilder sumBetTypeGridOptionsBuilder,
    GridOptionColumnDecorator decorator,
    IMapper mapper)
    : GridOptionFactory
{
    public async Task<GridOptions> CreateGridOption(ExcelQueryModel queryModel, string title, int isLive)
    {
        var criteria = mapper.Map<SumBetTypeCriteria>(queryModel);
        criteria.IsLive = isLive;

        var winlosses = await sumBetTypeWinLossService.Query(criteria);
        var gridOptions = sumBetTypeGridOptionsBuilder.Build(winlosses, title);

        return decorator.Build(gridOptions, "Bet Type");
    }
}
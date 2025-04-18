namespace HouseWinLoss.Soccer.BetTypeFull;

using AutoMapper;
using Common.ColumnDecorators;
using Fanex.Excel.Models;
using GroupBetType;

public class GridOptionFactoryGroupBetType(
    GroupBetTypeWinLossService groupBetTypeWinLossService,
    GroupBetTypeGridOptionsBuilder groupBetTypeGridOptionsBuilder,
    GridOptionColumnDecorator decorator,
    IMapper mapper)
    : GridOptionFactory
{
    public async Task<GridOptions> CreateGridOption(ExcelQueryModel queryModel, string title, int isLive)
    {
        var criteria = mapper.Map<GroupBetTypeCriteria>(queryModel);
        criteria.IsLive = isLive;

        var winlosses = await groupBetTypeWinLossService.Query(criteria);

        var gridOptions = groupBetTypeGridOptionsBuilder.Build(winlosses, title);

        return decorator.Build(gridOptions, "Bet Type");
    }
}
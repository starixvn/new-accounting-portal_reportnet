using AutoMapper;
using Fanex.Excel.Models;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Soccer.GroupBetType;

namespace HouseWinLoss.Soccer.BetTypeFull
{
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
}

using AutoMapper;
using Fanex.Excel.Models;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Soccer.GroupBetType;

namespace HouseWinLoss.Soccer.BetTypeFull
{
    public class GridOptionFactoryGroupBetType : GridOptionFactory
    {
        private readonly GroupBetTypeWinLossService groupBetTypeWinLossService;

        private readonly GroupBetTypeGridOptionsBuilder groupBetTypeGridOptionsBuilder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public GridOptionFactoryGroupBetType(
            GroupBetTypeWinLossService groupBetTypeWinLossService,
            GroupBetTypeGridOptionsBuilder groupBetTypeGridOptionsBuilder,
            GridOptionColumnDecorator decorator,
            IMapper mapper)
        {
            this.groupBetTypeWinLossService = groupBetTypeWinLossService;
            this.groupBetTypeGridOptionsBuilder = groupBetTypeGridOptionsBuilder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

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

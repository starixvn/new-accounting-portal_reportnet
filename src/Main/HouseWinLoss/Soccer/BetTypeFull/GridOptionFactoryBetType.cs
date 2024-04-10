using AutoMapper;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Soccer.BetType;

namespace HouseWinLoss.Soccer.BetTypeFull
{
    public class GridOptionFactoryBetType : GridOptionFactory
    {
        private readonly BasicWinLossService basicWinLossService;

        private readonly BasicGridOptionsBuilder basicGridOptionsBuilder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public GridOptionFactoryBetType(
            BasicWinLossService basicWinLossService,
            BasicGridOptionsBuilder basicGridOptionsBuilder,
            GridOptionColumnDecorator decorator,
            IMapper mapper)
        {
            this.basicWinLossService = basicWinLossService;
            this.basicGridOptionsBuilder = basicGridOptionsBuilder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        public async Task<GridOptions> CreateGridOption(ExcelQueryModel queryModel, string title, int isLive)
        {
            var criteria = mapper.Map<BetTypeCriteria>(queryModel);
            criteria.IsLive = isLive;

            var winlosses = await basicWinLossService.Query(criteria);

            var gridOptions = basicGridOptionsBuilder.Build(winlosses, title);

            return decorator.Build(gridOptions, "Bet Type");
        }
    }
}

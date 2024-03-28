using AutoMapper;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Soccer.SumBetType;

namespace Nap_Reports.HouseWinLoss.Soccer.BetTypeFull
{
    public class GridOptionFactorySumBetType : GridOptionFactory
    {
        private readonly SumBetTypeWinLossService sumBetTypeWinLossService;

        private readonly SumBetTypeGridOptionsBuilder sumBetTypeGridOptionsBuilder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public GridOptionFactorySumBetType(
            SumBetTypeWinLossService sumBetTypeWinLossService,
            SumBetTypeGridOptionsBuilder sumBetTypeGridOptionsBuilder,
            GridOptionColumnDecorator decorator,
            IMapper mapper)
        {
            this.sumBetTypeWinLossService = sumBetTypeWinLossService;
            this.sumBetTypeGridOptionsBuilder = sumBetTypeGridOptionsBuilder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        public async Task<GridOptions> CreateGridOption(ExcelQueryModel queryModel, string title, int isLive)
        {
            var criteria = mapper.Map<SumBetTypeCriteria>(queryModel);
            criteria.IsLive = isLive;

            var winlosses = await sumBetTypeWinLossService.Query(criteria);
            var gridOptions = sumBetTypeGridOptionsBuilder.Build(winlosses, title);

            return decorator.Build(gridOptions, "Bet Type");
        }
    }
}

namespace HouseWinLoss.Soccer.BetTypeFull
{
    using AutoMapper;
    using HouseWinLoss.Common.BasicWinLoss;
    using HouseWinLoss.Common.ColumnDecorators;
    using HouseWinLoss.Configuration;
    using HouseWinLoss.Soccer.GroupBetType;
    using HouseWinLoss.Soccer.SumBetType;

    public interface AbstractFactory
    {
        GridOptionFactory CreateGridOptionFactory(bool isMajor, int isLive);
    }

    public class AbstractFactoryImp : AbstractFactory
    {
        private readonly SumBetTypeWinLossService sumBetTypeWinLossService;

        private readonly SumBetTypeGridOptionsBuilder sumBetTypeGridOptionsBuilder;

        private readonly GroupBetTypeWinLossService groupBetTypeWinLossService;

        private readonly GroupBetTypeGridOptionsBuilder groupBetTypeGridOptionsBuilder;

        private readonly BasicWinLossService basicWinLossService;

        private readonly BasicGridOptionsBuilder basicGridOptionsBuilder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public AbstractFactoryImp(
            SumBetTypeWinLossService sumBetTypeWinLossService,
            SumBetTypeGridOptionsBuilder sumBetTypeGridOptionsBuilder,
            GroupBetTypeWinLossService groupBetTypeWinLossService,
            GroupBetTypeGridOptionsBuilder groupBetTypeGridOptionsBuilder,
            BasicWinLossService basicWinLossService,
            BasicGridOptionsBuilder basicGridOptionsBuilder,
            GridOptionColumnDecoratorFull decorator,
            IMapper mapper)
        {
            this.sumBetTypeWinLossService = sumBetTypeWinLossService;
            this.sumBetTypeGridOptionsBuilder = sumBetTypeGridOptionsBuilder;
            this.groupBetTypeWinLossService = groupBetTypeWinLossService;
            this.groupBetTypeGridOptionsBuilder = groupBetTypeGridOptionsBuilder;
            this.basicWinLossService = basicWinLossService;
            this.basicGridOptionsBuilder = basicGridOptionsBuilder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        public GridOptionFactory CreateGridOptionFactory(bool isMajor, int isLive)
        {
            if (isMajor && isLive == Constants.LiveDefault)
            {
                return new GridOptionFactorySumBetType(sumBetTypeWinLossService, sumBetTypeGridOptionsBuilder, decorator, mapper);
            }

            if (isMajor && isLive == 0)
            {
                return new GridOptionFactoryGroupBetType(groupBetTypeWinLossService, groupBetTypeGridOptionsBuilder, decorator, mapper);
            }
            else
            {
                return new GridOptionFactoryBetType(basicWinLossService, basicGridOptionsBuilder, decorator, mapper);
            }
        }
    }
}

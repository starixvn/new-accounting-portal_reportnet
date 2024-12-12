namespace HouseWinLoss.Soccer.BetTypeFull
{
    using AutoMapper;
    using Common.BasicWinLoss;
    using Common.ColumnDecorators;
    using Configuration;
    using GroupBetType;
    using SumBetType;

    public interface AbstractFactory
    {
        GridOptionFactory CreateGridOptionFactory(bool isMajor, int isLive);
    }

    public class AbstractFactoryImp(
        SumBetTypeWinLossService sumBetTypeWinLossService,
        SumBetTypeGridOptionsBuilder sumBetTypeGridOptionsBuilder,
        GroupBetTypeWinLossService groupBetTypeWinLossService,
        GroupBetTypeGridOptionsBuilder groupBetTypeGridOptionsBuilder,
        BasicWinLossService basicWinLossService,
        BasicGridOptionsBuilder basicGridOptionsBuilder,
        GridOptionColumnDecoratorFull decorator,
        IMapper mapper)
        : AbstractFactory
    {
        private readonly GridOptionColumnDecorator decorator = decorator;

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

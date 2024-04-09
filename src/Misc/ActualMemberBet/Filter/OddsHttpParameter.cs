using MediatR;

namespace ActualMemberBet.Filter
{
    public class OddsHttpParameter : IRequest<IEnumerable<Odds>>
    {
    }
}

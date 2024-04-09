using System.Collections.Generic;
using MediatR;

namespace ActualMemberBet.Filter
{
    public class CurrencyHttpParameter : IRequest<IEnumerable<Currency>>
    {
    }
}

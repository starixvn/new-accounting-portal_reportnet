namespace ActualMemberBet.Filter;

using MediatR;

public class CurrencyHttpParameter : IRequest<IEnumerable<Currency>>;
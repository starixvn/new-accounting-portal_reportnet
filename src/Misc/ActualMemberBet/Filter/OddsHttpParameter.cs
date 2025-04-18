namespace ActualMemberBet.Filter;

using MediatR;

public class OddsHttpParameter : IRequest<IEnumerable<Odds>>;
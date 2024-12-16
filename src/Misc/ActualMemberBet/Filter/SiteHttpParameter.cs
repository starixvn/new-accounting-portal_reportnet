using MediatR;

namespace ActualMemberBet.Filter;

public class SiteHttpParameter : IRequest<IEnumerable<Site>>;
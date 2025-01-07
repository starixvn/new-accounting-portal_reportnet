using MediatR;

namespace SMAWinLoss.Filter;

public class SiteHttpParameter : IRequest<IEnumerable<Site>>;
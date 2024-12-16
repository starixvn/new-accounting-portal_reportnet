using MediatR;

namespace SMAWinLoss.Filter;

public class BetFromHttpParameter : IRequest<IEnumerable<BetFrom>>;
using MediatR;

namespace SMAWinLoss.Filter;

public class SportHttpParameter : IRequest<IEnumerable<Sport>>;
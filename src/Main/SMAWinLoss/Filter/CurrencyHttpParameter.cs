using MediatR;

namespace SMAWinLoss.Filter
{
    public class CurrencyHttpParameter : IRequest<IEnumerable<Currency>>
    {
    }
}
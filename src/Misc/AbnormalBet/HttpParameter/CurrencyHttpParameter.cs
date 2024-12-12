using AbnormalBet.Model;
using MediatR;

namespace AbnormalBet.HttpParameter
{
    public class CurrencyHttpParameter : IRequest<IEnumerable<FilterItemModel>>;
}
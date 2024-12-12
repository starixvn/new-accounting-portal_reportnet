using AbnormalBet.Model;
using MediatR;

namespace AbnormalBet.HttpParameter
{
    public class SportHttpParameter : IRequest<IEnumerable<FilterItemModel>>;
}

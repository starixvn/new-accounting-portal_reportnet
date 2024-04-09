using AbnormalBet.Model;
using MediatR;

namespace AbnormalBet.HttpParameter
{
    public class BetTypeHttpParameter : IRequest<IEnumerable<FilterItemModel>>
    {
        public string SportId { get; set; }

    }
}

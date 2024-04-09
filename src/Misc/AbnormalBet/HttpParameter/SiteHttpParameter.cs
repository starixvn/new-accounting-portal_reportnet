using AbnormalBet.Model;
using MediatR;

namespace AbnormalBet.HttpParameter
{
    public class SiteHttpParameter : IRequest<IEnumerable<SiteModel>>
    {
    }
}

using CashOutPerformance.Models;
using MediatR;

namespace CashOutPerformance.Requests
{
    public class LeagueGroupRequest : IRequest<IEnumerable<FilterItemModel>>;
}
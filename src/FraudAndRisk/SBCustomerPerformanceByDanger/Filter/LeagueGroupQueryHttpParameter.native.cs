using MediatR;

namespace SBCustomerPerformanceByDanger.Filter
{
    public class LeagueGroupQueryHttpParameter : IRequest<IEnumerable<LeagueGroupModel>>
    {
        public string SportBettype { get; set; }
    }
}
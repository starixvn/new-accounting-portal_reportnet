using MediatR;

namespace SBCustomerPerformanceByDanger.Filter
{
    public class LeagueQueryHttpParameter : IRequest<IEnumerable<string>>
    {
        public string SportBettype { get; set; }

        public string SearchText { get; set; }
    }
}
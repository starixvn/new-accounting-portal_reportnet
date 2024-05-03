using MediatR;
using System.Collections.Generic;

namespace SBCustomerPerformanceByDanger.Filter
{
    public class LeagueQueryHttpParameter : IRequest<IEnumerable<string>>
    {
        public string SportBettype { get; set; }

        public string SearchText { get; set; }
    }
}
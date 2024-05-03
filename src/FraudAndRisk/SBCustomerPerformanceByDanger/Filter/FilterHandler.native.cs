using AutoMapper;
using MediatR;

namespace SBCustomerPerformanceByDanger.Filter
{
    public class FilterHandler : IRequestHandler<BalanceUpDateQueryHttpParameter, DateTime>,
        IRequestHandler<SportQueryHttpParameter, List<SportModel>>,
        IRequestHandler<DivisionQueryHttpParameter, IEnumerable<DivisionModel>>,
        IRequestHandler<LeagueGroupQueryHttpParameter, IEnumerable<LeagueGroupModel>>,
        IRequestHandler<LeagueQueryHttpParameter, IEnumerable<string>>
        
    {
        private readonly FilterService filterService;

        private readonly IMapper mapper;    

        public FilterHandler(FilterService filterService, IMapper mapper)
        {
            this.filterService = filterService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<LeagueGroupModel>> Handle(LeagueGroupQueryHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<LeagueGroupQueryCriteria>(request);
            return await filterService.GetLeagueGroup(criteria);
        }

        public async Task<IEnumerable<string>> Handle(LeagueQueryHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<LeagueQueryCriteria>(request);
            return await filterService.GetLeagueSuggestion(criteria);
        }

        public async Task<IEnumerable<DivisionModel>> Handle(DivisionQueryHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<DivisionQueryCriteria>(request);
            return await filterService.GetDivision(criteria);
        }

        public async Task<List<SportModel>> Handle(SportQueryHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<SportQueryCriteria>(request);
            return await filterService.GetSport(criteria);
        }

        public async Task<DateTime> Handle(BalanceUpDateQueryHttpParameter request, CancellationToken cancellationToken)
        {
            var balanceUpDate = filterService.GetBalanceUpDate();
            return await Task.FromResult(balanceUpDate);
        }
    }
}
using AutoMapper;
using CashOutPerformance.Criteria;
using CashOutPerformance.Models;
using CashOutPerformance.Requests;
using CashOutPerformance.Services;
using CashOutPerformance.ViewModels;
using MediatR;

namespace CashOutPerformance.Handlers
{
    public class Handler(CashOutPerformanceService service, IMapper mapper)
        : IRequestHandler<SportRequest, IEnumerable<FilterGroupItemModel>>,
            IRequestHandler<LicenseeRequest, IEnumerable<SiteModel>>,
            IRequestHandler<SportLevelRequest, SportLevelModel>,
            IRequestHandler<TicketRequest<EventStatusLevelModel>, TicketViewModel<EventStatusLevelModel>>,
            IRequestHandler<TicketRequest<BetTypeLevelModel>, TicketViewModel<BetTypeLevelModel>>,
            IRequestHandler<TicketRequest<MatchDetailLevelModel>, TicketViewModel<MatchDetailLevelModel>>,
            IRequestHandler<LeagueRequest, IEnumerable<LeagueModel>>,
            IRequestHandler<LeagueGroupRequest, IEnumerable<FilterItemModel>>
    {
        public async Task<IEnumerable<FilterGroupItemModel>> Handle(SportRequest request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<SportCriteria>(request);
            return await service.GetSportBettypes(criteria);
        }

        public async Task<IEnumerable<SiteModel>> Handle(LicenseeRequest request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<LicenseeCriteria>(request);
            return await service.GetLicensees(criteria);
        }

        public async Task<SportLevelModel> Handle(SportLevelRequest request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<SportLevelCriteria>(request);
            return await service.GetSportLevel(criteria);
        }

        public async Task<TicketViewModel<EventStatusLevelModel>> Handle(TicketRequest<EventStatusLevelModel> request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<EventStatusLevelCriteria>(request);
            return await service.GetTickets<EventStatusLevelModel, EventStatusLevelCriteria>(criteria);
        }

        public async Task<TicketViewModel<BetTypeLevelModel>> Handle(TicketRequest<BetTypeLevelModel> request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<BetTypeLevelCriteria>(request);
            return await service.GetTickets<BetTypeLevelModel, BetTypeLevelCriteria>(criteria);
        }

        public async Task<TicketViewModel<MatchDetailLevelModel>> Handle(TicketRequest<MatchDetailLevelModel> request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<MatchDetailLevelCriteria>(request);
            return await service.GetTickets<MatchDetailLevelModel, MatchDetailLevelCriteria>(criteria);
        }

        public async Task<IEnumerable<LeagueModel>> Handle(LeagueRequest request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<LeagueCriteria>(request);
            return await service.GetLeagues(criteria);
        }

        public async Task<IEnumerable<FilterItemModel>> Handle(LeagueGroupRequest request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<LeagueGroupCriteria>(request);
            return await service.GetLeagueGroups(criteria);
        }
    }
}
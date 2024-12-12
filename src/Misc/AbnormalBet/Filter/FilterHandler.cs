using AbnormalBet.HttpParameter;
using AbnormalBet.Model;
using MediatR;

namespace AbnormalBet.Filter
{
    public class FilterHandler(FilterService filterService)
        : IRequestHandler<SiteHttpParameter, IEnumerable<SiteModel>>,
            IRequestHandler<SportHttpParameter, IEnumerable<FilterItemModel>>,
            IRequestHandler<BetTypeHttpParameter, IEnumerable<FilterItemModel>>,
            IRequestHandler<ReasonHttpParameter, IEnumerable<FilterItemModel>>,
            IRequestHandler<CurrencyHttpParameter, IEnumerable<FilterItemModel>>,
            IRequestHandler<LeagueHttpParameter, IEnumerable<LeagueModel>>
    {
        public async Task<IEnumerable<FilterItemModel>> Handle(CurrencyHttpParameter request, CancellationToken cancellationToken)
        {
            return await filterService.QueryCurrency();
        }

        public async Task<IEnumerable<LeagueModel>> Handle(LeagueHttpParameter request, CancellationToken cancellationToken)
        {
            return await filterService.QueryLeague(request.SportId, request.SearchText, request.SearchExactly);
        }

        public async Task<IEnumerable<FilterItemModel>> Handle(ReasonHttpParameter request, CancellationToken cancellationToken)
        {
            return await filterService.QueryReason();
        }

        public async Task<IEnumerable<FilterItemModel>> Handle(BetTypeHttpParameter request, CancellationToken cancellationToken)
        {
            return await filterService.QueryBetType(request.SportId);
        }

        public async Task<IEnumerable<FilterItemModel>> Handle(SportHttpParameter request, CancellationToken cancellationToken)
        {
            return await filterService.QuerySport();
        }

        public async Task<IEnumerable<SiteModel>> Handle(SiteHttpParameter request, CancellationToken cancellationToken)
        {
            return await filterService.QuerySite();
        }
    }
}
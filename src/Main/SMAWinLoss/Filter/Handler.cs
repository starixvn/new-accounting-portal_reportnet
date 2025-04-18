namespace SMAWinLoss.Filter;

using MediatR;
using Utilities.PermissionFilter;

public class Handler(FilterService service, PermissionFilterService permissionService)
    : IRequestHandler<CurrencyHttpParameter, IEnumerable<Currency>>,
        IRequestHandler<SiteHttpParameter, IEnumerable<Site>>,
        IRequestHandler<SportHttpParameter, IEnumerable<Sport>>,
        IRequestHandler<LeagueGroupHttpParameter, LeagueGroupModel>,
        IRequestHandler<AvailableDatabaseHttpParameter, bool>,
        IRequestHandler<AvailableDateDataHttpParameter, DateTime>,
        IRequestHandler<BetFromHttpParameter, IEnumerable<BetFrom>>,
        IRequestHandler<PermissionHttpParameter, int>
{
    public async Task<LeagueGroupModel> Handle(LeagueGroupHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.GetLeagueGroup();
    }

    public async Task<DateTime> Handle(AvailableDateDataHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.GetAvailableDateData();
    }

    public async Task<IEnumerable<Sport>> Handle(SportHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.GetSports();
    }

    public async Task<IEnumerable<Site>> Handle(SiteHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.GetSites();
    }

    public async Task<IEnumerable<Currency>> Handle(CurrencyHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.GetCurrencies();
    }

    public async Task<bool> Handle(AvailableDatabaseHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.GetAvailableDatabase();
    }

    public async Task<IEnumerable<BetFrom>> Handle(BetFromHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.GetBetFroms();
    }

    public async Task<int> Handle(PermissionHttpParameter request, CancellationToken cancellationToken)
    {
        return await permissionService.GetPermissionProfileId(request.FunctionKey);
    }
}
using AutoMapper;
using CashOutPerformance.Constants;
using CashOutPerformance.Criteria;
using CashOutPerformance.Models;
using CashOutPerformance.Requests;
using CashOutPerformance.Services;
using CashOutPerformance.ViewModels;
using MediatR;

namespace CashOutPerformance.Handlers;

public class Handler(CashOutPerformanceService service, IMapper mapper, ModelsHelperService modelsHelperService)
    : IRequestHandler<SportRequest, IEnumerable<FilterGroupItemModel>>,
        IRequestHandler<LicenseeRequest, IEnumerable<SiteModel>>,
        IRequestHandler<SportLevelRequest, SportLevelModel>,
        IRequestHandler<TicketRequest<EventStatusLevelModel>, TicketViewModel<EventStatusLevelModel>>,
        IRequestHandler<TicketRequest<BetTypeLevelModel>, TicketViewModel<BetTypeLevelModel>>,
        IRequestHandler<TicketRequest<MatchDetailLevelModel>, TicketViewModel<MatchDetailLevelModel>>,
        IRequestHandler<LeagueRequest, IEnumerable<LeagueModel>>,
        IRequestHandler<LeagueGroupRequest, IEnumerable<FilterItemModel>>,
        IRequestHandler<TicketRequest<SimpleBetTypeLevelModel>, TicketViewModel<SimpleBetTypeLevelModel>>,
        IRequestHandler<BetTypeDrillDownRequest, TicketViewModel<BetTypeLevelModel>>
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
        var data = await service.GetTickets<BetTypeLevelModel, BetTypeLevelCriteria>(criteria);
        var result = modelsHelperService.UpdateBetTypeViewModel(data);
        return result;
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

    public async Task<TicketViewModel<BetTypeLevelModel>> Handle(BetTypeDrillDownRequest request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<BetTypeLevelCriteria>(request);
        var rawData = await service.GetTickets<BetTypeLevelModel, BetTypeLevelCriteria>(criteria);
        TicketViewModel<BetTypeLevelModel> data;
        var gameTimeCriteria = mapper.Map<GameTimeCriteria>(request);
        switch (request.Type)
        {
            case BetTypeDrillDownTypeConstants.GameTime:
                data = modelsHelperService.UpdateGameTimeLevelModel(rawData.TicketList.ToList(), gameTimeCriteria);
                break;
            case BetTypeDrillDownTypeConstants.DetailGameTime:
                data = modelsHelperService.UpdateDetailGameTimeLevelModel(rawData.TicketList.ToList(), gameTimeCriteria);
                break;
            default:
                data = modelsHelperService.UpdateWinningProbabilityLevelModel(rawData.TicketList.ToList(), gameTimeCriteria);
                break;
        }

        return data;
    }

    public async Task<TicketViewModel<SimpleBetTypeLevelModel>> Handle(TicketRequest<SimpleBetTypeLevelModel> request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<BetTypeLevelCriteria>(request);
        var data = await service.GetTickets<SimpleBetTypeLevelModel, BetTypeLevelCriteria>(criteria);
        data.TicketList = data.TicketList.OrderByDescending(s => s.TotalIncome_RM);
        return data;
    }
}
namespace PunterAnalysis.Performance.Summary;

using AutoMapper;
using Criteria;
using Fanex.Data.Repository;
using MediatR;
using Model;

public class Handler(IDynamicRepository repository, IMapper mapper)
    : IRequestHandler<SummaryHttpParameterJson, ViewModelJson>
{
    public async Task<ViewModelJson> Handle(SummaryHttpParameterJson request, CancellationToken cancellationToken)
    {
        var viewModel = new ViewModelJson
        {
            MonthStatistics = await Query<MonthStatistic, MonthCriteria>(request),
            ProductTurnoverStatistics = await Query<ProductTurnoverStatistic, ProductTurnoverCriteria>(request),
            SportStatistics = await Query<SportStatistic, SportCriteria>(request),
            BetTypeStatistics = await Query<BetTypeStatistic, BetTypeCriteria>(request),
            EventStatistics = await Query<EventStatistic, EventCriteria>(request)
        };

        viewModel.SummaryStatistic = QuerySummaryStatistic(viewModel.MonthStatistics);

        return viewModel;
    }

    private SummaryStatistic QuerySummaryStatistic(List<MonthStatistic> monthStatistics)
    {
        return new SummaryStatistic
        {
            BetCount = monthStatistics.Sum(m => m.BetCount),
            Turnover = monthStatistics.Sum(m => m.Turnover),
            WinLoss = monthStatistics.Sum(m => m.WinLoss)
        };
    }

    private async Task<List<TResult>> Query<TResult, TCriteria>(SummaryHttpParameterJson message) where TCriteria : ICriteria
    {
        var criteria = mapper.Map<TCriteria>(message);
        var entities = await repository.FetchAsync<TResult>(criteria);
        return entities.ToList();
    }
}
using AutoMapper;
using Fanex.Data.Repository;
using MediatR;
using PunterAnalysis.Performance.Sportsbook.Criteria;
using PunterAnalysis.Performance.Sportsbook.Model;

namespace PunterAnalysis.Performance.Sportsbook
{
    public class Handler : IRequestHandler<SportsbookHttpParameterJson, ViewModelJson>
    {
        private readonly IDynamicRepository repository;

        private readonly IMapper mapper;

        public Handler(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ViewModelJson> Handle(SportsbookHttpParameterJson request, CancellationToken cancellationToken)
        {
            var viewModel = new ViewModelJson();
            viewModel.SportStatistics = await Query<SportStatistic, SportCriteria>(request);
            viewModel.SummaryStatistic = QuerySummaryStatistics(viewModel.SportStatistics);
            return viewModel;
        }

        private SummaryStatistic QuerySummaryStatistics(List<SportStatistic> sportStatistics)
        {
            return new SummaryStatistic
            {
                BetCount = sportStatistics.Sum(m => m.BetCount),
                Turnover = sportStatistics.Sum(m => m.Turnover),
                WinLoss = sportStatistics.Sum(m => m.WinLoss),
            };
        }

        private async Task<List<TResult>> Query<TResult, TCriteria>(SportsbookHttpParameterJson message) where TCriteria : ICriteria
        {
            var criteria = mapper.Map<TCriteria>(message);
            var entities = await repository.FetchAsync<TResult>(criteria);
            return entities.ToList();
        }
    }
}
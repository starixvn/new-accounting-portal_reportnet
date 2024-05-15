using AutoMapper;
using Fanex.Data.Repository;
using MediatR;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Shared
{
    public class HandlerBase
    {
        private readonly PunterAnalysisService punterAnalysisService;

        private readonly IMapper mapper;

        protected HandlerBase(PunterAnalysisService punterAnalysisService, IMapper mapper)
        {
            this.punterAnalysisService = punterAnalysisService;
            this.mapper = mapper;
        }

        protected async Task<SummaryEntity<TModel>> Handle<TCriteria, TModel>(IRequest<SummaryEntity<TModel>> message)
            where TCriteria : ICriteria
            where TModel : class, new()
        {
            var criteria = mapper.Map<TCriteria>(message);
            return await punterAnalysisService.Query<TModel>(criteria);
        }
    }
}
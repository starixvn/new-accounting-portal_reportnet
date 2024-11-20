using AutoMapper;
using Fanex.Data.Repository;
using Fanex.NAP.Client;
using MediatR;

namespace PunterAnalysis.Filter.UsernameSuggestion
{
    public class Handler : IRequestHandler<HttpParameterJson, IEnumerable<string>>
    {
        private readonly IDynamicRepository repository;

        private readonly INapSession napSession;

        private readonly IMapper mapper;

        public Handler(INapSession napSession, IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.napSession = napSession;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<string>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = mapper.Map<QueryCriteria>(request);
            criteria.CustId = napSession.UserId;
            return await repository.FetchAsync<string>(criteria);
        }
    }
}
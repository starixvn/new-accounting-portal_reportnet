using AutoMapper;
using Fanex.Data.Repository;
using Fanex.NAP.Client;
using MediatR;

namespace PunterAnalysis.Filter.UsernameSuggestion;

public class Handler(INapSession napSession, IDynamicRepository repository, IMapper mapper)
    : IRequestHandler<HttpParameterJson, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<QueryCriteria>(request);
        criteria.CustId = napSession.UserId;
        return await repository.FetchAsync<string>(criteria);
    }
}
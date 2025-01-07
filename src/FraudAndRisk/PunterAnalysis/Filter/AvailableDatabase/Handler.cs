using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.AvailableDatabase;

public class Handler(IDynamicRepository repository) : IRequestHandler<HttpParameterJson, bool>
{
    public async Task<bool> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = new QueryCriteria();
        return await repository.GetAsync<bool>(criteria);
    }
}
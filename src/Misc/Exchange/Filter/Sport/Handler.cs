namespace Exchange.Filter.Sport;

using Fanex.Data.Repository;
using MediatR;

public class Handler(IDynamicRepository repository) : IRequestHandler<HttpParameterJson, IEnumerable<SportModel>>
{
    public async Task<IEnumerable<SportModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = new Criteria();
        return await repository.FetchAsync<SportModel>(criteria);
    }
}
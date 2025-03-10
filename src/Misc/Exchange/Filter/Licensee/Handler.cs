namespace Exchange.Filter.Licensee;

using Fanex.Data.Repository;
using MediatR;

public class Handler(IDynamicRepository repository) : IRequestHandler<HttpParameterJson, IEnumerable<LicenseeModel>>
{
    public async Task<IEnumerable<LicenseeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = new Criteria();
        return await repository.FetchAsync<LicenseeModel>(criteria);
    }
}
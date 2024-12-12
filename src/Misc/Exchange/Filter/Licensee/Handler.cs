using Fanex.Data.Repository;
using MediatR;

namespace Exchange.Filter.Licensee
{
    public class Handler(IDynamicRepository repository) : IRequestHandler<HttpParameterJson, IEnumerable<LicenseeModel>>
    {
        public async Task<IEnumerable<LicenseeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = new Criteria();
            return await repository.FetchAsync<LicenseeModel>(criteria);
        }
    }
}
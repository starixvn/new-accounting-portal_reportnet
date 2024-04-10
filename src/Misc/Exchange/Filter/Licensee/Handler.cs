using Fanex.Data.Repository;
using MediatR;

namespace Exchange.Filter.Licensee
{
    public class Handler : IRequestHandler<HttpParameterJson, IEnumerable<LicenseeModel>>
    {
        private readonly IDynamicRepository repository;

        public Handler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<LicenseeModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = new Criteria();
            return await repository.FetchAsync<LicenseeModel>(criteria);
        }
    }
}
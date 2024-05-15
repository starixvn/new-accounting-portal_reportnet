using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.AvailableDatabase
{
    public class Handler : IRequestHandler<HttpParameterJson, bool>
    {
        private readonly IDynamicRepository repository;

        public Handler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = new QueryCriteria();
            return await repository.GetAsync<bool>(criteria);
        }
    }
}
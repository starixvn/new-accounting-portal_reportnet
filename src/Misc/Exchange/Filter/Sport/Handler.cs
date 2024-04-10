using Fanex.Data.Repository;
using MediatR;

namespace Exchange.Filter.Sport
{
    public class Handler : IRequestHandler<HttpParameterJson, IEnumerable<SportModel>>
    {
        private readonly IDynamicRepository repository;

        public Handler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<SportModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = new Criteria();
            return await repository.FetchAsync<SportModel>(criteria);
        }
    }
}
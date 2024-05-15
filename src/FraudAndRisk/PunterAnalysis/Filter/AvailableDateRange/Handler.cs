using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.AvailableDateRange
{
    public class Handler : IRequestHandler<HttpParameterJson, DateRange>
    {
        private readonly IDynamicRepository repository;

        public Handler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DateRange> Handle(HttpParameterJson request, CancellationToken cancellationToken)
        {
            var criteria = new QueryCriteria();
            return await repository.GetAsync<DateRange>(criteria);
        }
    }
}
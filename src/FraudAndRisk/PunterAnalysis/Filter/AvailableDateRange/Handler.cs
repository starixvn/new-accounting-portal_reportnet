using Fanex.Data.Repository;
using MediatR;

namespace PunterAnalysis.Filter.AvailableDateRange;

public class Handler(IDynamicRepository repository) : IRequestHandler<HttpParameterJson, DateRange>
{
    public async Task<DateRange> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = new QueryCriteria();
        return await repository.GetAsync<DateRange>(criteria);
    }
}
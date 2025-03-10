namespace PunterAnalysis.Filter.AvailableDateRange;

using Fanex.Data.Repository;
using MediatR;

public class Handler(IDynamicRepository repository) : IRequestHandler<HttpParameterJson, DateRange>
{
    public async Task<DateRange> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        var criteria = new QueryCriteria();
        return await repository.GetAsync<DateRange>(criteria);
    }
}
using MediatR;
using Utilities.LinqDynamicSum;

namespace Exchange.Report;

public class Handler(Service service) : IRequestHandler<LicenseeHttpParameterJson, ExchangeModel>,
    IRequestHandler<HttpParameterJson, SummaryEntity<ExchangeItemModel>>
{
    public async Task<ExchangeModel> Handle(LicenseeHttpParameterJson request, CancellationToken cancellationToken)
    {

        return await service.QueryLicensee(request);
    }

    public async Task<SummaryEntity<ExchangeItemModel>> Handle(HttpParameterJson request, CancellationToken cancellationToken)
    {
        return await service.Query(request);
    }
}
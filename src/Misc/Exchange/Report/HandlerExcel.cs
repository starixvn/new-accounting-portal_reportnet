namespace Exchange.Report;

using AutoMapper;
using MediatR;
using OfficeOpenXml;
using Utilities.LinqDynamicSum;

public class HandlerExcel(Service service, ExcelBuilder excelBuilder, IMapper mapper)
    : IRequestHandler<HttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
    {
        var jsonMessage = mapper.Map<HttpParameterJson>(request);
        var exchange = await service.Query(jsonMessage);
        var exchangeExcelModel = mapper.Map<SummaryEntity<ExchangeExcelEntity>>(exchange);
        var excelPackage = excelBuilder.Build(request, exchangeExcelModel);
        return excelPackage;
    }
}
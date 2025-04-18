namespace Exchange.Report;

using AutoMapper;
using MediatR;
using OfficeOpenXml;

public class LicenseeHandlerExcel(Service service, LicenseeExcelBuilder excelBuilder, IMapper mapper)
    : IRequestHandler<LicenseeHttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(LicenseeHttpParameterExcel request, CancellationToken cancellationToken)
    {
        var jsonMessage = mapper.Map<LicenseeHttpParameterJson>(request);
        var exchange = await service.QueryLicensee(jsonMessage);
        var exchangeExcelModel = mapper.Map<ExchangeExcelModel>(exchange);
        var excelPackage = excelBuilder.Build(request, exchangeExcelModel);
        return excelPackage;
    }
}
using AutoMapper;
using MediatR;
using OfficeOpenXml;

namespace Exchange.Report
{
    public class LicenseeHandlerExcel : IRequestHandler<LicenseeHttpParameterExcel, ExcelPackage>
    {
        private readonly Service service;

        private readonly LicenseeExcelBuilder excelBuilder;

        private readonly IMapper mapper;

        public LicenseeHandlerExcel(Service service, LicenseeExcelBuilder excelBuilder, IMapper mapper)
        {
            this.service = service;
            this.excelBuilder = excelBuilder;
            this.mapper = mapper;
        }

        public async Task<ExcelPackage> Handle(LicenseeHttpParameterExcel request, CancellationToken cancellationToken)
        {
            var jsonMessage = mapper.Map<LicenseeHttpParameterJson>(request);
            var exchange = await service.QueryLicensee(jsonMessage);
            var exchangeExcelModel = mapper.Map<ExchangeExcelModel>(exchange);
            var excelPackage = excelBuilder.Build(request, exchangeExcelModel);
            return excelPackage;
        }
    }
}
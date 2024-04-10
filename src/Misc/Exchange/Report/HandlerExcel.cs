using AutoMapper;
using MediatR;
using OfficeOpenXml;
using Utilities.LinqDynamicSum;

namespace Exchange.Report
{
    public class HandlerExcel : IRequestHandler<HttpParameterExcel, ExcelPackage>
    {
        private readonly Service service;

        private readonly ExcelBuilder excelBuilder;

        private readonly IMapper mapper;

        public HandlerExcel(Service service, ExcelBuilder excelBuilder, IMapper mapper)
        {
            this.service = service;
            this.excelBuilder = excelBuilder;
            this.mapper = mapper;
        }

        public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
        {
            var jsonMessage = mapper.Map<HttpParameterJson>(request);
            var exchange = await service.Query(jsonMessage);
            var exchangeExcelModel = mapper.Map<SummaryEntity<ExchangeExcelEntity>>(exchange);
            var excelPackage = excelBuilder.Build(request, exchangeExcelModel);
            return excelPackage;
        }
    }
}
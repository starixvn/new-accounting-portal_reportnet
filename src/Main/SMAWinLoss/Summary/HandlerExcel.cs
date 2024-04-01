using AutoMapper;
using MediatR;
using OfficeOpenXml;

namespace SMAWinLoss.Summary
{
    public class HandlerExcel : IRequestHandler<WinLossHttpParameterExcel, ExcelPackage>
    {
        private readonly IMapper mapper;

        private readonly WinLossService service;

        private readonly ExcelBuilder excelBuilder;

        public HandlerExcel(IMapper mapper,WinLossService service, ExcelBuilder excelBuilder)
        {
            this.mapper = mapper;
            this.service = service;
            this.excelBuilder = excelBuilder;
        }

        public async Task<ExcelPackage> Handle(WinLossHttpParameterExcel request, CancellationToken cancellationToken)
        {
            var jsonMessage = mapper.Map<WinLossHttpParameter>(request);
            var winlosses = await service.GetSMAWinLoss(jsonMessage);
            var excelPackage = excelBuilder.Build(request.Year, request.Title, winlosses);
            return excelPackage;
        }
    }
}
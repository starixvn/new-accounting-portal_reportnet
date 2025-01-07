using AutoMapper;
using MediatR;
using OfficeOpenXml;

namespace SMAWinLoss.Summary;

public class HandlerExcel(IMapper mapper, WinLossService service, ExcelBuilder excelBuilder)
    : IRequestHandler<WinLossHttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(WinLossHttpParameterExcel request, CancellationToken cancellationToken)
    {
        var jsonMessage = mapper.Map<WinLossHttpParameter>(request);
        var winlosses = await service.GetSMAWinLoss(jsonMessage);
        var excelPackage = excelBuilder.Build(request.Year, request.Title, winlosses);
        return excelPackage;
    }
}
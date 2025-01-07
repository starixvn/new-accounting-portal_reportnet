using ActualMemberBet.Common;
using AutoMapper;
using MediatR;
using Utilities.LinqDynamicSum;
using OfficeOpenXml;

namespace ActualMemberBet.Bettype;

public class HandlerExcel(ActualMemberBetService service, BettypeExcelBuilder excelBuilder, IMapper mapper)
    : IRequestHandler<HttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        var bettypeActualMemberBet = await service.Query<BetTypeActualMemberBet>(criteria);
        var bettypeActualMemberBetExcel = mapper.Map<SummaryEntity<BetTypeActualMemberBetExcel>>(bettypeActualMemberBet);
        var excelPackage = excelBuilder.Build(request, bettypeActualMemberBetExcel);
        return excelPackage;
    }
}
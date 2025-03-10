namespace ActualMemberBet.Bettype;

using AutoMapper;
using Common;
using MediatR;
using OfficeOpenXml;
using Utilities.LinqDynamicSum;

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
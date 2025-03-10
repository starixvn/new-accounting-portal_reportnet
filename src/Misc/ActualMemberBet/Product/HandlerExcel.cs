namespace ActualMemberBet.Product;

using AutoMapper;
using Common;
using MediatR;
using OfficeOpenXml;
using Utilities.LinqDynamicSum;

public class HandlerExcel(ActualMemberBetService service, ProductExcelBuilder excelBuilder, IMapper mapper)
    : IRequestHandler<HttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        var productActualMemberBet = await service.Query<ProductActualMemberBet>(criteria);
        var productActualMemberBetExcel = mapper.Map<SummaryEntity<ProductActualMemberBetExcel>>(productActualMemberBet);
        var excelPackage = excelBuilder.Build(productActualMemberBetExcel);
        return excelPackage;
    }
}
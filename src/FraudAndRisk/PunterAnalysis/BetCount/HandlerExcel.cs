namespace PunterAnalysis.BetCount;

using AutoMapper;
using MediatR;
using Model;
using OfficeOpenXml;
using Shared;
using Utilities.LinqDynamicSum;

public class HandlerExcel(
    BetCountService betCountService,
    BetCountExcelSetting excelSettingFactory,
    PunterAnalysisExcelBuilder excelBuilder,
    IMapper mapper)
    : IRequestHandler<HttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        var model = await betCountService.Query(criteria);
        var excelViewModel = mapper.Map<SummaryEntity<BetCountExcelModel>>(model);
        var excelSettingParams = mapper.Map<ExcelSettingParams>(request);
        var excelSetting = excelSettingFactory.CreateSetting(excelSettingParams);
        var excelPackage = excelBuilder.CreateExcelPackage(excelViewModel, excelSetting);
        return excelPackage;
    }
}
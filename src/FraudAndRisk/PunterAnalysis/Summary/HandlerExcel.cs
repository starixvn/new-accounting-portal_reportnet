namespace PunterAnalysis.Summary;

using AutoMapper;
using MediatR;
using Model;
using OfficeOpenXml;
using Shared;
using Utilities.LinqDynamicSum;

public class HandlerExcel(
    SummaryService summaryService,
    SummaryExcelSetting excelSettingFactory,
    PunterAnalysisExcelBuilder excelBuilder,
    IMapper mapper)
    : IRequestHandler<HttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        var model = await summaryService.GetSummaries(criteria);
        var excelViewModel = mapper.Map<SummaryEntity<SummaryExcelModel>>(model);
        var excelSettingParams = mapper.Map<ExcelSettingParams>(request);
        var excelSetting = excelSettingFactory.CreateSetting(excelSettingParams);
        var excelPackage = excelBuilder.CreateExcelPackage(excelViewModel, excelSetting);
        return excelPackage;
    }
}
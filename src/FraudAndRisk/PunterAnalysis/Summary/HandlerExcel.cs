using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.Shared;
using PunterAnalysis.Summary.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Summary
{
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
}
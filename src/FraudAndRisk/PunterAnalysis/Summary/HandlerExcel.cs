using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.Shared;
using PunterAnalysis.Summary.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Summary
{
    public class HandlerExcel : IRequestHandler<HttpParameterExcel, ExcelPackage>
    {
        private readonly SummaryService summaryService;

        private readonly SummaryExcelSetting excelSettingFactory;

        private readonly PunterAnalysisExcelBuilder excelBuilder;

        private readonly IMapper mapper;

        public HandlerExcel(
            SummaryService summaryService,
            SummaryExcelSetting excelSettingFactory,
            PunterAnalysisExcelBuilder excelBuilder,
            IMapper mapper)
        {
            this.summaryService = summaryService;
            this.excelSettingFactory = excelSettingFactory;
            this.excelBuilder = excelBuilder;
            this.mapper = mapper;
        }

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
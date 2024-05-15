using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.BetCount.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.BetCount
{
    public class HandlerExcel : IRequestHandler<HttpParameterExcel, ExcelPackage>
    {
        private readonly BetCountService betCountService;

        private readonly BetCountExcelSetting excelSettingFactory;

        private readonly PunterAnalysisExcelBuilder excelBuilder;

        private readonly IMapper mapper;

        public HandlerExcel(
            BetCountService betCountService,
            BetCountExcelSetting excelSettingFactory, 
            PunterAnalysisExcelBuilder excelBuilder,
            IMapper mapper)
        {
            this.betCountService = betCountService;
            this.excelSettingFactory = excelSettingFactory;
            this.excelBuilder = excelBuilder;
            this.mapper = mapper;
        }

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
}
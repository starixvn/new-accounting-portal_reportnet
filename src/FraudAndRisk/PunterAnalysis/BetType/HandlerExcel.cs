using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.BetType.Model;
using PunterAnalysis.Shared;

namespace PunterAnalysis.BetType
{
    public class HandlerExcel : HandlerExcelBase, IRequestHandler<HttpParameterExcel, ExcelPackage>
    {
        private readonly BetTypeExcelSetting excelSettingFactory;

        private readonly IMapper mapper;

        public HandlerExcel(PunterAnalysisService punterAnalysisService, BetTypeExcelSetting excelSettingFactory, PunterAnalysisExcelBuilder excelBuilder, IMapper mapper)
            : base(punterAnalysisService, excelBuilder, mapper)
        {
            this.mapper = mapper;
            this.excelSettingFactory = excelSettingFactory;
        }

        public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
        {
            var excelSettingParams = mapper.Map<ExcelSettingParams>(request);
            var excelSetting = excelSettingFactory.CreateSetting(excelSettingParams);
            return await Handle<Criteria, BetTypeModel, BetTypeExcelModel>(request, excelSetting);
        }
    }
}
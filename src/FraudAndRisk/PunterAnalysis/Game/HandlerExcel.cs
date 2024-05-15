using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.Game.Model;
using PunterAnalysis.Shared;

namespace PunterAnalysis.Game
{
    public class HandlerExcel : HandlerExcelBase, IRequestHandler<HttpParameterExcel, ExcelPackage>
    {
        private readonly GameExcelSetting excelSettingFactory;

        private readonly IMapper mapper;

        public HandlerExcel(PunterAnalysisService punterAnalysisService, GameExcelSetting excelSettingFactory, PunterAnalysisExcelBuilder excelBuilder, IMapper mapper)
            : base(punterAnalysisService, excelBuilder, mapper)
        {
            this.excelSettingFactory = excelSettingFactory;
            this.mapper = mapper;
        }

        public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
        {
            var excelSettingParams = mapper.Map<ExcelSettingParams>(request);
            var excelSetting = excelSettingFactory.CreateSetting(excelSettingParams);
            return await Handle<Criteria, GameModel, GameExcelModel>(request, excelSetting);
        }
    }
}
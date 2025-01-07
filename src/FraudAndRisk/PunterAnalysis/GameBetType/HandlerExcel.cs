using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.GameBetType.Model;
using PunterAnalysis.Shared;

namespace PunterAnalysis.GameBetType;

public class HandlerExcel(
    PunterAnalysisService punterAnalysisService,
    GameBetTypeExcelSetting excelSettingFactory,
    PunterAnalysisExcelBuilder excelBuilder,
    IMapper mapper)
    : HandlerExcelBase(punterAnalysisService, excelBuilder, mapper),
        IRequestHandler<HttpParameterExcel, ExcelPackage>
{
    private readonly IMapper mapper = mapper;

    public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
    {
        var excelSettingParams = mapper.Map<ExcelSettingParams>(request);
        var excelSetting = excelSettingFactory.CreateSetting(excelSettingParams);
        return await Handle<Criteria, GameBetTypeModel, GameBetTypeExcelModel>(request, excelSetting);
    }
}
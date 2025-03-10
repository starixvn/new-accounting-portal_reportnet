namespace PunterAnalysis.Game;

using AutoMapper;
using MediatR;
using Model;
using OfficeOpenXml;
using Shared;

public class HandlerExcel(
    PunterAnalysisService punterAnalysisService,
    GameExcelSetting excelSettingFactory,
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
        return await Handle<Criteria, GameModel, GameExcelModel>(request, excelSetting);
    }
}
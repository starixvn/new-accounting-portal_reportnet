using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.Shared;
using PunterAnalysis.SportType.Model;

namespace PunterAnalysis.SportType;

public class HandlerExcel(
    PunterAnalysisService punterAnalysisService,
    SportTypeExcelSetting excelSettingFactory,
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
        return await Handle<Criteria, SportTypeModel, SportTypeExcelModel>(request, excelSetting);
    }
}
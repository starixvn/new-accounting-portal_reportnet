using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.League.Model;
using PunterAnalysis.Shared;

namespace PunterAnalysis.League;

public class HandlerExcel(
    LeagueServiceFactory leagueServiceFactory,
    LeagueExcelSetting excelSettingFactory,
    LeagueExcelBuilder excelBuilder,
    IMapper mapper)
    : IRequestHandler<HttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
    {
        var criteria = mapper.Map<Criteria>(request);
        var leagueService = leagueServiceFactory.CreateLeagueService(request.PeriodType);
        var model = await leagueService.Query(criteria);
        var excelSettingParams = mapper.Map<ExcelSettingParams>(request);
        var excelSetting = excelSettingFactory.CreateSetting(excelSettingParams);
        var excelPackage = excelBuilder.CreateExcelPackage(model, excelSetting);
        return excelPackage;
    }
}
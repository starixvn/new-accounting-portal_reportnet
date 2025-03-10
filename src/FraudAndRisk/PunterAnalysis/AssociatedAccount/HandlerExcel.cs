namespace PunterAnalysis.AssociatedAccount;

using AutoMapper;
using MediatR;
using Model;
using OfficeOpenXml;
using Shared;
using Utilities.LinqDynamicSum;

public class HandlerExcel(
    AssociatedFacade associatedFacade,
    AssociatedAccountExcelSetting excelSettingFactory,
    PunterAnalysisExcelBuilder excelBuilder,
    IMapper mapper)
    : IRequestHandler<HttpParameterExcel, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(HttpParameterExcel request, CancellationToken cancellationToken)
    {
        var associatedSummaries = await associatedFacade.Query(request);

        var excelViewModel = mapper.Map<SummaryEntity<AssociatedSummaryExcelModel>>(associatedSummaries);
        var excelSettingParams = mapper.Map<ExcelSettingParams>(request);
        var excelSetting = excelSettingFactory.CreateSetting(excelSettingParams);
        var excelPackage = excelBuilder.CreateExcelPackage(excelViewModel, excelSetting);
        return excelPackage;
    }
}
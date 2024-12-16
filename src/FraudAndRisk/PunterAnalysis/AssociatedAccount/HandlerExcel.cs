using AutoMapper;
using MediatR;
using OfficeOpenXml;
using PunterAnalysis.AssociatedAccount.Model;
using PunterAnalysis.Shared;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.AssociatedAccount;

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
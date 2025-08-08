using AutoMapper;
using OfficeOpenXml;
using TGR.Constants;
using TGR.Criteria.JuiceLeftOver;
using TGR.Model.JuiceLeftOver;
using TGR.ViewModel.JuiceLeftOver;
using Utilities.Configurations;
using Utilities.LinqDynamicSum;

namespace TGR.Services.JuiceLeftOver;

public interface ExcelService
{
    Task<ExcelPackage> ExportExcelBetTypePerformance(IEnumerable<BetTypePerformanceInfo> data,
        GetBetTypePerformanceCriteria criteria);
}

public class ExcelServiceImp(ConfigurationService configurationService, IMapper mapper) : BaseExcelService(configurationService), ExcelService
{
    private const byte startBodyRowIndex = 5;
    public Task<ExcelPackage> ExportExcelBetTypePerformance(IEnumerable<BetTypePerformanceInfo> data, GetBetTypePerformanceCriteria criteria)
    {
        LoadTemplate("BetType", "BetType.xlsx", ExcelConstants.BetTypePerformance);
        BuildTitle(criteria.FromDate, criteria.ToDate, sheet.Cells["A2:Q2"]);
        BuildBodyRows<BetTypePerformanceInfo, BetTypePerformanceInfoExcelItem>(data, startBodyRowIndex, mapper);
        var totalData = data.DynamicSum();
        BuildTotalRow<BetTypePerformanceInfo, BetTypePerformanceInfoExcelItem>(totalData, startBodyRowIndex + data.Count(), mapper);
        ClearTemplate(ExcelConstants.BetTypePerformance);
        return Task.FromResult(ExcelPackage!);
    }
}
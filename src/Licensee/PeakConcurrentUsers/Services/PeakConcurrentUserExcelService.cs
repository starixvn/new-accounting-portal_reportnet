using OfficeOpenXml;
using PeakConcurrentUsers.Criteria;

namespace PeakConcurrentUsers.Services;

public interface PeakConcurrentUserExcelService
{
    Task<ExcelPackage> ExportExcel(PeakConcurrentUserCriteria criteria);
}

public class PeakConcurrentUserExcelServiceImp : PeakConcurrentUserExcelService
{
    public Task<ExcelPackage> ExportExcel(PeakConcurrentUserCriteria criteria)
    {
        throw new NotImplementedException();
    }
}
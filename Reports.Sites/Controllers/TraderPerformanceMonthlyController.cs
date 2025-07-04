using Reports.Sites.Settings;
using TGR.Criteria;
using TGR.Model;
using TGR.Services;

namespace Reports.Sites.Controllers;

using Fanex.Data.Repository;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;

public class TraderPerformanceMonthlyController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.TraderPerformanceMonthly, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> Index()
    {
        return View("~/Views/TraderPerformanceMonthly/Index.cshtml");
    }

    public ActionResult ExportCoverageExcel(int year, string months, string weeks)
    {
        var templatePath = "ExcelTemplate/TraderPerformanceMonthly/MonthlyPerformanceCoverage.xlsx";
        var monthList = months.Split([','], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var weekList = weeks.Split([','], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var criteria = new GetTraderPerformanceMonthlyWeekCriteria
        {
            Year = year
        };
        var dynamicRepository = new DynamicRepository();
        var rawData = dynamicRepository.Fetch<TraderPerformanceWeekRawData>(criteria).ToList();

        using var service = new TraderMonthlyWeekReportService(templatePath, year, monthList, weekList, rawData);
        var memoryStream = service.ExportExcel();
        var filename = $"Saba_Trader_Winloss_Weekly_Report_{year}_(HDPOU).xlsx";
        return File(memoryStream, "application/vnd.ms-excel", filename);
    }
}

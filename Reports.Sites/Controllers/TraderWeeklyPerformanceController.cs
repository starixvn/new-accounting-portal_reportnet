using Reports.Sites.Settings;
using TGR.Services;

namespace Reports.Sites.Controllers;

using Fanex.Data.Repository;
using Fanex.NAP.Client.Attributes;
using Fanex.NAP.Client.Helpers.Business;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.TraderWeeklyPerformance;
using TGR.Model.SoccerPerformanceDetail;

public class TraderWeeklyPerformanceController : Controller
{
    [NapAuth(FunctionKey = FunctionKeys.TraderWeeklyPerformance, Policy = Constant.Policy.Read)]
    public async Task<IActionResult> Index()
    {
        return View("~/Views/TraderWeeklyPerformance/Index.cshtml");
    }

    public ActionResult ExportCoverageExcel(int year, string months, string weeks)
    {
        var templatePath = "ExcelTemplate/TraderWeeklyPerformance/TraderWeeklyPerformanceCoverage.xlsx";
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

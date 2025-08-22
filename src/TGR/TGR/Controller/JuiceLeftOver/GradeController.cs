using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using Microsoft.AspNetCore.Mvc;
using TGR.Criteria.JuiceLeftOver;
using TGR.Services.JuiceLeftOver;

namespace TGR.Controller.JuiceLeftOver;

[Route("api/juice-left-over/grade")]
[ApiController]
public class GradeController(GradeService service, ExcelService excelService) : ControllerBase
{
    [Route("main")]
    public async Task<IActionResult>  GetGradePerformance(GetGradePerformanceCriteria criteria)
    {
        var result = await service.GetGradePerformance(criteria);

        return Ok(result);
    }

    [Route("grade-trader")]
    public async Task<IActionResult>  GetTraderPerformanceOfGrade(GetTraderPerformanceOfGradeCriteria criteria)
    {
        var result = await service.GetGradeTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("grade-league-trader")]
    public async Task<IActionResult>  GetTraderPerformanceOfGradeByLeague(GetTraderPerformanceOfGradeByLeagueCriteria criteria)
    {
        var result = await service.GetGradeLeagueTraderPerformance(criteria);

        return Ok(result);
    }

    [Route("grade-league")]
    public async Task<IActionResult>  GetLeaguePerformanceOfGrade(GetLeaguePerformanceOfGradeCriteria criteria)
    {
        var result = await service.GetGradeLeaguePerformance(criteria);

        return Ok(result);
    }

    [Route("grade-trader-match")]
    public async Task<IActionResult>  GetMatchPerformanceOfGradeTrader(GetMatchPerformanceOfGradeTraderCriteria criteria)
    {
        var result = await service.GetGradeTraderMatchPerformance(criteria);

        return Ok(result);
    }

    [Route("grade-league-trader-match")]
    public async Task<IActionResult>  GetMatchPerformanceOfGradeLeagueTrader(GetMatchPerformanceOfGradeLeagueTraderCriteria criteria)
    {
        var result = await service.GetGradeLeagueTraderMatchPerformance(criteria);

        return Ok(result);
    }

    [Route("grade-bettype")]
    public async Task<IActionResult>  GetBettypeDetailGrade(GetBetTypePerformanceOfGradeLeagueTraderMatchCriteria criteria)
    {
        var result = await service.GetGradeLeagueTraderMatchBettypePerformance(criteria);

        return Ok(result);
    }

    [HttpPost("export-grade")]
    public async Task<IActionResult> ExportGradePerformance([FromForm]GetGradePerformanceCriteria criteria)
    {
        var data = await service.GetGradePerformance(criteria);
        var excelPackage = await excelService.ExportExcelGradePerformance(data, criteria);
        var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
        var fileName = $"Performance_Detail_Grade_(Juice-LeftOver)_{criteria.FromDate:yyyyMMdd}_{criteria.ToDate:yyyyMMdd}";
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
    }
}

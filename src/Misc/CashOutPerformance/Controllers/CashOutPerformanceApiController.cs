using CashOutPerformance.Models;
using CashOutPerformance.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CashOutPerformance.Controllers;

using Utilities.ExcelResult;

[Route("api/cash-out-performance")]
[ApiController]
public class CashOutPerformanceApiController(IMediator mediator) : ControllerBase
{
    [Route("sports")]
    public async Task<IActionResult> Sports()
    {
        var viewModel = await mediator.Send(new SportRequest());
        return Ok(viewModel);
    }

    [Route("licensees")]
    public async Task<IActionResult> Licensees()
    {
        var viewModel = await mediator.Send(new LicenseeRequest());
        return Ok(viewModel);
    }

    [Route("leagues")]
    public async Task<IActionResult> Leagues(LeagueRequest request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("leagueGroups")]
    public async Task<IActionResult> LeagueGroups()
    {
        var viewModel = await mediator.Send(new LeagueGroupRequest());
        return Ok(viewModel);
    }

    [Route("sport-level")]
    [HttpPost]
    public async Task<IActionResult> SportLevel(SportLevelRequest request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("event-status-level")]
    [HttpPost]
    public async Task<IActionResult> EventStatusLevel(TicketRequest<EventStatusLevelModel> request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("bettype-level")]
    [HttpPost]
    public async Task<IActionResult> BetTypeLevel(TicketRequest<BetTypeLevelModel> request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("match-detail-level")]
    [HttpPost]
    public async Task<IActionResult> MatchDetailLevel(TicketRequest<MatchDetailLevelModel> request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("excel-sport")]
    [HttpPost]
    public async Task<IActionResult> TicketsExcelForSport(SportLevelExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("excel-event-status")]
    [HttpPost]
    public async Task<IActionResult> TicketsExcelForEventStatus(TicketExcelRequest<EventStatusLevelModel> request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("excel-bettype")]
    [HttpPost]
    public async Task<IActionResult> TicketsExcelForBetType(TicketExcelRequest<BetTypeLevelModel> request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("excel-match-detail")]
    [HttpPost]
    public async Task<IActionResult> TicketsExcelForMatchDetail(TicketExcelRequest<MatchDetailLevelModel> request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("simple-bettype-level")]
    [HttpPost]
    public async Task<IActionResult> SimpleBetTypeLevel(TicketRequest<SimpleBetTypeLevelModel> request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("drill-down-bettype-level")]
    [HttpPost]
    public async Task<IActionResult> GameTimeLevel(BetTypeDrillDownRequest request)
    {
        var viewModel = await mediator.Send(request);
        return Ok(viewModel);
    }

    [Route("excel-simple-bettype")]
    [HttpPost]
    public async Task<IActionResult> TicketsExcelForSimpleBetType(TicketExcelRequest<SimpleBetTypeLevelModel> request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("excel-drill-down-bettype")]
    [HttpPost]
    public async Task<IActionResult> TicketsExcelForDrillDownBetType(BetTypeDrillDownExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }
}
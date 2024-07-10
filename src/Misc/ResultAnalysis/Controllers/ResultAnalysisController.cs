using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;
using ResultAnalysis.Requests;

namespace ResultAnalysis.Controllers
{
    [Route("api/result-analysis")]
	[ApiController]
	public class ResultAnalysisController: ControllerBase
	{
		private readonly IMediator mediator;

		public ResultAnalysisController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[Route("leagues")]
		public async Task<IActionResult> Leagues(LeagueRequest request)
		{
			var viewModel = await mediator.Send(request);
			return Ok(viewModel);
		}

		[Route("result-analysis")]
		public async Task<IActionResult> ResultAnalysis(ResultAnalysisRequest request)
		{
			var viewModel = await mediator.Send(request);
			return Ok(viewModel);
		}

		[Route("export-excel")]
		[HttpPost]
		public async Task<IActionResult> Excel(ResultAnalysisExcelRequest request)
		{
			var excelPackage = await mediator.Send(request);
			return new EpplusExcelResult(excelPackage);
		}
	}
}

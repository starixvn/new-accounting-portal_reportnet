using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;
using PunterAnalysis.GameBetType.Model;

namespace PunterAnalysis.GameBetType
{
    [Route("api/punter-analysis/game-bet-type")]
    [ApiController]
    public class PunterAnalysisGameBetTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public PunterAnalysisGameBetTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("")]
        public async Task<IActionResult> Index(HttpParameterJson parameter)
        {
            var viewModel = await mediator.Send(parameter);
            return Ok(viewModel);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(HttpParameterExcel parameter)
        {
            var excelPackage = await mediator.Send(parameter);
            return new EpplusExcelResult(excelPackage);
        }
    }
}
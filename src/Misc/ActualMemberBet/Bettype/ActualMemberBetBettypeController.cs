using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;

namespace ActualMemberBet.Bettype
{

    [Route("api/actual-member-bet/bettype")]
    [ApiController]
    public class ActualMemberBetBettypeController(IMediator mediator) : ControllerBase
    {
        [Route("")]
        public async Task<IActionResult> Index(HttpParameter parameter)
        {
            var viewModel = await mediator.Send(parameter);
            return Ok(viewModel);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(HttpParameterExcel httpParameter)
        {
            var excelPackage = await mediator.Send(httpParameter);
            return new EpplusExcelResult(excelPackage);
        }
    }
}

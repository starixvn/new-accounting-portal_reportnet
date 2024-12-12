using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;

namespace ActualMemberBet.Product
{
    [Route("api/actual-member-bet/product")]
    [ApiController]
    public class ActualMemberBetProductController(IMediator mediator) : ControllerBase
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

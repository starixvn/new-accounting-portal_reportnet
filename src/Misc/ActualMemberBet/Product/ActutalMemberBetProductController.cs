using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;

namespace ActualMemberBet.Product
{
    [Route("api/actual-member-bet/product")]
    [ApiController]
    public class ActutalMemberBetProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ActutalMemberBetProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

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

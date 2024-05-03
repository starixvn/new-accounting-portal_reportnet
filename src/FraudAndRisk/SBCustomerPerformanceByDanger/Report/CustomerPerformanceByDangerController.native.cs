using MediatR;
using Microsoft.AspNetCore.Mvc;
using Olympus.WebApi.Epplus;

namespace SBCustomerPerformanceByDanger.Report
{

    [Route("api/sb-customer-performance-by-danger/report")]
    [ApiController]
    public class CustomerPerformanceByDangerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerPerformanceByDangerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("data")]
        [HttpPost]
        public async Task<IActionResult> GetCustomerPerformanceByDanger(CustomerPerformanceByDangerQueryHttpParameter parameter)
        {
            var viewModel = await mediator.Send(parameter);
            return Ok(viewModel);
        }

        [Route("excel")]
        [HttpPost]
        public async Task<IActionResult> LicenseeExcel(CustomerPerformanceByDangerQueryExcelHttpParameter httpParameter)
        {
            var excelPackage = await mediator.Send(httpParameter);
            return new EpplusExcelResult(excelPackage);
        }
    }
}
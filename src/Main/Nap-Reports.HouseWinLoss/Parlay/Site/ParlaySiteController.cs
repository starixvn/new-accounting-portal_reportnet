using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Configuration;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.Parlay.Site
{
    [Route("api/house-win-loss-by-products/parlay/site")]
    [ApiController]
    public class ParlaySiteController : ControllerBase
    {
        private readonly WinLossService service;

        private readonly GridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public ParlaySiteController(WinLossService service, GridOptionsBuilder builder, GridOptionColumnDecoratorBasic decorator, IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, string.Empty);
            gridOptions = decorator.Build(gridOptions, queryModel.SiteType);

            return Ok(gridOptions);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<Criteria>(queryModel);
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, queryModel.Title);
            gridOptions = decorator.Build(gridOptions, queryModel.SiteType);

            return new FanexExcelResult(gridOptions, Constants.SheetName);
        }
    }
}
namespace HouseWinLoss.Parlay.Site
{
    using AutoMapper;
    using Common.ColumnDecorators;
    using Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/parlay/site")]
    [ApiController]
    public class ParlaySiteController(
        WinLossService service,
        GridOptionsBuilder builder,
        GridOptionColumnDecoratorBasic decorator,
        IMapper mapper)
        : ControllerBase
    {
        private readonly GridOptionColumnDecorator decorator = decorator;

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
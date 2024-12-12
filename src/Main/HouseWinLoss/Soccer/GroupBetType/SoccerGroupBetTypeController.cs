namespace HouseWinLoss.Soccer.GroupBetType
{
    using AutoMapper;
    using Common.ColumnDecorators;
    using Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.ExcelResult;

    [Route("api/house-win-loss-by-products/soccer/group-bettype")]
    [ApiController]
    public class SoccerGroupBetTypeController(
        GroupBetTypeWinLossService service,
        GroupBetTypeGridOptionsBuilder builder,
        GridOptionColumnDecoratorFull decorator,
        IMapper mapper)
        : ControllerBase
    {
        private readonly GridOptionColumnDecorator decorator = decorator;

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<GroupBetTypeCriteria>(queryModel);
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, string.Empty);
            const int mainColumnWidth = 210;
            gridOptions = decorator.Build(gridOptions, "Bet Type", mainColumnWidth);

            return Ok(gridOptions);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<GroupBetTypeCriteria>(queryModel);
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, queryModel.Title);
            const int mainColumnWidth = 210;
            gridOptions = decorator.Build(gridOptions, "Bet Type", mainColumnWidth);

            return new FanexExcelResult(gridOptions, Constants.SheetName);
        }
    }
}
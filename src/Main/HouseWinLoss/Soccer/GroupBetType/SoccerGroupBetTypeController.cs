using AutoMapper;
using HouseWinLoss.Common.ColumnDecorators;
using HouseWinLoss.Configuration;
using Microsoft.AspNetCore.Mvc;
using Utilities.ExcelResult;

namespace HouseWinLoss.Soccer.GroupBetType
{
    [Route("api/house-win-loss-by-products/soccer/group-bettype")]
    [ApiController]
    public class SoccerGroupBetTypeController : ControllerBase
    {
        private readonly GroupBetTypeWinLossService service;
        private readonly GroupBetTypeGridOptionsBuilder builder;
        private readonly GridOptionColumnDecorator decorator;
        private readonly IMapper mapper;

        public SoccerGroupBetTypeController(GroupBetTypeWinLossService service, GroupBetTypeGridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator, IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

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
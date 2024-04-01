using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using Nap_Reports.HouseWinLoss.Configuration;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.Soccer.SumBetType
{
    [Route("api/house-win-loss-by-products/soccer/sum-bettype")]
    [ApiController]
    public class SoccerSumBetTypeController : ControllerBase
    {
        private readonly SumBetTypeWinLossService service;

        private readonly SumBetTypeGridOptionsBuilder builder;

        private readonly GridOptionColumnDecorator decorator;

        private readonly IMapper mapper;

        public SoccerSumBetTypeController(SumBetTypeWinLossService service, SumBetTypeGridOptionsBuilder builder, GridOptionColumnDecoratorFull decorator, IMapper mapper)
        {
            this.service = service;
            this.builder = builder;
            this.decorator = decorator;
            this.mapper = mapper;
        }

        [Route("")]
        public async Task<IActionResult> Json(QueryModel queryModel)
        {
            var criteria = mapper.Map<SumBetTypeCriteria>(queryModel);
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, string.Empty);
            const int mainColumnWidth = 210;
            gridOptions = decorator.Build(gridOptions, "Bet Type", mainColumnWidth);

            return Ok(gridOptions);
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var criteria = mapper.Map<SumBetTypeCriteria>(queryModel);
            var winlosses = await service.Query(criteria);
            var gridOptions = builder.Build(winlosses, queryModel.Title);
            const int mainColumnWidth = 210;
            gridOptions = decorator.Build(gridOptions, "Bet Type", mainColumnWidth);

            return new FanexExcelResult(gridOptions, Constants.SheetName);
        }
    }
}
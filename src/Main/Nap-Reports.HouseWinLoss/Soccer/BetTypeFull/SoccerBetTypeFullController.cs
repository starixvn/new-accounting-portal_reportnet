using Fanex.Excel.Models;
using Microsoft.AspNetCore.Mvc;
using Nap_Reports.HouseWinLoss.Configuration;
using Nap_Reports.Utilities.ExcelResult;

namespace Nap_Reports.HouseWinLoss.Soccer.BetTypeFull
{
    [Route("api/house-win-loss-by-products/soccer/bettype-full")]
    public class SoccerBetTypeFullController : ControllerBase
    {
        private readonly AbstractFactory factory;

        public SoccerBetTypeFullController(AbstractFactory factory)
        {
            this.factory = factory;
        }

        [Route("excel")]
        public async Task<IActionResult> Excel(ExcelQueryModel queryModel)
        {
            var isMajor = queryModel.IsMajor == Constants.IsMajor;
            var betTypeFullGridOptionFactory = factory.CreateGridOptionFactory(isMajor, 2);
            var betTypeFullGridOptions = await betTypeFullGridOptionFactory.CreateGridOption(queryModel, queryModel.BetTypeFullTitle, 2);

            var betTypeLiveGridOptionFactory = factory.CreateGridOptionFactory(isMajor, 1);
            var betTypeLiveGridOptions = await betTypeLiveGridOptionFactory.CreateGridOption(queryModel, queryModel.BetTypeLiveTitle, 1);

            var betTypeNonLiveGridOptionFactory = factory.CreateGridOptionFactory(isMajor, 0);
            var betTypeNonLiveGridOptions = await betTypeNonLiveGridOptionFactory.CreateGridOption(queryModel, queryModel.BetTypeNonLiveTitle, 0);

            return new FanexExcelResult(
                new List<GridOptions> { betTypeFullGridOptions, betTypeLiveGridOptions, betTypeNonLiveGridOptions },
                Constants.SheetName);
        }
    }
}
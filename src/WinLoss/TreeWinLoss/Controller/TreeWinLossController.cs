using Microsoft.AspNetCore.Mvc;
using Nap_Reports.Utilities.DateTimes;
using TreeWinLoss.Criteria;
using TreeWinLoss.Model;
using TreeWinLoss.Services;

namespace TreeWinLoss.Controller
{
    [Route("api/tree-win-loss")]

    [ApiController]
    public class TreeWinLossController(TreeWinLossService service, TreeWinLossExcelService excelService) : ControllerBase
    {
        [Route("product")]
        public async Task<IActionResult> GetProduct()
        {
            var result = await service.GetProduct();
            return Ok(result);
        }

        [Route("sport")]
        public async Task<IActionResult> GetSport()
        {
            var result = await service.GetSport();
            return Ok(result);
        }

        [Route("main")]
        public async Task<IActionResult> GetTreeWinLossByProduct(TreeWinLossByProductCriteria criteria)
        {
            var result = await service.GetTreeWinLossByProduct(criteria);
            return Ok(result);
        }

        [Route("win-loss-by-level")]
        public async Task<IActionResult> GetTreeWinLossByLevel(TreeWinLossByLevelCriteria criteria)
        {
            var result = await service.GetTreeWinLossByLevel(criteria);
            return Ok(result);
        }

        [Route("virtual-rate")]
        public async Task<IActionResult> GetTreeWinLossVirtualRate(VirtualRateRequestModel model)
        {
            var result = await service.GetVirtualRate(model.CustId);
            return Ok(result);
        }

        [Route("excel-winloss-by-product")]
        public async Task<IActionResult> ExportTreeWinLossByProduct(TreeWinLossByProductCriteria criteria)
        {
            var excelPackage = await excelService.ExportExcelTreeWinLossByProduct(criteria);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileName =
                $"Tree_Win_Loss_Report_From_{criteria.FromDate.ToShortFormatedDateWithSlash().Replace('/', '_')}_To_{criteria.ToDate.ToShortFormatedDateWithSlash().Replace('/', '_')}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [Route("excel-winloss-by-level")]
        public async Task<IActionResult> ExportTreeWinLossByProduct(TreeWinLossByLevelCriteria criteria)
        {
            var excelPackage = await excelService.ExportExcelTreeWinLossByLevel(criteria);
            var stream = new MemoryStream(await excelPackage.GetAsByteArrayAsync());
            var fileName =
                $"Tree_Win_Loss_Report_From_{criteria.FromDate.ToShortFormatedDateWithSlash().Replace('/', '_')}_To_{criteria.ToDate.ToShortFormatedDateWithSlash().Replace('/', '_')}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}

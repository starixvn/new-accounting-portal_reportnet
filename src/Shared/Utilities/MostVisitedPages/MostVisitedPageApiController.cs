using Microsoft.AspNetCore.Mvc;

namespace Utilities.MostVisitedPages
{
    [Route("api/most-visited-page")]
    [ApiController]
    public class MostVisitedPageApiController(MostVisitedPagesService service) : ControllerBase
    {
        [Route("main")]
        public async Task<IActionResult> GetMostvisitedPages()
        {
            var result = await service.GetMostVisitedPages();

            return Ok(result);
        }

        [Route("update-count")]
        public IActionResult UpdateMostVisitedPageCount(MostVisitedPageModel mostVisitedPage)
        {
            service.UpdateCount(mostVisitedPage);
            return Ok();
        }

        [Route("update-position")]
        public async Task<IActionResult> UpdateMostVisitedPagePosition(UpdateMostVisitedPagePositionCriteria mostVisitedPage)
        {
            var result = await service.UpdatePosition(mostVisitedPage);
            return Ok(result);
        }

        [Route("delete")]
        public async Task<IActionResult> DeleteMostVisitedPage(DeleteMostVisitedPagesCriteria mostVisitedPage)
        {
            var result = await service.Delete(mostVisitedPage);

            var mostVisitedPages = await service.GetMostVisitedPages();

            return Ok(mostVisitedPages);
        }
    }
}

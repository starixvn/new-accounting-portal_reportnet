using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SaveBetReport.Criteria;
using SaveBetReport.Models;
using SaveBetReport.Services;

namespace SaveBetReport.Controller;

[Route("api/save-bet")]
[ApiController]
public class SaveBetReportApiController(SaveBetReportService service, IConfiguration configuration) : ControllerBase
{
    private const string DefaultFolder = "{folder}";

    [Route("leagues-by-sport")]
    [HttpPost]
    public async Task<IActionResult> GetLeaguesBySport(GetLeaguesCriteria criteria)
    {
        var result = await service.GetLeagues(criteria);
        return Ok(result);
    }

    [Route("sport-by-date")]
    [HttpPost]
    public async Task<IActionResult> GetSportsByDate(GetSportsCriteria criteria)
    {
        var result = await service.GetSports(criteria);
        return Ok(result);
    }

    [Route("reports")]
    [HttpPost]
    public async Task<IActionResult> GetSaveBetReport(GetMatchesCriteria criteria)
    {
        var result = await service.GetMatches(criteria);
        return Ok(result);
    }

    [Route("download")]
    [HttpPost]
    public IActionResult Download(DownloadFileModel model)
    {
        var folderList = configuration.GetSection("SaveBetDockerFolders").Value!.Split(",");
        foreach (var folder in folderList)
        {
            var path = configuration.GetSection("SaveBetFileServerDirectory").Value!.Replace(DefaultFolder, folder);
            var filePath = Path.Combine(path, model.SubFolder, model.FileName);
            if (!System.IO.File.Exists(filePath)) continue;
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentResult = File(fileBytes, model.ContentType, model.FileName);
            return Ok(new ApiResult { Success = true, Data = contentResult });

        }
        return Ok(new ApiResult { Success = false });
    }
}
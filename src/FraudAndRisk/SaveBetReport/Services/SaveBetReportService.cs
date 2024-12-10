using Fanex.Data.Repository;
using Fanex.RemotingRef;
using SaveBetReport.Criteria;
using SaveBetReport.Models;
using Utilities.DateTimes;

namespace SaveBetReport.Services;

public interface SaveBetReportService
{
    Task<IEnumerable<League>> GetLeagues(GetLeaguesCriteria criteria);

    Task<IEnumerable<Sport>> GetSports(GetSportsCriteria criteria);

    Task<IEnumerable<Match>> GetMatches(GetMatchesCriteria criteria);
}

public class SaveBetReportServiceImp(IDynamicRepository repository) : SaveBetReportService
{
    public Task<IEnumerable<League>> GetLeagues(GetLeaguesCriteria criteria)
    {
        return repository.FetchAsync<League>(criteria);
    }

    public Task<IEnumerable<Sport>> GetSports(GetSportsCriteria criteria)
    {
        return repository.FetchAsync<Sport>(criteria);
    }

    public async Task<IEnumerable<Match>> GetMatches(GetMatchesCriteria criteria)
    {
        var result = (await repository.FetchAsync<Match>(criteria)).ToList();
        result.ForEach(item =>
        {
            item.MatchCode = new RemotingRefService().GetBetRadarMatchCodeFormat(item.MatchCode, "en");
            item.EventDateString = item.EventDate.ToShortFormatedDateWithSlash().Replace("/", "_");
        });
        return result;
    }
}
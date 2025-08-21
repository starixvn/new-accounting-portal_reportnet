namespace Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;

using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using TGR.Criteria.JuiceLeftOver;
using TGR.Model.JuiceLeftOver;
using Utilities.RemotingRef;

public interface TeamService
{
    Task<IEnumerable<TeamInfo>> GetTeams();

    Task<IEnumerable<TeamPerformanceInfo>> GetTeamPerformance(GetTeamPerformanceCriteria criteria);

    Task<IEnumerable<TeamLeaguePerformanceInfo>> GetTeamLeaguePerformance(ICriteria criteria);

    Task<IEnumerable<TeamLeagueTraderPerformanceInfo>> GetTeamLeagueTraderPerformance(GetTraderPerformanceOfLeagueTeam criteria);

    Task<IEnumerable<TeamLeagueTraderMatchPerformanceInfo>> GetTeamLeagueTraderMatchPerformance(ICriteria criteria);

    Task<IEnumerable<TeamLeagueTraderMatchBettypePerformanceInfo>> GetTeamLeagueTraderMatchBettypePerformance(
        GetBettypeDetailTeamCriteria criteria);

    Task<IEnumerable<TeamTraderMatchBettypePerformanceInfo>> GetTeamTraderMatchBettypePerformance(ICriteria criteria);

    Task<IEnumerable<TeamTraderPerformanceInfo>> GetTeamTraderPerformance(GetTraderPerformanceOfTeamCriteria criteria);

    Task<Dictionary<string, IEnumerable<TeamTraderMatchPerformanceInfo>>> GetTeamTraderMatchPerformance(ICriteria criteria);
}

public class TeamServiceImp(IDynamicRepository repository, RemotingRefService remotingRefService) : TeamService
{
    public async Task<IEnumerable<TeamInfo>> GetTeams()
    {
        var result = await repository.FetchAsync<TeamInfo>(new GetTeamsCriteria());

        result ??= new List<TeamInfo>();

        return result;
    }

    public async Task<IEnumerable<TeamPerformanceInfo>> GetTeamPerformance(GetTeamPerformanceCriteria criteria)
    {
        var result = await repository.FetchAsync<TeamPerformanceInfo>(criteria);

        result ??= new List<TeamPerformanceInfo>();

        if (!criteria.IsViewScore)
        {
            result = result.Select(item =>
            {
                item.Score = null;
                return item;
            });
        }

        return result;
    }

    public async Task<IEnumerable<TeamLeaguePerformanceInfo>> GetTeamLeaguePerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<TeamLeaguePerformanceInfo>(criteria);

        result ??= new List<TeamLeaguePerformanceInfo>();

        return result;
    }

    public async Task<IEnumerable<TeamLeagueTraderPerformanceInfo>> GetTeamLeagueTraderPerformance(GetTraderPerformanceOfLeagueTeam criteria)
    {
        var result = await repository.FetchAsync<TeamLeagueTraderPerformanceInfo>(criteria);

        result ??= new List<TeamLeagueTraderPerformanceInfo>();

        if (!criteria.IsViewScore)
        {
            result = result.Select(item =>
            {
                item.Score = null;
                return item;
            });
        }

        return result;
    }

    public async Task<IEnumerable<TeamLeagueTraderMatchPerformanceInfo>> GetTeamLeagueTraderMatchPerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<TeamLeagueTraderMatchPerformanceInfo>(criteria);

        result ??= new List<TeamLeagueTraderMatchPerformanceInfo>();

        result = result.Select(item =>
        {
            var homeTeam = remotingRefService.GetTeamName(item.HomeId);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId);
            item.MatchName = $"{homeTeam} -vs- {awayTeam}";

            return item;
        });

        return result;
    }

    public async Task<IEnumerable<TeamLeagueTraderMatchBettypePerformanceInfo>> GetTeamLeagueTraderMatchBettypePerformance(
        GetBettypeDetailTeamCriteria criteria)
    {
        var result = await repository.FetchAsync<TeamLeagueTraderMatchBettypePerformanceInfo>(criteria);

        result ??= new List<TeamLeagueTraderMatchBettypePerformanceInfo>();

        if (!criteria.IsViewScore)
        {
            result = result.Select(item =>
            {
                item.Score = null;
                return item;
            });
        }

        return result;
    }

    public async Task<IEnumerable<TeamTraderMatchBettypePerformanceInfo>> GetTeamTraderMatchBettypePerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<TeamTraderMatchBettypePerformanceInfo>(criteria);

        result ??= new List<TeamTraderMatchBettypePerformanceInfo>();

        return result;
    }

    public async Task<IEnumerable<TeamTraderPerformanceInfo>> GetTeamTraderPerformance(GetTraderPerformanceOfTeamCriteria criteria)
    {
        var result = await repository.FetchAsync<TeamTraderPerformanceInfo>(criteria);

        result ??= new List<TeamTraderPerformanceInfo>();

        if (!criteria.IsViewScore)
        {
            result = result.Select(item =>
            {
                item.Score = null;
                return item;
            });
        }

        return result;
    }

    public async Task<Dictionary<string, IEnumerable<TeamTraderMatchPerformanceInfo>>> GetTeamTraderMatchPerformance(ICriteria criteria)
    {
        var data = await repository.FetchAsync<TeamTraderMatchPerformanceInfo>(criteria);
        
        var result = new Dictionary<string, IEnumerable<TeamTraderMatchPerformanceInfo>>();

        data ??= new List<TeamTraderMatchPerformanceInfo>();

        data = data.Select(item =>
        {
            var homeTeam = remotingRefService.GetTeamName(item.HomeId);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId);
            item.MatchName = $"{homeTeam} -vs- {awayTeam}";

            return item;
        });

        if (data.Any())
        {
            var groups = data.GroupBy(t => t.LeagueId);
            
            foreach (var group in groups)
            {
                var leagueName = remotingRefService.GetLeagueName(group.Key);

                if (string.IsNullOrEmpty(leagueName))
                {
                    leagueName = group.Key.ToString();
                }

                result.Add(leagueName, group.Select(t => t));
            }
        }

        return result;
    }
}
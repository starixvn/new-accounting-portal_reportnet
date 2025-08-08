namespace Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;

using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using TGR.Criteria.JuiceLeftOver;
using TGR.Model.JuiceLeftOver;
using Utilities.RemotingRef;

public interface LeagueService
{
    Task<IEnumerable<LeaguePerformanceInfo>> GetLeaguePerformance(ICriteria criteria);

    Task<IEnumerable<LeagueTraderPerformanceInfo>> GetTraderPerformanceByLeague(GetTraderPerformanceOfLeagueCriteria criteria);

    Task<IEnumerable<LeagueTraderMatchPerformanceInfo>> GetMatchPerformanceOfLeagueAndTrader(ICriteria criteria);

    Task<IEnumerable<LeagueTraderMatchBettypePerformanceInfo>> GetBettypePerformanceOfLeagueTraderAndMatch(
        GetBettypeDetailLeagueCriteria criteria);

    Task<IEnumerable<LeagueMatchPerformanceInfo>> GetMatchPerformanceOfLeague(ICriteria criteria);

    Task<IEnumerable<LeagueMatchBettypePerformanceInfo>> GetBettypePerformanceOfLeagueAndMatch(
        GetBetTypePerformanceOfLeagueMatchCriteria criteria);
}

public class LeagueServiceImp(IDynamicRepository repository, RemotingRefService remotingRefService) : LeagueService
{
    public async Task<IEnumerable<LeaguePerformanceInfo>> GetLeaguePerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<LeaguePerformanceInfo>(criteria);

        result ??= new List<LeaguePerformanceInfo>();

        return result;
    }

    public async Task<IEnumerable<LeagueTraderPerformanceInfo>> GetTraderPerformanceByLeague(GetTraderPerformanceOfLeagueCriteria criteria)
    {
        var result = await repository.FetchAsync<LeagueTraderPerformanceInfo>(criteria);

        result ??= new List<LeagueTraderPerformanceInfo>();

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

    public async Task<IEnumerable<LeagueTraderMatchPerformanceInfo>> GetMatchPerformanceOfLeagueAndTrader(ICriteria criteria)
    {
        var result = await repository.FetchAsync<LeagueTraderMatchPerformanceInfo>(criteria);

        result ??= new List<LeagueTraderMatchPerformanceInfo>();

        result = result.Select(item =>
        {
            var homeTeam = remotingRefService.GetTeamName(item.HomeId);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId);
            item.MatchName = $"{homeTeam} vs {awayTeam}";

            return item;
        });

        return result;
    }

    public async Task<IEnumerable<LeagueTraderMatchBettypePerformanceInfo>> GetBettypePerformanceOfLeagueTraderAndMatch(
        GetBettypeDetailLeagueCriteria criteria)
    {
        var result = await repository.FetchAsync<LeagueTraderMatchBettypePerformanceInfo>(criteria);

        result ??= new List<LeagueTraderMatchBettypePerformanceInfo>();

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

    public async Task<IEnumerable<LeagueMatchPerformanceInfo>> GetMatchPerformanceOfLeague(ICriteria criteria)
    {
        var result = await repository.FetchAsync<LeagueMatchPerformanceInfo>(criteria);

        result ??= new List<LeagueMatchPerformanceInfo>();

        result = result.Select(item =>
        {
            var homeTeam = remotingRefService.GetTeamName(item.HomeId);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId);
            item.MatchName = $"{homeTeam} vs {awayTeam}";

            return item;
        });

        return result;
    }

    public async Task<IEnumerable<LeagueMatchBettypePerformanceInfo>> GetBettypePerformanceOfLeagueAndMatch(
        GetBetTypePerformanceOfLeagueMatchCriteria criteria)
    {
        var result = await repository.FetchAsync<LeagueMatchBettypePerformanceInfo>(criteria);

        result ??= new List<LeagueMatchBettypePerformanceInfo>();

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
}
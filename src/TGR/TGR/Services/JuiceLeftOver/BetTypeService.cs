namespace Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;

using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using TGR.Criteria.JuiceLeftOver;
using TGR.Model.JuiceLeftOver;
using Utilities.RemotingRef;

public interface BetTypeService
{
    Task<IEnumerable<BetTypePerformanceInfo>> GetBetTypePerformance(ICriteria criteria);

    Task<IEnumerable<BetTypeLeaguePerformanceInfo>> GetBetTypeLeaguePerformance(ICriteria criteria);

    Task<IEnumerable<BetTypeLeagueTraderPerformanceInfo>> GetBetTypeLeagueTraderPerformance(
        GetTraderPerformanceOfBetTypeLeagueCriteria criteria);

    Task<IEnumerable<BetTypeLeagueTraderMatchPerformanceInfo>> GetBetTypeLeagueTraderMatchPerformance(
        GetMatchPerformanceOfBetTypeLeagueTraderCriteria criteria);

    Task<IEnumerable<BetTypeTraderPerformanceInfo>> GetBetTypeTraderPerformance(GetTraderPerformanceOfBetTypeCriteria criteria);

    Task<Dictionary<string, IEnumerable<BetTypeTraderMatchPerformanceInfo>>> GetBetTypeTraderMatchPerformance(
        GetMatchPerformanceOfBetTypeTraderCriteria criteria);
}

public class BetTypeServiceImp(IDynamicRepository repository, RemotingRefService remotingRefService) : BetTypeService
{
    public async Task<IEnumerable<BetTypePerformanceInfo>> GetBetTypePerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<BetTypePerformanceInfo>(criteria);

        result ??= new List<BetTypePerformanceInfo>();

        return result;
    }

    public async Task<IEnumerable<BetTypeLeaguePerformanceInfo>> GetBetTypeLeaguePerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<BetTypeLeaguePerformanceInfo>(criteria);

        result ??= new List<BetTypeLeaguePerformanceInfo>();

        return result;
    }

    public async Task<IEnumerable<BetTypeLeagueTraderPerformanceInfo>> GetBetTypeLeagueTraderPerformance(
        GetTraderPerformanceOfBetTypeLeagueCriteria criteria)
    {
        var result = await repository.FetchAsync<BetTypeLeagueTraderPerformanceInfo>(criteria);

        result ??= new List<BetTypeLeagueTraderPerformanceInfo>();

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

    public async Task<IEnumerable<BetTypeLeagueTraderMatchPerformanceInfo>> GetBetTypeLeagueTraderMatchPerformance(
        GetMatchPerformanceOfBetTypeLeagueTraderCriteria criteria)
    {
        var result = await repository.FetchAsync<BetTypeLeagueTraderMatchPerformanceInfo>(criteria);

        result ??= new List<BetTypeLeagueTraderMatchPerformanceInfo>();

        if (!criteria.IsViewScore)
        {
            result = result.Select(item =>
            {
                item.Score = null;
                return item;
            });
        }

        result = result.Select(item =>
        {
            var homeTeam = remotingRefService.GetTeamName(item.HomeId);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId);
            item.MatchName = $"{homeTeam} -vs- {awayTeam}";
            
            return item;
        });

        return result;
    }

    public async Task<IEnumerable<BetTypeTraderPerformanceInfo>> GetBetTypeTraderPerformance(GetTraderPerformanceOfBetTypeCriteria criteria)
    {
        var result = await repository.FetchAsync<BetTypeTraderPerformanceInfo>(criteria);

        result ??= new List<BetTypeTraderPerformanceInfo>();

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

    public async Task<Dictionary<string, IEnumerable<BetTypeTraderMatchPerformanceInfo>>> GetBetTypeTraderMatchPerformance(
        GetMatchPerformanceOfBetTypeTraderCriteria criteria)
    {
        var data = await repository.FetchAsync<BetTypeTraderMatchPerformanceInfo>(criteria);
        var result = new Dictionary<string, IEnumerable<BetTypeTraderMatchPerformanceInfo>>();

        data ??= new List<BetTypeTraderMatchPerformanceInfo>();

        if (!criteria.IsViewScore)
        {
            data = data.Select(item =>
            {
                item.Score = null;
                return item;
            });
        }

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
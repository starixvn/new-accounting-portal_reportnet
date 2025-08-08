namespace Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;

using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using TGR.Criteria.JuiceLeftOver;
using TGR.Model.JuiceLeftOver;
using Utilities.RemotingRef;

public interface GradeService
{
    Task<IEnumerable<GradePerformanceInfo>> GetGradePerformance(GetGradePerformanceCriteria criteria);

    Task<IEnumerable<GradeLeaguePerformanceInfo>> GetGradeLeaguePerformance(ICriteria criteria);

    Task<IEnumerable<GradeLeagueTraderPerformanceInfo>> GetGradeLeagueTraderPerformance(
        GetTraderPerformanceOfGradeByLeagueCriteria criteria);

    Task<IEnumerable<GradeLeagueTraderMatchPerformanceInfo>> GetGradeLeagueTraderMatchPerformance(ICriteria criteria);

    Task<IEnumerable<GradeLeagueTraderMatchBettypePerformanceInfo>> GetGradeLeagueTraderMatchBettypePerformance(
        GetBetTypePerformanceOfGradeLeagueTraderMatchCriteria criteria);

    Task<IEnumerable<GradeTraderMatchBettypePerformanceInfo>> GetGradeTraderMatchBettypePerformance(ICriteria criteria);

    Task<IEnumerable<GradeTraderPerformanceInfo>> GetGradeTraderPerformance(GetTraderPerformanceOfGradeCriteria criteria);

    Task<Dictionary<string, IEnumerable<GradeTraderMatchPerformanceInfo>>> GetGradeTraderMatchPerformance(ICriteria criteria);
}

public class GradeServiceImp(IDynamicRepository repository, RemotingRefService remotingRefService) : GradeService
{
    public async Task<IEnumerable<GradePerformanceInfo>> GetGradePerformance(GetGradePerformanceCriteria criteria)
    {
        var result =  await repository.FetchAsync<GradePerformanceInfo>(criteria);

        result ??= new List<GradePerformanceInfo>();

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

    public async Task<IEnumerable<GradeLeaguePerformanceInfo>> GetGradeLeaguePerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<GradeLeaguePerformanceInfo>(criteria);

        result  ??= new List<GradeLeaguePerformanceInfo>();

        return result;
    }

    public async Task<IEnumerable<GradeLeagueTraderPerformanceInfo>> GetGradeLeagueTraderPerformance(
        GetTraderPerformanceOfGradeByLeagueCriteria criteria)
    {
        var result = await repository.FetchAsync<GradeLeagueTraderPerformanceInfo>(criteria);

        result ??= new List<GradeLeagueTraderPerformanceInfo>();

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

    public async Task<IEnumerable<GradeLeagueTraderMatchPerformanceInfo>> GetGradeLeagueTraderMatchPerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<GradeLeagueTraderMatchPerformanceInfo>(criteria);

        result ??= new List<GradeLeagueTraderMatchPerformanceInfo>();

        result = result.Select(item =>
        {
            var homeTeam = remotingRefService.GetTeamName(item.HomeId);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId);
            item.MatchName = $"{homeTeam} vs {awayTeam}";

            return item;
        });

        return result;
    }

    public async  Task<IEnumerable<GradeLeagueTraderMatchBettypePerformanceInfo>> GetGradeLeagueTraderMatchBettypePerformance(
        GetBetTypePerformanceOfGradeLeagueTraderMatchCriteria criteria)
    {
        var result = await repository.FetchAsync<GradeLeagueTraderMatchBettypePerformanceInfo>(criteria);

        result ??= new List<GradeLeagueTraderMatchBettypePerformanceInfo>();

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

    public async Task<IEnumerable<GradeTraderMatchBettypePerformanceInfo>> GetGradeTraderMatchBettypePerformance(ICriteria criteria)
    {
        var result = await repository.FetchAsync<GradeTraderMatchBettypePerformanceInfo>(criteria);

        result ??= new List<GradeTraderMatchBettypePerformanceInfo>();

        return result;
    }

    public async Task<IEnumerable<GradeTraderPerformanceInfo>> GetGradeTraderPerformance(GetTraderPerformanceOfGradeCriteria criteria)
    {
        var result = await repository.FetchAsync<GradeTraderPerformanceInfo>(criteria);

        result ??= new List<GradeTraderPerformanceInfo>();

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

    public async Task<Dictionary<string, IEnumerable<GradeTraderMatchPerformanceInfo>>> GetGradeTraderMatchPerformance(ICriteria criteria)
    {
        var data =await repository.FetchAsync<GradeTraderMatchPerformanceInfo>(criteria);
        var result = new Dictionary<string, IEnumerable<GradeTraderMatchPerformanceInfo>>();

        data ??= new List<GradeTraderMatchPerformanceInfo>();

        data = data.Select(item =>
        {
            var homeTeam = remotingRefService.GetTeamName(item.HomeId);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId);
            item.MatchName = $"{homeTeam} vs {awayTeam}";

            return item;
        });

        if (data.Count() > 0)
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
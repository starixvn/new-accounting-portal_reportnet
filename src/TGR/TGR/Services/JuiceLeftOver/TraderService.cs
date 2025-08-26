namespace Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;

using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using TGR.Criteria.JuiceLeftOver;
using TGR.Model.JuiceLeftOver;
using Utilities.RemotingRef;

public interface TraderService
{
    Task<IEnumerable<TraderPerformanceInfo>> GetTraderPerformance(GetTraderPerformanceCriteria criteria);

    Task<IEnumerable<MatchPerformanceInfo>> GetTraderMatchPerformance(GetTraderMatchPerformanceCriteria criteria);

    Task<IEnumerable<TraderMatchBetTypePerformanceInfo>> GetTraderMatchBettypePerformance(GetTraderMatchBettypePerformanceCriteria criteria);
}

public class TraderServiceImp(IDynamicRepository repository, RemotingRefService remotingRefService) : TraderService
{
    public async Task<IEnumerable<TraderPerformanceInfo>> GetTraderPerformance(GetTraderPerformanceCriteria criteria)
    {
        var result = await repository.FetchAsync<TraderPerformanceInfo>(criteria);

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

    public async  Task<IEnumerable<MatchPerformanceInfo>> GetTraderMatchPerformance(GetTraderMatchPerformanceCriteria criteria)
    {
        var result = await repository.FetchAsync<MatchPerformanceInfo>(criteria);

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
            var homeTeam = remotingRefService.GetTeamName(item.HomeId.Value);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId.Value);
            item.MatchName = $"{homeTeam} -vs- {awayTeam}";
            item.LeagueName = remotingRefService.GetLeagueName(item.LeagueId.Value);

            return item;
        });

        return result;
    }

    public async Task<IEnumerable<TraderMatchBetTypePerformanceInfo>> GetTraderMatchBettypePerformance(GetTraderMatchBettypePerformanceCriteria criteria)
    {
        var result = await repository.FetchAsync< TraderMatchBetTypePerformanceInfo >(criteria);

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
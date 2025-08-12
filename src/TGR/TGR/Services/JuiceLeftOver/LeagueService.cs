namespace Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;

using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    Task<string> GetCsvDataForMatchPerformance(ICriteria criteria);
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

    public async Task<string> GetCsvDataForMatchPerformance(ICriteria criteria)
    {
        var match = await repository.FetchAsync<LeagueMatchPerformanceInfo>(criteria);

        match ??= new List<LeagueMatchPerformanceInfo>();

        var content = new StringBuilder();
        const string NA = "N/A";
        const string formatNumber = "0.##";

        decimal totalTO = 0, totalWL = 0, totalJuiceWL = 0, totalJuiceTO = 0, totalLeftoverWL = 0, totalLeftoverTO = 0;
        int totalMatch = 0, totalLeague = 0, totalBetcount = 0;

        totalMatch = match.Select(m => m.MatchId).Distinct().Count();
        totalLeague = match.Select(m => m.LeagueId).Distinct().Count();

        content.AppendLine("Event Date,League,Match,Juice_Turnover(MY),Juice_WinLoss,Juice_Margin,Juice_MarginTotal,LO_Turnover(MY),LO_WinLoss,LO_Margin,LO_MarginTotal,Total_TurnOver(MY),Total_WinLoss,Total_Margin,Bet_Count");

        foreach (var item in match)
        {
            var juiceMargin = item.JuiceMargin.HasValue ? item.JuiceMargin.Value.ToString(formatNumber) : NA;
            var juiceMarginTotal = item.JuiceMarginTotal.HasValue ? item.JuiceMarginTotal.Value.ToString(formatNumber) : NA;
            var leftoverMargin = item.LeftoverMargin.HasValue ? item.LeftoverMargin.Value.ToString(formatNumber) : NA;
            var leftoverMarginTotal = item.LeftoverMarginTotal.HasValue ? item.LeftoverMarginTotal.Value.ToString(formatNumber) : NA;
            var margin = item.Margin.HasValue ? item.Margin.Value.ToString(formatNumber) : NA;

            content.AppendLine($"{item.EventDateString}," +
                $"{item.LeagueName}," +
                $"{item.MatchName}," +
                $"{item.TurnoverJuice.ToString(formatNumber)}," +
                $"{item.Juice.ToString(formatNumber)}," +
                $"{juiceMargin}," +
                $"{juiceMarginTotal}," +
                $"{item.TurnoverLeftover.ToString(formatNumber)}," +
                $"{item.WinLossLeftover.ToString(formatNumber)}," +
                $"{leftoverMargin}," +
                $"{leftoverMarginTotal}," +
                $"{item.Turnover.ToString(formatNumber)}," +
                $"{item.WinLoss.ToString(formatNumber)}," +
                $"{margin}," +
                $"{item.BetCount}");

            totalJuiceTO += item.TurnoverJuice;
            totalJuiceWL += item.Juice;
            totalLeftoverTO += item.TurnoverLeftover;
            totalLeftoverWL += item.WinLossLeftover;
            totalTO += item.Turnover;
            totalWL += item.WinLoss;
            totalBetcount += item.BetCount ?? 0;
        }

        var totalJuiceMargin = totalJuiceTO > 0 ? (totalJuiceWL / totalJuiceTO * 100).ToString(formatNumber) : NA;
        var totalJuiceMarginTotal = totalTO > 0 ? (totalJuiceWL / totalTO * 100).ToString(formatNumber) : NA;
        var totalLeftoverMargin = totalLeftoverTO > 0 ? (totalLeftoverWL / totalLeftoverTO * 100).ToString(formatNumber) : NA;
        var totalLeftoverMarginTotal = totalTO > 0 ? (totalLeftoverWL / totalTO * 100).ToString(formatNumber) : NA;
        var totalMargin = totalTO > 0 ? (totalWL / totalTO * 100).ToString(formatNumber) : NA;

        content.AppendLine("Total," +
            $"{totalLeague}," +
            $"{totalMatch}," +
            $"{totalJuiceTO.ToString(formatNumber)}," +
            $"{totalJuiceWL.ToString(formatNumber)}," +
            $"{totalJuiceMargin}," +
            $"{totalJuiceMarginTotal}," +
            $"{totalLeftoverTO.ToString(formatNumber)}," +
            $"{totalLeftoverWL.ToString(formatNumber)}," +
            $"{totalLeftoverMargin}," +
            $"{totalLeftoverMarginTotal}," +
            $"{totalTO.ToString(formatNumber)}," +
            $"{totalWL.ToString(formatNumber)}," +
            $"{totalMargin}," +
            $"{totalBetcount.ToString(formatNumber)}");

        return content.ToString();
    }
}
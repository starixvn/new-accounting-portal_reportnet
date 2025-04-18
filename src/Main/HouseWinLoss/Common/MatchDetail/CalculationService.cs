namespace HouseWinLoss.Common.MatchDetail;

using AutoMapper;
using Utilities.LinqDynamicSum;
using Utilities.RemotingRef;

public interface CalculationService
{
    IEnumerable<LeagueWinLoss> CalculateLeagues(IEnumerable<RawWinLoss> rawWinLoss);

    WinLoss CalculateSumWinLoss(IEnumerable<RawWinLoss> rawWinLosses);

    IEnumerable<WinLoss> CalculateRM(IEnumerable<RawWinLoss> rawWinLosses);
}

public class CalculationServiceImp(RemotingRefService refService, IMapper mapper) : CalculationService
{
    public WinLoss FromRawWinLoss(RawWinLoss rawWinLoss)
    {
        var winLoss = mapper.Map<WinLoss>(rawWinLoss);
        if (winLoss.SportType is SportTypeConstants.VirtualSoccer or SportTypeConstants.VirtualBetByBasketball)
        {
            winLoss.AwayName = refService.GetTeamName3rd(rawWinLoss.AwayId);
            winLoss.HomeName = GetHomeName3rd(rawWinLoss.HomeId, rawWinLoss.IsNeutral);
        }
        else
        {
            winLoss.AwayName = refService.GetTeamName(rawWinLoss.AwayId);
            winLoss.HomeName = GetHomeName(rawWinLoss.HomeId, rawWinLoss.IsNeutral);
        }
        winLoss.LeagueName = rawWinLoss.LeagueName;
        winLoss.MatchCode = refService.GetMatchCode(winLoss.MatchCode);
        return winLoss;
    }

    private string GetHomeName3rd(int teamId, bool isNeutral)
    {
        return refService.GetTeamName3rd(teamId)
               + (isNeutral ? " (N)" : string.Empty);
    }

    private string GetHomeName(int teamId, bool isNeutral)
    {
        return refService.GetTeamName(teamId)
               + (isNeutral ? " (N)" : string.Empty);
    }

    private string GetLeague(string leagueCode, string leagueName)
    {
        return string.IsNullOrEmpty(leagueCode) ? leagueName : $"{leagueCode} - {leagueName}";
    }

    public IEnumerable<LeagueWinLoss> CalculateLeagues(IEnumerable<RawWinLoss> rawWinLoss)
    {
        return rawWinLoss.Where(x => x.LeagueId > 0 && x.MatchId > 0)
            .GroupBy(x => new { x.LeagueId, x.LeagueCode })
            .Select(x => CalculateLeague(x, x.Key.LeagueId, x.Key.LeagueCode))
            .OrderBy(x => x.LeagueName);
    }

    public LeagueWinLoss CalculateLeague(IEnumerable<RawWinLoss> rawWinLoss, int leagueId, string leagueCode)
    {
        var leagueTotal = CalculateSumWinLoss(rawWinLoss);
        leagueTotal.LeagueId = leagueId;
        leagueTotal.LeagueName = GetLeague(leagueCode, rawWinLoss.FirstOrDefault()?.LeagueName);
        leagueTotal.SportType = rawWinLoss.FirstOrDefault()?.SportType ?? 0;
        var matches = rawWinLoss.Select(FromRawWinLoss)
            .OrderBy(m => m.MatchCode)
            .ThenBy(m => m.HomeName);

        return new LeagueWinLoss
        {
            LeagueName = leagueTotal.LeagueName,
            Matches = matches.ToList(),
            Total = leagueTotal
        };
    }

    public WinLoss CalculateSumWinLoss(IEnumerable<RawWinLoss> rawWinLosses)
    {
        var matchWinLosses = rawWinLosses.Where(r => r.MatchId > 0 && r.LeagueId > 0);
        var sumWinLoss = matchWinLosses.DynamicSum();
        sumWinLoss.SportType = rawWinLosses.FirstOrDefault()?.SportType ?? 0;
        return FromRawWinLoss(sumWinLoss);
    }

    public IEnumerable<WinLoss> CalculateRM(IEnumerable<RawWinLoss> rawWinLosses)
    {
        return rawWinLosses.Where(r => r.MatchId == 0 && r.LeagueId == 0)
            .Select(FromRawWinLoss);
    }
}

public class Calculation3rdServiceImp(RemotingRef3rdServiceImp refService, IMapper mapper)
    : CalculationServiceImp(refService, mapper);
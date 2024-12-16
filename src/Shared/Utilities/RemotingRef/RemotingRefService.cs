using Fanex.RemotingRef;

namespace Utilities.RemotingRef;

public interface RemotingRefService
{
    string GetTeamName(int teamId);

    string GetLeagueName(int leagueId);

    string GetMatchCode(string matchCode);

    string GetSportName(int sportId);

    string GetBetTypeName(int betTypeId);
}

public class RemotingRefServiceImp(IRemotingRefService remotingRefService) : RemotingRefService
{
    public string GetTeamName(int teamId)
    {
        var teamIdString = teamId.ToString();
        return remotingRefService.GetTeamName(teamIdString, "en", string.Empty);
    }

    public string GetLeagueName(int leagueId)
    {
        var leagueIdString = leagueId.ToString();
        return remotingRefService.GetLeagueName(leagueIdString, "en", string.Empty);
    }

    public string GetMatchCode(string matchCode)
    {
        return string.IsNullOrEmpty(matchCode) ? string.Empty : remotingRefService.GetBetRadarMatchCodeFormat(matchCode, "en");
    }

    public string GetSportName(int sportId)
    {
        var sportIdString = sportId.ToString();
        return remotingRefService.GetSportName(sportIdString, "en");
    }

    public string GetBetTypeName(int betTypeId)
    {
        var betTypeIdString = betTypeId.ToString();
        return remotingRefService.GetBetTypeName(betTypeIdString, "en");
    }
}

public class RemotingRef3rdServiceImp(IRemotingRefService remotingRefService) : RemotingRefService
{
    public virtual string GetLeagueName(int leagueId)
    {
        var leagueIdString = leagueId.ToString();
        return remotingRefService.GetLeagueName3rd(leagueIdString, "en", string.Empty);
    }

    public virtual string GetTeamName(int teamId)
    {
        var teamIdString = teamId.ToString();
        return remotingRefService.GetTeamName3rd(teamIdString, "en", string.Empty);
    }

    public string GetMatchCode(string matchCode)
    {
        return string.IsNullOrEmpty(matchCode) ? string.Empty : remotingRefService.GetBetRadarMatchCodeFormat(matchCode, "en");
    }

    public string GetSportName(int sportId)
    {
        var sportIdString = sportId.ToString();
        return remotingRefService.GetSportName(sportIdString, "en");
    }

    public string GetBetTypeName(int betTypeId)
    {
        var betTypeIdString = betTypeId.ToString();
        return remotingRefService.GetBetTypeName(betTypeIdString, "en");
    }
}
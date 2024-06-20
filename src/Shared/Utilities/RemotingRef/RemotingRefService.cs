using Fanex.RemotingRef;

namespace Utilities.RemotingRef
{
    public interface RemotingRefService
    {
        string GetTeamName(int teamId);

        string GetLeagueName(int leagueId);

        string GetMatchCode(string matchCode);

        string GetSportName(int sportId);
    }

    public class RemotingRefServiceImp : RemotingRefService
    {
        private readonly IRemotingRefService remotingRefService;

        public RemotingRefServiceImp(IRemotingRefService remotingRefService)
        {
            this.remotingRefService = remotingRefService;
        }

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
            if (string.IsNullOrEmpty(matchCode))
            {
                return string.Empty;
            }

            return remotingRefService.GetBetRadarMatchCodeFormat(matchCode, "en");
        }

        public string GetSportName(int sportId)
        {
            var sportIdString = sportId.ToString();
            return remotingRefService.GetSportName(sportIdString, "en");
        }
    }

    public class RemotingRef3rdServiceImp : RemotingRefService
    {
        private readonly IRemotingRefService remotingRefService;

        public RemotingRef3rdServiceImp(IRemotingRefService remotingRefService)
        {
            this.remotingRefService = remotingRefService;
        }

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
            if (string.IsNullOrEmpty(matchCode))
            {
                return string.Empty;
            }

            return remotingRefService.GetBetRadarMatchCodeFormat(matchCode, "en");
        }

        public string GetSportName(int sportId)
        {
            var sportIdString = sportId.ToString();
            return remotingRefService.GetSportName(sportIdString, "en");
        }
    }
}

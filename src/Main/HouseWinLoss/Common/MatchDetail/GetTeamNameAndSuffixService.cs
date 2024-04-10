namespace HouseWinLoss.Common.MatchDetail
{
    public interface GetTeamNameAndSuffixService
    {
        string GetTeamNameWithSuffixByMatchCode(string teamName, string matchCode);
    }

    public class GetTeamNameAndSuffixServiceImp : GetTeamNameAndSuffixService
    {
        private static Dictionary<string, string> PlayOffBettype => new Dictionary<string, string>
        {
            { "(O)", "(O/U)" },
            { "(o)", "(O/U)" },
            { "( o)", "(O/U)" },
            { "( O)", "(O/U)" },
            { "(h)", "(HDP)" },
            { "(H)", "(HDP)" },
            { "( h)", "(HDP)" },
            { "( H)", "(HDP)" },
        };

        public string GetTeamNameWithSuffixByMatchCode(string teamName, string matchCode)
        {
            string matchCodeValue = matchCode.Replace(" ", string.Empty).ToLowerInvariant();
            var bettype = PlayOffBettype.FirstOrDefault(s => matchCodeValue.Contains(s.Key)).Value ?? string.Empty;
            return string.IsNullOrEmpty(bettype) ? teamName : $"{teamName} {bettype}";
        }
    }
}
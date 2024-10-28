using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace LeagueWinLoss.Model;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class LeagueSuggestionViewModel
{
    public string Query { get; set; }

    public List<string> Suggestions { get; set; }

    public string[] DataInfo { get; set; }
}
namespace LeagueWinLoss.Model;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class LeagueSuggestionViewModel
{
    public string Query { get; set; }

    public List<string> Suggestions { get; set; }

    public string[] DataInfo { get; set; }
}
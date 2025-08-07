namespace TGR.Model.JuiceLeftOver;

/// <summary>
/// Represents performance information for a specific bet type.
/// </summary>
public class BetTypePerformanceInfo : PerformanceInfoBase
{
    /// <summary>
    /// Gets or sets the bet type identifier.
    /// </summary>
    public short BetType { get; set; }

    /// <summary>
    /// Gets or sets the name of the bet type.
    /// </summary>
    public string TypeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of leagues.
    /// </summary>
    public int LeagueCount { get; set; }

    /// <summary>
    /// Gets or sets the score value.
    /// </summary>
    public decimal? Score { get; set; }
}
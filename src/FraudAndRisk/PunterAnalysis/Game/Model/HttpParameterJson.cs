using MediatR;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Game.Model;

public class HttpParameterJson : IRequest<SummaryEntity<GameModel>>
{
    public int PeriodType { get; set; } = 1;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CustId { get; set; }

    public string ListCombinationId { get; set; }

    public int OrderBy { get; set; } = 1;

    public int SortBy { get; set; } = 1;

    public string ListLeagueGroupId { get; set; }

    public int LeagueGroupId { get; set; }
}
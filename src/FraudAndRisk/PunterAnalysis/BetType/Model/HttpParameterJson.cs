namespace PunterAnalysis.BetType.Model;

using MediatR;
using Utilities.LinqDynamicSum;

public class HttpParameterJson : IRequest<SummaryEntity<BetTypeModel>>
{
    public int PeriodType { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CustId { get; set; }

    public string ListCombinationId { get; set; }

    public string LeagueId { get; set; }

    public int SportId { get; set; }

    public int OrderBy { get; set; } = 1;

    public int SortBy { get; set; } = 1;

    public string ListLeagueGroupId { get; set; }

    public int LeagueGroupId { get; set; }
}
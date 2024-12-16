using MediatR;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.GameBetType.Model;

public class HttpParameterJson : IRequest<SummaryEntity<GameBetTypeModel>>
{
    public int PeriodType { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CustId { get; set; }

    public string ListCombinationId { get; set; }

    public int LeagueGroupId { get; set; }

    public int OrderBy { get; set; } = 1;

    public int SortBy { get; set; } = 1;

    public int BettypeGroupId { get; set; }
}
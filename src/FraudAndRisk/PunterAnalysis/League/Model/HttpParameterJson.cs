namespace PunterAnalysis.League.Model;

using Constants;
using MediatR;

public class HttpParameterJson : IRequest<ReportModel>
{
    public int PeriodType { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CustId { get; set; }

    public string ListCombinationId { get; set; }

    public string LeagueId { get; set; }

    public int SportId { get; set; }

    public int BetTypeId { get; set; }

    public int LiveStatusId { get; set; } = CommonConstant.LiveAndNonLive;

    public string ListLeagueGroupId { get; set; }

    public int LeagueGroupId { get; set; }
}
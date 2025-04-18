namespace ResultAnalysis.Requests;

using MediatR;
using Models;

public class ResultAnalysisOfQuarterRequest : IRequest<ResultAnalysisOfQuarterModel>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int SportId { get; set; }

    public string LeagueGroupIds { get; set; }

    public string LeagueName { get; set; }
}
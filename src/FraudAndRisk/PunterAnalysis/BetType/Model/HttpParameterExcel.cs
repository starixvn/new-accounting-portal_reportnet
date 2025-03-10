namespace PunterAnalysis.BetType.Model;

using MediatR;
using OfficeOpenXml;

public class HttpParameterExcel : IRequest<ExcelPackage>
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

    public string Title { get; set; }

    public string Filter { get; set; }

    public string ListLeagueGroupId { get; set; }

    public int LeagueGroupId { get; set; }
}
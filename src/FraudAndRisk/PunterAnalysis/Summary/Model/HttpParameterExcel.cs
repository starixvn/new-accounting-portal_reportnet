using MediatR;
using OfficeOpenXml;

namespace PunterAnalysis.Summary.Model;

public class HttpParameterExcel : IRequest<ExcelPackage>
{
    public int TopRow { get; set; }

    public int OrderBy { get; set; }

    public int SortBy { get; set; }

    public int PeriodType { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int Depth { get; set; }

    public string Username { get; set; }

    public string ListCombinationId { get; set; }

    public string LeagueId { get; set; }

    public string Title { get; set; }

    public string Filter { get; set; }

    public string ListLeagueGroupId { get; set; }
}
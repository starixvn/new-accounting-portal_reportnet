namespace ResultAnalysis.Requests;

using MediatR;
using OfficeOpenXml;

public class ResultAnalysisExcelRequest: IRequest<ExcelPackage>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int SportId { get; set; }

    public string LeagueGroupIds { get; set; }

    public string LeagueName { get; set; }
}
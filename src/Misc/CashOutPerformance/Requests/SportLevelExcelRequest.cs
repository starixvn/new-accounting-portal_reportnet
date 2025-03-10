namespace CashOutPerformance.Requests;

using MediatR;
using OfficeOpenXml;

public class SportLevelExcelRequest : IRequest<ExcelPackage>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string SportBettype { get; set; }

    public string Sites { get; set; }

    public string Leagues { get; set; }

    public string LeagueGroupIDs { get; set; }

    public string GlobalShowTime { get; set; }

    public byte LiveIndicator { get; set; }

    public string Title { get; set; }

    public string[] BreadCrumb { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public string Server { get; set; }

    public bool IsShowFullColumn { get; set; }

    public bool ExchangeBy { get; set; }

    public int FilterType { get; set; }

    public bool? EvenOddCust { get; set; }

    public byte? PriceService { get; set; }
}
using MediatR;
using OfficeOpenXml;

namespace CashOutPerformance.Requests;

public class BetTypeDrillDownExcelRequest : IRequest<ExcelPackage>
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string SportBettype { get; set; }

    public string Sites { get; set; }

    public string Leagues { get; set; }

    public string LeagueGroupIDs { get; set; }

    public string GlobalShowTime { get; set; }

    public byte LiveIndicator { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public string Server { get; set; }

    public bool ExchangeBy { get; set; }

    public int FilterType { get; set; }

    public bool? EvenOddCust { get; set; }

    public int BetType { get; set; }

    public string OutCome { get; set; }

    public string Title { get; set; }

    public string[] BreadCrumb { get; set; }

    public string Type { get; set; }

    public string Guid { get; set; }
}
using MediatR;
using OfficeOpenXml;

namespace ActualMemberBet.Bettype;

public class HttpParameterExcel : IRequest<ExcelPackage>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int SiteType { get; set; }

    public int CurrencyId { get; set; }

    public string OddsType { get; set; }

    public string KeyId { get; set; }

    public string ProductName { get; set; }
}
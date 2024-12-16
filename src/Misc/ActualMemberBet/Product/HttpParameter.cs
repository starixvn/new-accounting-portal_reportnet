using MediatR;
using Utilities.LinqDynamicSum;

namespace ActualMemberBet.Product;

public class HttpParameter : IRequest<SummaryEntity<ProductActualMemberBet>>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int SiteType { get; set; }

    public int CurrencyId { get; set; }

    public string OddsType { get; set; }
}
using MediatR;

namespace UniversalReports.SMAWinLoss.VirtualRate
{
    public class VirtualRateHttpParameter : IRequest<IEnumerable<VirtualRateModel>>
    {
        public int CustId { get; set; }
    }
}
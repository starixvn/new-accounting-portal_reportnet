using MediatR;
using System.Collections.Generic;

namespace UniversalReports.SMAWinLoss.VirtualRate
{
    public class VirtualRateHttpParameter : IRequest<IEnumerable<VirtualRateModel>>
    {
        public int CustId { get; set; }
    }
}
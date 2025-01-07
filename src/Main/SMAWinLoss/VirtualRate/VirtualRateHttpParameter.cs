namespace SMAWinLoss.VirtualRate;

using MediatR;

public class VirtualRateHttpParameter : IRequest<IEnumerable<VirtualRateModel>>
{
    public int CustId { get; set; }
}
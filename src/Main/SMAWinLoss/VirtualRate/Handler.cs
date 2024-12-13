namespace SMAWinLoss.VirtualRate
{
    using MediatR;

    public class Handler(VirtualRateService service)
        : IRequestHandler<VirtualRateHttpParameter, IEnumerable<VirtualRateModel>>
    {
        public async Task<IEnumerable<VirtualRateModel>> Handle(VirtualRateHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.GetVirtualRate(request);
        }
    }
}
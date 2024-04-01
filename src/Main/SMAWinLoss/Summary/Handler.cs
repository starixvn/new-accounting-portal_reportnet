using MediatR;

namespace SMAWinLoss.Summary
{
    public class Handler : IRequestHandler<WinLossHttpParameter, WinLossModel>
    {
        private readonly WinLossService service;

        public Handler(WinLossService service)
        {
            this.service = service;
        }

        public async Task<WinLossModel> Handle(WinLossHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.GetSMAWinLoss(request);
        }
    }
}
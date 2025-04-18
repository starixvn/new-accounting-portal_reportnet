namespace SMAWinLoss.Summary;

using MediatR;

public class Handler(WinLossService service) : IRequestHandler<WinLossHttpParameter, WinLossModel>
{
    public async Task<WinLossModel> Handle(WinLossHttpParameter request, CancellationToken cancellationToken)
    {
        return await service.GetSMAWinLoss(request);
    }
}
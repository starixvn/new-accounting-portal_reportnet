using MediatR;

namespace UpdatedUsernameCredit.UpdatedCredit;

public class Handler(UpdatedCreditService service) : IRequestHandler<Message, IEnumerable<UpdatedCreditModel>>
{
    public async Task<IEnumerable<UpdatedCreditModel>> Handle(Message request, CancellationToken cancellationToken)
    {
        return await service.GetUpdatedCredit(request);
    }
}
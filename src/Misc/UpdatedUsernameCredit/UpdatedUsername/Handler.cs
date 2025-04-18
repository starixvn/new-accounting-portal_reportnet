namespace UpdatedUsernameCredit.UpdatedUsername;

using MediatR;

public class Handler(UpdatedUsernameService service) : IRequestHandler<Message, IEnumerable<UpdatedUsernameModel>>
{
    public async Task<IEnumerable<UpdatedUsernameModel>> Handle(Message request, CancellationToken cancellationToken)
    {
        return await service.GetUpdatedUsername(request);
    }
}
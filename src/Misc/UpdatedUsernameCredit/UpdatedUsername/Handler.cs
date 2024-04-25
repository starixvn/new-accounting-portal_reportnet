using MediatR;

namespace UpdatedUsernameCredit.UpdatedUsername
{
    public class Handler : IRequestHandler<Message, IEnumerable<UpdatedUsernameModel>>
    {
        private readonly UpdatedUsernameService service;

        public Handler(UpdatedUsernameService service)
        {
            this.service = service;
        }

        public async Task<IEnumerable<UpdatedUsernameModel>> Handle(Message request, CancellationToken cancellationToken)
        {
            return await service.GetUpdatedUsername(request);
        }
    }
}
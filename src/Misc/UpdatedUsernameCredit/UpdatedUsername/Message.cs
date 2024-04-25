using MediatR;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedUsername
{
    public class Message : BaseMessage, IRequest<IEnumerable<UpdatedUsernameModel>>
    {
    }
}
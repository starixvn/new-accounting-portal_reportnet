using MediatR;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedCredit
{
    public class Message : BaseMessage, IRequest<IEnumerable<UpdatedCreditModel>>;
}
namespace UpdatedUsernameCredit.UpdatedCredit;

using Common;
using MediatR;

public class Message : BaseMessage, IRequest<IEnumerable<UpdatedCreditModel>>;
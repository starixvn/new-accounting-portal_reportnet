namespace UpdatedUsernameCredit.UpdatedUsername;

using Common;
using MediatR;

public class Message : BaseMessage, IRequest<IEnumerable<UpdatedUsernameModel>>;
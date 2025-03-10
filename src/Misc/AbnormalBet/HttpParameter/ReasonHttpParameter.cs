namespace AbnormalBet.HttpParameter;

using MediatR;
using Model;

public class ReasonHttpParameter : IRequest<IEnumerable<FilterItemModel>>;
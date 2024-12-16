using AbnormalBet.Model;
using MediatR;

namespace AbnormalBet.HttpParameter;

public class ReasonHttpParameter : IRequest<IEnumerable<FilterItemModel>>;
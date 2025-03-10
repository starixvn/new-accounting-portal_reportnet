namespace AbnormalBet.HttpParameter;

using MediatR;
using Model;

public class SportHttpParameter : IRequest<IEnumerable<FilterItemModel>>;
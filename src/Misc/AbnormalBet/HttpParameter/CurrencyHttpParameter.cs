namespace AbnormalBet.HttpParameter;

using MediatR;
using Model;

public class CurrencyHttpParameter : IRequest<IEnumerable<FilterItemModel>>;
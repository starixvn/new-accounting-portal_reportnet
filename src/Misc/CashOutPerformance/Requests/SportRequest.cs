namespace CashOutPerformance.Requests;

using MediatR;
using Models;

public class SportRequest : IRequest<IEnumerable<FilterGroupItemModel>>;
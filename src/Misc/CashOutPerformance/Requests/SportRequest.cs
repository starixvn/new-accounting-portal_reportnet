using CashOutPerformance.Models;
using MediatR;

namespace CashOutPerformance.Requests;

public class SportRequest : IRequest<IEnumerable<FilterGroupItemModel>>;
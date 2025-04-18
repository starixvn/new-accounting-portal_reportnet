namespace CashOutPerformance.Requests;

using MediatR;
using Models;

public class LeagueGroupRequest : IRequest<IEnumerable<FilterItemModel>>;
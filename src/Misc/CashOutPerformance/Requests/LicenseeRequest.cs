namespace CashOutPerformance.Requests;

using MediatR;
using Models;

public class LicenseeRequest : IRequest<IEnumerable<SiteModel>>;
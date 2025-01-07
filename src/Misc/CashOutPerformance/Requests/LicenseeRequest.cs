using CashOutPerformance.Models;
using MediatR;

namespace CashOutPerformance.Requests;

public class LicenseeRequest : IRequest<IEnumerable<SiteModel>>;
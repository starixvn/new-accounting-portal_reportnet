using MediatR;
using System.Collections.Generic;

namespace SMAWinLoss.Filter
{
    public class SiteHttpParameter : IRequest<IEnumerable<Site>>
    {
    }
}
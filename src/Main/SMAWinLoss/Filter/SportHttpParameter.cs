using MediatR;
using System.Collections.Generic;

namespace SMAWinLoss.Filter
{
    public class SportHttpParameter : IRequest<IEnumerable<Sport>>
    {
    }
}
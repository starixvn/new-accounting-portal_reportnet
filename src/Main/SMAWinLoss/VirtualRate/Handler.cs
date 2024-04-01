using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UniversalReports.SMAWinLoss.VirtualRate
{
    public class Handler : IRequestHandler<VirtualRateHttpParameter, IEnumerable<VirtualRateModel>>
    {
        private readonly VirtualRateService service;

        public Handler(VirtualRateService service)
        {
            this.service = service;
        }

        public async Task<IEnumerable<VirtualRateModel>> Handle(VirtualRateHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.GetVirtualRate(request);
        }
    }
}
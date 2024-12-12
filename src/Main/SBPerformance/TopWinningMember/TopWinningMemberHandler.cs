using MediatR;
using Utilities.LinqDynamicSum;

namespace SBPerformance.TopWinningMember
{
    public class TopWinningMemberHandler(TopWinningMemberService service)
        : IRequestHandler<TopWinningMemberHttpParameter, SummaryEntity<TopWinningMemberItemModel>>
    {
        public async Task<SummaryEntity<TopWinningMemberItemModel>> Handle(TopWinningMemberHttpParameter request, CancellationToken cancellationToken)
        {
            return await service.Query(request);
        }
    }
}
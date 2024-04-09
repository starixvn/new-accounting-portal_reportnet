using Fanex.Data.Repository;
using MediatR;

namespace ActualMemberBet.Filter
{
    public class SiteHandler : IRequestHandler<SiteHttpParameter, IEnumerable<Site>>
    {
        private readonly IDynamicRepository repository;

        public SiteHandler(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Site>> Handle(SiteHttpParameter request, CancellationToken cancellationToken)
        {
            var criteria = new SiteCriteria();
            return await repository.FetchAsync<Site>(criteria);
        }
    }
}

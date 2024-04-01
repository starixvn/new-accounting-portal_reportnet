using AutoMapper;
using Fanex.Data.Repository;

namespace UniversalReports.SMAWinLoss.VirtualRate
{
    public interface VirtualRateService
    {
        Task<IEnumerable<VirtualRateModel>> GetVirtualRate(VirtualRateHttpParameter parameter);
    }

    public class VirtualRateServiceImp : VirtualRateService
    {
        private readonly IDynamicRepository repository;

        private readonly IMapper mapper;

        public VirtualRateServiceImp(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<VirtualRateModel>> GetVirtualRate(VirtualRateHttpParameter parameter)
        {
            var criteria = mapper.Map<VirtualRateCriteria>(parameter);
            return await repository.FetchAsync<VirtualRateModel>(criteria);
        }
    }
}
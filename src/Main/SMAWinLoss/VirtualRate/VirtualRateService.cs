using AutoMapper;
using Fanex.Data.Repository;

namespace UniversalReports.SMAWinLoss.VirtualRate
{
    public interface VirtualRateService
    {
        Task<IEnumerable<VirtualRateModel>> GetVirtualRate(VirtualRateHttpParameter parameter);
    }

    public class VirtualRateServiceImp(IDynamicRepository repository, IMapper mapper) : VirtualRateService
    {
        public async Task<IEnumerable<VirtualRateModel>> GetVirtualRate(VirtualRateHttpParameter parameter)
        {
            var criteria = mapper.Map<VirtualRateCriteria>(parameter);
            return await repository.FetchAsync<VirtualRateModel>(criteria);
        }
    }
}
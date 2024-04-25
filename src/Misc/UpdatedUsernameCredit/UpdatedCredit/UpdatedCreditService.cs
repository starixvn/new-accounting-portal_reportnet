using AutoMapper;
using Fanex.Data.Repository;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedCredit
{
    public interface UpdatedCreditService
    {
        Task<IEnumerable<UpdatedCreditModel>> GetUpdatedCredit(BaseMessage message);
    }

    public class UpdatedCreditServiceImp : UpdatedCreditService
    {
        private readonly IDynamicRepository repository;

        private readonly IMapper mapper;

        public UpdatedCreditServiceImp(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UpdatedCreditModel>> GetUpdatedCredit(BaseMessage message)
        {
            var criteria = mapper.Map<Criteria>(message);

            var rawData = await repository.FetchAsync<UpdatedCreditEntity>(criteria);

            return mapper.Map<List<UpdatedCreditModel>>(rawData.ToList());
        }
    }
}
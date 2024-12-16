using AutoMapper;
using Fanex.Data.Repository;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedCredit;

public interface UpdatedCreditService
{
    Task<IEnumerable<UpdatedCreditModel>> GetUpdatedCredit(BaseMessage message);
}

public class UpdatedCreditServiceImp(IDynamicRepository repository, IMapper mapper) : UpdatedCreditService
{
    public async Task<IEnumerable<UpdatedCreditModel>> GetUpdatedCredit(BaseMessage message)
    {
        var criteria = mapper.Map<Criteria>(message);

        var rawData = await repository.FetchAsync<UpdatedCreditEntity>(criteria);

        return mapper.Map<List<UpdatedCreditModel>>(rawData.ToList());
    }
}
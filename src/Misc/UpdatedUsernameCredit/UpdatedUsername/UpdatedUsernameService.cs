namespace UpdatedUsernameCredit.UpdatedUsername;

using AutoMapper;
using Common;
using Fanex.Data.Repository;

public interface UpdatedUsernameService
{
    Task<List<UpdatedUsernameModel>> GetUpdatedUsername(BaseMessage message);
}

public class UpdatedUsernameServiceImp(
    IDynamicRepository repository,
    LogParseService logParseService,
    IMapper mapper)
    : UpdatedUsernameService
{
    public async Task<List<UpdatedUsernameModel>> GetUpdatedUsername(BaseMessage message)
    {
        var criteria = mapper.Map<Criteria>(message);

        var rawData = await repository.FetchAsync<UpdatedUsernameEntity>(criteria);

        var result = rawData.Select(GetUpdatedUsernameItem).ToList();

        return result;
    }

    public UpdatedUsernameModel GetUpdatedUsernameItem(UpdatedUsernameEntity updatedUsernameEntity)
    {
        var updatedUsernameLog = logParseService.GetUsernameLog(updatedUsernameEntity.TrackingValue);
        var updatedUsername = mapper.Map<UpdatedUsernameModel>(updatedUsernameEntity);
        updatedUsername.FromUsername = updatedUsernameLog.FromName;
        updatedUsername.ToUsername = updatedUsernameLog.ToName;

        return updatedUsername;
    }
}
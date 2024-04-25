using AutoMapper;
using Fanex.Data.Repository;
using UpdatedUsernameCredit.Common;

namespace UpdatedUsernameCredit.UpdatedUsername
{
    public interface UpdatedUsernameService
    {
        Task<List<UpdatedUsernameModel>> GetUpdatedUsername(BaseMessage message);
    }

    public class UpdatedUsernameServiceImp : UpdatedUsernameService
    {
        private readonly IDynamicRepository repository;

        private readonly LogParseService logParseService;

        private readonly IMapper mapper;

        public UpdatedUsernameServiceImp(IDynamicRepository repository, LogParseService logParseService,  IMapper mapper)
        {
            this.repository = repository;
            this.logParseService = logParseService;
            this.mapper = mapper;
        }

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
}
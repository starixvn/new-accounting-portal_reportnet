using DuplicateTickets.Criteria;
using Fanex.Data.Repository;

namespace DuplicateTickets.Services
{
    public interface DuplicateTicketsCashOutService
    {
        Task<bool> IsDuplicateTicketsCashOut(int betTypeId, string transIds);
    }

    public class DuplicateTicketsCashOutServiceImp(IDynamicRepository repository) : DuplicateTicketsCashOutService
    {
        public async Task<bool> IsDuplicateTicketsCashOut(int betTypeId, string transIds)
        {
            if (betTypeId != 911)
            {
                return false;
            }

            var criteria = new DuplicateTicketsCheckCashOutCriteria { TransIds = transIds };

            await repository.ExecuteAsync(criteria);

            return criteria.IsDuplicated;

        }
    }
}
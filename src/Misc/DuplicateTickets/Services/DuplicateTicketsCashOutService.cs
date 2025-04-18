namespace DuplicateTickets.Services;

using Criteria;
using Fanex.Data.Repository;

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
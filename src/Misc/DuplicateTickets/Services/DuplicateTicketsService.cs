using DuplicateTickets.Criteria;
using DuplicateTickets.Entities;
using DuplicateTickets.Model;
using Fanex.Data.Repository;
using Fanex.Logging;
using Fanex.NAP.Client;
using Newtonsoft.Json;

namespace DuplicateTickets.Services;

public interface DuplicateTicketsService
{
    Task<DuplicateTicketsModel> GetDuplicateTicketByUserName(DuplicateTicketsCriteria criteria);
}

public class DuplicateTicketsServiceImp(
    IDynamicRepository repository,
    INapSession napSession,
    DuplicateTicketsCashOutService cashOutService,
    DuplicateTicketsUtility utility) : DuplicateTicketsService
{
    public async Task<DuplicateTicketsModel> GetDuplicateTicketByUserName(DuplicateTicketsCriteria criteria)
    {
        var duplicateTicketsInfo = new DuplicateTicketsModel();

        var result = new List<DuplicateTicketsByUserNameModel>();
        var reportData = await GetDuplicateTicketData(criteria);
        var duplicateTicketGroup = reportData.GroupBy(x => new
        {
            x.UserName,
            x.CustId,
            x.TypeName,
            x.WinLostDate,
            x.Balance,
            x.Stake,
            x.Odds,
            x.Bettype,
            x.RefNoGroup,
            x.MatchId,
            x.Ip,
            x.WalletType,
            x.BetTeam,
            x.OddsId
        }).ToList();


        foreach (var duplicateTicketByUserName in duplicateTicketGroup)
        {
            var mainTicket = utility.AssignMainTicketInfo(duplicateTicketByUserName);
            var subTickets = utility.AssignSubTicketInfo(duplicateTicketByUserName);
            var groupHavingMultipleTickets = subTickets.GroupBy(x => new { x.GroupNo }).Where(g => g.Count() > 1).ToList();
            foreach (var group in groupHavingMultipleTickets)
            {
                var transIds = string.Join(",", group.Select(s => s.ReferenceNumber));
                var isDuplicateCashOut = await cashOutService.IsDuplicateTicketsCashOut(mainTicket.BetTypeId, transIds);

                if (mainTicket.BetTypeId != 911 || isDuplicateCashOut)
                {
                    mainTicket.TicketInfo.AddRange(group);
                }
            }

            result.Add(mainTicket);
        }

        duplicateTicketsInfo.Rows = result;
        var numOfDuplicateTickets = result.Sum(x => x.TicketInfo.Count);
        duplicateTicketsInfo.HaveDuplicateTickets = numOfDuplicateTickets > 0;
        duplicateTicketsInfo.MD5Hash = utility.GetMD5Hash(JsonConvert.SerializeObject(result));


        if (result.Count <= 0)
        {
            return duplicateTicketsInfo;
        }

        var json = JsonConvert.SerializeObject(result, Formatting.Indented);
        await LogService.InfoAsync($"Misc / Duplicate Tickets by Username: {napSession.UserName}" + Environment.NewLine + json);

        return duplicateTicketsInfo;
    }

    private async Task<List<DuplicateTicketEntity>> GetDuplicateTicketData(DuplicateTicketsCriteria criteria)
    {
        return (await repository.FetchAsync<DuplicateTicketEntity>(criteria)).ToList();
    }
}
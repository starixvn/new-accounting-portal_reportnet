using AutoMapper;
using HouseWinLoss.Common.Models;
using Utilities.RemotingRef;

namespace HouseWinLoss.Common.Services
{
    public interface CalculationService
    {
        WinLoss FromRawWinLossByRowName(WinLossRaw rawWinLoss);

        SiteWinLossSummary CalculateSumWinLossSummary(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses);

        SiteWinLossSummary CalculateSiteChargedCom(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses);

        WinLoss CalculateSumWinLoss(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses);

        IEnumerable<SiteWinLossSummary> CalculateSiteWinLossSummary(IEnumerable<WinLossRaw> rawWinLosses);

        IEnumerable<WinLoss> CalculateRM(IEnumerable<WinLossRaw> rawWinLosses);

        IEnumerable<WinLoss> CalculateBetTypes(IEnumerable<WinLossRaw> rawWinLosses);
    }

    public class CalculationServiceImp(RemotingRefService refService, IMapper mapper) : CalculationService
    {
        private const string CreditName = @"Credit";

        private const string DepositName = @"Deposit";

        private const string CN88Name = @"CN88";

        public WinLoss FromRawWinLossByRowName(WinLossRaw rawWinLoss)
        {
            var winloss = FromRawWinLoss(rawWinLoss);
            winloss.Name = GetMatchCode(rawWinLoss.RowName);

            return winloss;
        }

        private WinLoss FromRawWinLossByGroupName(WinLossRaw rawWinLoss)
        {
            var winloss = FromRawWinLoss(rawWinLoss);
            winloss.Name = rawWinLoss.GroupName;
            return winloss;
        }

        private WinLoss FromRawWinLoss(WinLossRaw rawWinLoss)
        {
            var winLoss = mapper.Map<WinLoss>(rawWinLoss);
            winLoss.Id = rawWinLoss.GroupId;
            return winLoss;
        }

        public WinLossRaw CalculateSum(IEnumerable<WinLossRaw> winlosses)
        {
            if (!winlosses.Any())
            {
                return new WinLossRaw();
            }

            return new WinLossRaw
            {
                GroupId = winlosses.First().GroupId,
                RowName = winlosses.First().RowName,
                GroupName = winlosses.First().GroupName,
                Commission = winlosses.Sum(r => r.Commission),
                MemberWinLoss = winlosses.Sum(r => r.MemberWinLoss),
                RealIncome = winlosses.Sum(r => r.RealIncome),
                VirtualIncome = winlosses.Sum(r => r.VirtualIncome),
                Variance = winlosses.Sum(r => r.Variance),
                ExtraPT = winlosses.Sum(r => r.ExtraPT),
                RealTurnover = winlosses.Sum(r => r.RealTurnover),
                SMAWinLoss = winlosses.Sum(r => r.SMAWinLoss),
                VirtualTurnover = winlosses.Sum(r => r.VirtualTurnover),
                IsSabaSport = winlosses.First().IsSabaSport,
                SportType = winlosses.First().SportType
            };
        }

        public WinLoss CalculateSumWinLoss(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var totalRawWinLoss = CalculateSum(rawWinLosses);
            var totalWinLoss = FromRawWinLossByGroupName(totalRawWinLoss);
            totalWinLoss.Name = sumTitle;

            return totalWinLoss;
        }

        public virtual SiteWinLossSummary CalculateSumWinLossSummary(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var nonRMWinLosses = rawWinLosses.Where(r => r.GroupName != "RM");
            var grandTotal = new SiteWinLossSummary
            {
                Credit = SumSubGrandTotalWinLoss(nonRMWinLosses, CreditName),
                Deposit = SumSubGrandTotalWinLoss(nonRMWinLosses, DepositName),
                CN88 = SumSubGrandTotalWinLoss(nonRMWinLosses, CN88Name),
                Total = CalculateSumWinLoss(sumTitle, nonRMWinLosses)
            };

            return grandTotal;
        }

        public virtual SiteWinLossSummary CalculateSiteChargedCom(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var nonRMWinLosses = rawWinLosses.Where(r => r.GroupName != "RM" && r.GroupId != 500);
            var grandTotal = new SiteWinLossSummary
            {
                Credit = SumSubGrandTotalWinLoss(nonRMWinLosses, CreditName),
                Deposit = SumSubGrandTotalWinLoss(nonRMWinLosses, DepositName),
                CN88 = SumSubGrandTotalWinLoss(nonRMWinLosses, CN88Name),
                Total = CalculateSumWinLoss(sumTitle, nonRMWinLosses)
            };

            return grandTotal;
        }

        private WinLoss SumSubGrandTotalWinLoss(IEnumerable<WinLossRaw> rawWinLosses, string subName)
        {
            var subData = rawWinLosses.Where(r => r.RowName == subName);
            var sumData = CalculateSum(subData);

            var winloss = FromRawWinLossByRowName(sumData);
            winloss.Name = subName;

            return winloss;
        }

        public virtual IEnumerable<SiteWinLossSummary> CalculateSiteWinLossSummary(IEnumerable<WinLossRaw> rawWinLosses)
        {
            var groups = rawWinLosses
                            .Where(r => r.GroupName != "RM")
                            .Select(r => new { Name = r.GroupName, Id = r.GroupId })
                            .Distinct();
            return from @group in groups
                   let rawGroupWinLosses = rawWinLosses.Where(r => r.GroupId == @group.Id && r.GroupName == @group.Name).ToArray()
                   select CalculateSiteWinLossSummary(@group.Id, @group.Name, rawGroupWinLosses);
        }

        public virtual SiteWinLossSummary CalculateSiteWinLossSummary(int groupId, string groupName, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var rawCreditWinLoss = rawWinLosses.FirstOrDefault(r => r.RowName == CreditName);
            var rawDepositWinLoss = rawWinLosses.FirstOrDefault(r => r.RowName == DepositName);
            var rawCN88WinLoss = rawWinLosses.FirstOrDefault(r => r.RowName == CN88Name);

            var creditWinLoss = rawCreditWinLoss == null ? CreateEmptyWinLoss(groupId, CreditName) : FromRawWinLossByRowName(rawCreditWinLoss);
            var depositWinLoss = rawDepositWinLoss == null ? CreateEmptyWinLoss(groupId, DepositName) : FromRawWinLossByRowName(rawDepositWinLoss);
            var cn88WinLoss = rawCN88WinLoss == null ? CreateEmptyWinLoss(groupId, CN88Name) : FromRawWinLossByRowName(rawCN88WinLoss);
            var totalWinLoss = rawWinLosses.Any() ? CalculateSumWinLoss(groupName, rawWinLosses) : CreateEmptyWinLoss(groupId, groupName);

            return new SiteWinLossSummary
            {
                Credit = creditWinLoss,
                Deposit = depositWinLoss,
                CN88 = cn88WinLoss,
                Total = totalWinLoss
            };
        }

        public WinLoss CreateEmptyWinLoss(int id, string name)
        {
            return new WinLoss
            {
                Id = id,
                Name = name
            };
        }

        public IEnumerable<WinLoss> CalculateRM(IEnumerable<WinLossRaw> rawWinLosses)
        {
            return rawWinLosses.Where(r => r.GroupName == "RM")
                .Select(FromRawWinLossByRowName);
        }

        private string GetMatchCode(string matchCode)
        {
            return string.IsNullOrEmpty(matchCode) ?
                string.Empty :
                refService.GetMatchCode(matchCode);
        }

        public IEnumerable<WinLoss> CalculateBetTypes(IEnumerable<WinLossRaw> rawWinLosses)
        {
            return from rawWinLoss in rawWinLosses select FromRawWinLossByGroupName(rawWinLoss);
        }
    }
}
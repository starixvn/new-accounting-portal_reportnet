using AutoMapper;
using HouseWinLoss.Common.Models;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.Products;

public interface Service
{
    Task<ProductWinLoss> Query(QueryModel queryModel);
}

public class ServiceImp(
    RawWinLossService rawWinLossService,
    CalculationService calculationService,
    IMapper mapper)
    : Service
{
    public async Task<ProductWinLoss> Query(QueryModel queryModel)
    {
        var criteria = mapper.Map<Criteria>(queryModel);
        var rawWinLosses = await rawWinLossService.Query(criteria);

        var rawSportsbookProducts = rawWinLosses.Where(r => ProductConstant.NameSportsbook.Contains(r.GroupName));
        var rawNonSportsbookProducts = rawWinLosses.Where(r => !ProductConstant.NameSportsbook.Contains(r.GroupName) && r.GroupName != ProductConstant.RM);
        var rawRMWinLosses = rawWinLosses.Where(r => r.GroupName == ProductConstant.RM);

        var sportsbookProducts = GetProductGroup(rawSportsbookProducts, "Sportsbook Total");
        var nonSportsbookProducts = GetProductGroup(rawNonSportsbookProducts, "Non-Sports Total");
        var sportsbookRMWinLosses = GetRMWinLoss(rawRMWinLosses);
        var grandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", rawWinLosses);
        grandTotal = CalculateGrandTotal(grandTotal, sportsbookRMWinLosses);
        var grandTotalWithoutCN88 = CalculateGrandTotalWithoutCN88(grandTotal);

        var result = new ProductWinLoss
        {
            Sportsbook = sportsbookProducts,
            RM = sportsbookRMWinLosses,
            NonSportsbook = nonSportsbookProducts,
            GrandTotal = grandTotal,
            GrandTotalWithoutCN88 = grandTotalWithoutCN88
        };

        return result;
    }

    public ProductGroupWinLoss GetProductGroup(IEnumerable<WinLossRaw> rawWinLosses, string groupName)
    {
        var products = calculationService.CalculateSiteWinLossSummary(rawWinLosses).ToList();
        var total = calculationService.CalculateSumWinLossSummary(groupName, rawWinLosses);
        return new ProductGroupWinLoss
        {
            Products = products,
            Total = total
        };
    }

    public List<WinLoss> GetRMWinLoss(IEnumerable<WinLossRaw> rawWinLosses)
    {
        return rawWinLosses.Select(calculationService.FromRawWinLossByRowName).ToList();
    }

    private static SiteWinLossSummary CalculateGrandTotal(SiteWinLossSummary siteWinLoss, List<WinLoss> rawWinLoss)
    {
        var rawEightRM = rawWinLoss.Where(r => r.Name.Contains("8RM"));
        var rawBARM = rawWinLoss.Where(r => r.Name.Contains("BARM"));
        siteWinLoss.Credit = CalculateGrandTotalProduct(rawEightRM, siteWinLoss.Credit);
        siteWinLoss.Deposit = CalculateGrandTotalProduct(rawBARM, siteWinLoss.Deposit);

        siteWinLoss.Total = CalculateGrandTotalProduct(rawWinLoss, siteWinLoss.Total);

        return siteWinLoss;
    }

    private static WinLoss CalculateGrandTotalProduct(IEnumerable<WinLoss> rawWinLoss, WinLoss winLoss)
    {
        foreach (var item in rawWinLoss)
        {
            winLoss.SMAWinLoss += item.SMAWinLoss;
            winLoss.Commission += item.Commission;
            winLoss.Variance += item.Variance;
            winLoss.RealIncome += item.RealIncome;
            winLoss.ExtraPT += item.ExtraPT;
        }

        return winLoss;
    }

    private static WinLoss CalculateGrandTotalWithoutCN88(SiteWinLossSummary grandTotal)
    {
        return new WinLoss
        {
            Name = "Grand Total (without CN88)",
            MemberWinLoss = grandTotal.Total.MemberWinLoss - grandTotal.CN88.MemberWinLoss,
            SMAWinLoss = grandTotal.Total.SMAWinLoss - grandTotal.CN88.SMAWinLoss,
            ExtraPT = grandTotal.Total.ExtraPT - grandTotal.CN88.ExtraPT,
            Commission = grandTotal.Total.Commission - grandTotal.CN88.Commission,
            RealIncome = grandTotal.Total.RealIncome - grandTotal.CN88.RealIncome,
            VirtualIncome = grandTotal.Total.VirtualIncome - grandTotal.CN88.VirtualIncome,
            Variance = grandTotal.Total.Variance - grandTotal.CN88.Variance,
            RealTurnover = grandTotal.Total.RealTurnover - grandTotal.CN88.RealTurnover,
            VirtualTurnover = grandTotal.Total.VirtualTurnover - grandTotal.CN88.VirtualTurnover
        };
    }
}
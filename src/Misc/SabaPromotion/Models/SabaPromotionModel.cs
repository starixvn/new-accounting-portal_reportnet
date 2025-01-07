namespace SabaPromotion.Models;

public class SabaPromotionModel
{
    public IEnumerable<SabaPromotionItem> Items { get; set; }

    public SabaPromotionItem Total { get; set; }
}

public class SabaPromotionItem
{
    public DateTime Date { get; set; }

    public string BetType { get; set; }

    public string Site { get; set; }

    public decimal HouseBonusExpense { get; set; }
}
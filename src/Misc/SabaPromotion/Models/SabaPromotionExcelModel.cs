namespace SabaPromotion.Models;

using Epplus.Mapper.Annotations;

public class SabaPromotionExcelModel
{
    [Cell("A4")]
    public DateTime Date { get; set; }

    [Cell("B4")]
    public string BetType { get; set; }

    [Cell("C4")]
    public string Site { get; set; }

    [Cell("D4")]
    public decimal HouseBonusExpense { get; set; }
}
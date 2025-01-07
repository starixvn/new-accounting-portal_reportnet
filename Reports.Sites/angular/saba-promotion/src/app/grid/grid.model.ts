export interface SabaPromotionItem {
    Date: string;
    BetType: string;
    Site: string;
    HouseBonusExpense: number;
}

export interface SabaPromotion {
    Items: SabaPromotionItem[];
    Total: SabaPromotionItem;
}
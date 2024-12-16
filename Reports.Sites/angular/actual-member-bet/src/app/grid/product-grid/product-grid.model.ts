export interface ProductItem {
    KeyId: string;
    ProductName: string;
    BetCount: number;
    MemberTurnoverRM: number;
    MemberNetWinLoss: number;
    HouseRealTurnover: number;
    HouseVirtualTurnover: number;
    HouseRealIncome: number;
    HouseVirtualWinLoss: number;
}

export interface ProductData {
    Entities: ProductItem[];
    Total: ProductItem;
}

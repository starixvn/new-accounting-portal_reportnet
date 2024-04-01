export interface BettypeItem {
    BettypeName: string;
    MemberTurnoverMYOdds: number;
    BetCount: number;
    MemberTurnoverRM: number;
    MemberNetWinLoss: number;
    HouseRealTurnoverMYOdds: number;
    HouseVirtualTurnoverMyOdds: number;
    HouseRealIncome: number;
    HouseVirtualWinLoss: number;
}

export interface BettypeData {
    Entities: BettypeItem[];
    Total: BettypeItem;
}

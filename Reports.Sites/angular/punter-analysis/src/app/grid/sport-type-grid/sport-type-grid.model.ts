export interface SportTypeGridModel {
    custId: number;
    custName: string;
}

export interface SportTypeItem {
    SportId: number;
    SportName: string;
    WinLoss: number;
    Turnover: number;
    Margin: number;
    BetCount: number;
    LeagueGroupId: number;
}

export interface SportTypeData {
    Entities: SportTypeItem[];
    Total: SportTypeItem;
}

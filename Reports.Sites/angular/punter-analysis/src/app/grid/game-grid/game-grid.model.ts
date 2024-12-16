export interface GameGridModel {
    custId: number;
    custName: string;
    sportId: number;
    sportName: string;
    leagueGroupId: number;
}

export interface GameItem {
    LeagueGroupId: number;
    LeagueGroupName: string;
    WinLoss: number;
    Turnover: number;
    Margin: number;
    BetCount: number;
}

export interface GameData {
    Entities: GameItem[];
    Total: GameItem;
}

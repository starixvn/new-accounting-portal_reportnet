import { SportTypeGridModel } from '../sport-type-grid/sport-type-grid.model';

export interface BetTypeGridModel extends SportTypeGridModel {
    sportId: number;
    sportName: string;
    leagueGroupId: number;
}

export interface BetTypeItem {
    BetTypeId: number;
    BetTypeName: string;
    NonLiveWinLoss: number;
    NonLiveTurnover: number;
    NonLiveMargin: number;
    NonLiveBetCount: number;
    LiveWinLoss: number;
    LiveTurnover: number;
    LiveMargin: number;
    LiveBetCount: number;
    WinLoss: number;
    Turnover: number;
    Margin: number;
    BetCount: number;
    HasLeague: boolean;
}

export interface BetTypeData {
    Entities: BetTypeItem[];
    Total: BetTypeItem;
}

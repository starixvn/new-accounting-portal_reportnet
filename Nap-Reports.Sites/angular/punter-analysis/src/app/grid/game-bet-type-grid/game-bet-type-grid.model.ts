import { BetTypeGroupGridModel } from './../bet-type-group-grid/bet-type-group-grid.model';
import { SportTypeGridModel } from '../sport-type-grid/sport-type-grid.model';
import { GameGridModel } from '../game-grid/game-grid.model';

export interface GameBetTypeGridModel extends BetTypeGroupGridModel {
    betTypeGroupId: number;
    betTypeGroupName: string;
}

export interface GameBetTypeItem {
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

export interface GameBetTypeData {
    Entities: GameBetTypeItem[];
    Total: GameBetTypeItem;
}

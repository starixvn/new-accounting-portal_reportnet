import { GameGridModel } from './../game-grid/game-grid.model';

export interface BetTypeGroupGridModel extends GameGridModel {
    leagueGroupId: number;
    leagueGroupName: string;
}

export interface BetTypeGroupItem {
    BettypeGroupId: number;
    BettypeGroupName: string;
    WinLoss: number;
    Turnover: number;
    Margin: number;
    BetCount: number;
}

export interface BetTypeGroupData {
    Entities: BetTypeGroupItem[];
    Total: BetTypeGroupItem;
}

import { LiveStatus } from '../grid.model';
import { BetTypeGridModel } from '../bet-type-grid/bet-type-grid.model';

export interface LeagueGridModel extends BetTypeGridModel {
    betTypeId: number;
    betTypeName: string;
    liveStatusId: LiveStatus;
    leagueGroupName?: string;
    betTypeGroupId?: number;
    betTypeGroupName?: string;
}

export interface PeriodGroupItem {
    WinLoss: number;
    Turnover: number;
    Margin: number;
    BetCount: number;
}

export interface LeagueItem {
    LeagueName: string;
    PeriodGroups: PeriodGroupItem[];
}

export interface LeagueData {
    Entities: LeagueItem[];
    Total: LeagueItem;
    GroupNames: string[];
}

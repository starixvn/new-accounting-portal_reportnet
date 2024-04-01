import { LiveStatus, GridType } from '../grid.model';

export interface BetCountGridModel {
    custId: number;
    custName: string;
    fromGridType: GridType;
    sportId?: number;
    sportName?: string;
    betTypeId?: number;
    betTypeName?: string;
    liveStatusId?: LiveStatus;
    leagueGroupId?: number;
    leagueGroupName?: string;
    betTypeGroupId?: number;
    betTypeGroupName?: string;
}

export interface BetCountItem {
    Date: string;
    WinLoss: number;
    BetCount: number;
    WinCount: number;
    DrawCount: number;
}

export interface BetCountData {
    Entities: BetCountItem[];
    Total: BetCountItem;
}

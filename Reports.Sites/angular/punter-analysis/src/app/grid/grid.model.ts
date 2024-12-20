import { Observable } from 'rxjs/Observable';

export enum GridType {
    UserName,
    AssociatedAccount,
    BetCount,
    SportType,
    BetType,
    League,
    Game,
    BetTypeGroup,
    GameBetType
}

export interface Grid {
    title: string;
    hasExcel: boolean;
    query(): Observable<any>;
    exportExcel(): void;
}

export enum LiveStatus {
    Live = 1,
    NonLive = 0,
    Both = -1
}

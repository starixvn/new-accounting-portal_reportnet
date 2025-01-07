import { Observable } from 'rxjs';

export enum GridType {
    Sport,
    Event,
    SimpleBetType,
    BetType,
    MatchDetail,
    GameTime,
    WinningProbability,
    DetailGameTime
}

export interface Grid {
    title: string;
    query(): Observable<any>;
    exportExcel(): void;
}

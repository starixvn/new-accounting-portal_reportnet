import { Observable } from 'rxjs';

export enum GridType {
    Sport,
    Event,
    BetType,
    MatchDetail
}

export interface Grid {
    title: string;
    query(): Observable<any>;
    exportExcel(): void;
}

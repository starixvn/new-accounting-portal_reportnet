import { Observable } from 'rxjs';

export enum GridType {
    Sport,
    SportDetail
}

export interface Grid {
    title: string;
    query(): Observable<any>;
    exportExcel(): void;
}

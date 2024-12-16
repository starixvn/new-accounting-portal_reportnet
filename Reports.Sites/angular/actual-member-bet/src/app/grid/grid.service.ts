import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid, GridType } from './grid.model';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class GridService {
    filterModel: FilterModel;
    activeGrid: Grid;
    keyId: string;
    productName: string;
    generateBetTypeGridSubject: Subject<number>;
    showGridCompletedSubject: Subject<number>;
}

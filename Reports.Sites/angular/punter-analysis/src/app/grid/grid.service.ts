import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid, GridType } from './grid.model';

@Injectable()
export class GridService {
    filterModel: FilterModel;
    activeGrid: Grid;
    goBack: (index: number) => void;
    generate: (gridType: GridType) => void;
}

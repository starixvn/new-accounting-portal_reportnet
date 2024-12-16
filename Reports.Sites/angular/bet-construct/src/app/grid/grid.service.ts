import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid, GridType } from './grid.model';

@Injectable({
  providedIn: 'root'
})
export class GridService {
  filterModel!: FilterModel;
  activeGrid!: Grid;
  generate: ((gridType: GridType) => void) | undefined;
}

import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid, GridType, QueryModel } from './grid.model';

@Injectable({
  providedIn: 'root'
})
export class GridService {
  filterModel: FilterModel;
  activeGrid: Grid;
  queryModel: FilterModel;
  sportTitle: string;
  eventTitle: string;
  cachedLicenseeFilterModel: FilterModel;
  cachedSportFilterModel: FilterModel;
  generate: (gridType: GridType, queryModel: QueryModel) => void;
}

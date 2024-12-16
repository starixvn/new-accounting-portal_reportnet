import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid, GridType } from './grid.model';

@Injectable({
  providedIn: 'root'
})
export class GridService {
  filterModel: FilterModel;
  activeGrid: Grid;
  queryModel: any;
  mainTitle: string;
  leagueTitle: string;
  sportTitle: string;
  eventTitle: string;
  matchTitle: string;
  betTypeTitle: string;
  siteTitle: string;
  usernameTitle: string;
  api: string;
  clickedFromColumn: number;
  generate: (gridType: GridType, queryModel: any) => void;
}

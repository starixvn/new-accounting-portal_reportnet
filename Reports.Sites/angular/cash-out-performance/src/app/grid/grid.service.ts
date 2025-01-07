import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid, GridType } from './grid.model';
import { BetTypeDrillDownQueryModel } from '../common/model';

@Injectable({
  providedIn: 'root'
})
export class GridService {
  filterModel: FilterModel;
  orginalSportBettype: string;
  cachedSportBettypeForSport: string;
  cachedSportBettype: string;
  activeGrid: Grid;
  eventTitle: string;
  betTypeTitle: string;
  gameTimeTitle: string;
  winningProbabilityTitle: string;
  detailGameTimeTitle: string;
  matchDetailTitle: string;
  cachedLiveIndicator: number;
  betTypeDrillDownQueryModel: BetTypeDrillDownQueryModel;
  generate: (gridType: GridType) => void;
}

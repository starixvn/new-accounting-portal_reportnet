import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Grid, GridType } from '../grid.model';
import { BettypeGridService } from './bettype-grid.service';
import { GridService } from '../grid.service';
import { BetTypeLevelModel, TicketViewModel } from '../../../app/common/model';
import * as _ from 'lodash';
import { BetTypeDrillDownType } from 'src/app/common/constants';

@Component({
  selector: 'app-bettype-grid',
  templateUrl: './bettype-grid.component.html',
  styleUrls: ['./bettype-grid.component.scss']
})
export class BettypeGridComponent implements Grid {

  dataViewModel: TicketViewModel<BetTypeLevelModel>;

  constructor(
    private readonly service: BettypeGridService,
    private readonly gridService: GridService) {
  }

  get title(): string {
    return this.gridService.betTypeTitle;
  }

  query() {
    return new Observable(observer => {
      this.service.getTickets(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
      });
      observer.complete();
    });
  }

  exportExcel(): void {
    this.gridService.filterModel.sportBettype = this.gridService.cachedSportBettypeForSport;
    this.service.exportExcel(this.gridService.filterModel);
  }

  get hasData(): boolean {
    return !_.isNil(this.dataViewModel) && this.dataViewModel.Total > 0;
  }

  isShowNotAvailable(data: number) : boolean {
    return _.isNil(data);
  }

  isLive(): boolean {
    return this.gridService.filterModel.liveIndicator === 1;
  }

  showMatchDetailLevel(betType: number, betTypeName: string): void {
    this.gridService.matchDetailTitle = betTypeName;
    const sportBettypeList =
      this.gridService.cachedSportBettypeForSport.split(',').filter(x => x.split('_')[1] === betType.toString()).join(',');
    this.gridService.filterModel.sportBettype = sportBettypeList;
    this.gridService.filterModel.pageSize = 100;
    this.gridService.filterModel.pageIndex = 1;
    this.gridService.generate(GridType.MatchDetail);
  }

  showGameTimeLevel(outCome: string, betType: number, bettypeName: string): void {
    this.gridService.gameTimeTitle = `${bettypeName} | ${outCome}`;
    this.gridService.betTypeDrillDownQueryModel = _.assignIn(this.gridService.filterModel, { betType : betType, outCome: outCome, type: BetTypeDrillDownType.GAME_TIME });
    this.gridService.betTypeDrillDownQueryModel.sportBettype = this.gridService.cachedSportBettypeForSport;
    this.gridService.betTypeDrillDownQueryModel.pageIndex = 0;
    this.gridService.betTypeDrillDownQueryModel.pageSize = 0;
    this.gridService.generate(GridType.GameTime);
  }

  showWinningProbabilityLevel(betType: number, bettypeName: string): void {
    this.gridService.winningProbabilityTitle = `${bettypeName}`;
    this.gridService.betTypeDrillDownQueryModel =  _.assignIn(this.gridService.filterModel, { betType : betType, outCome: '', type: BetTypeDrillDownType.WINNING_PROBABILITY });
    this.gridService.betTypeDrillDownQueryModel.sportBettype = this.gridService.cachedSportBettypeForSport;
    this.gridService.betTypeDrillDownQueryModel.pageIndex = 0;
    this.gridService.betTypeDrillDownQueryModel.pageSize = 0;
    this.gridService.generate(GridType.WinningProbability);
  }

  showDetailGameTimeLevel(betType: number, bettypeName: string): void {
    this.gridService.detailGameTimeTitle = `${bettypeName}`;
    this.gridService.betTypeDrillDownQueryModel =  _.assignIn(this.gridService.filterModel, { betType : betType, outCome: '', type: BetTypeDrillDownType.DETAIL_GAME_TIME });
    this.gridService.betTypeDrillDownQueryModel.sportBettype = this.gridService.cachedSportBettypeForSport;
    this.gridService.betTypeDrillDownQueryModel.pageIndex = 0;
    this.gridService.betTypeDrillDownQueryModel.pageSize = 0;
    this.gridService.generate(GridType.DetailGameTime);
  }
}
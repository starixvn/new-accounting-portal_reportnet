import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Grid, GridType } from '../grid.model';
import { GridService } from '../grid.service';
import { BetTypeLevelModel, TicketViewModel } from '../../../app/common/model';
import * as _ from 'lodash';
import { SimpleBettypeGridService } from './simple-bettype-grid.service';

@Component({
  selector: 'app-simple-bettype-grid',
  templateUrl: './simple-bettype-grid.component.html',
  styleUrls: ['./simple-bettype-grid.component.scss']
})
export class SimpleBettypeGridComponent implements Grid {

  dataViewModel: TicketViewModel<BetTypeLevelModel>;
  originalPageSize = 100;

  constructor(
    private readonly service: SimpleBettypeGridService,
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

  showMatchDetailLevel(betType: number, betTypeName: string): void{
    this.gridService.matchDetailTitle = betTypeName;
    const sportBettypeList =
      this.gridService.cachedSportBettypeForSport.split(',').filter(x => x.split('_')[1] === betType.toString()).join(',');
    this.gridService.filterModel.sportBettype = sportBettypeList;
    this.gridService.filterModel.pageSize = this.originalPageSize;
    this.gridService.filterModel.pageIndex = 1;
    this.gridService.generate(GridType.MatchDetail);
  }

}

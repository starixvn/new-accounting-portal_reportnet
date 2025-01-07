import { Component, OnInit } from '@angular/core';
import { Grid } from '../grid.model';
import { Observable } from 'rxjs';
import { GameTimeService } from './game-time.service';
import { GridService } from '../grid.service';
import { BetTypeLevelModel, TicketViewModel } from '../../../app/common/model';
import * as _ from 'lodash';

@Component({
  selector: 'app-game-time-grid',
  templateUrl: './game-time-grid.component.html',
  styleUrls: ['./game-time-grid.component.scss']
})
export class GameTimeGridComponent implements Grid {

  dataViewModel: TicketViewModel<BetTypeLevelModel>;

  constructor(private readonly service: GameTimeService,
    private readonly gridService: GridService,) { }

  get title(): string {
    return this.gridService.gameTimeTitle;
  }

  query(): Observable<any> {
    return new Observable(observer => {
      this.service.getTickets(this.gridService.betTypeDrillDownQueryModel).subscribe(data => {
        this.dataViewModel = data;
      });
      observer.complete();
    });
  }

  isShowNotAvailable(data: number) : boolean {
    return _.isNil(data);
  }

  exportExcel(): void {
    this.service.exportExcel(this.gridService.betTypeDrillDownQueryModel);
  }
}

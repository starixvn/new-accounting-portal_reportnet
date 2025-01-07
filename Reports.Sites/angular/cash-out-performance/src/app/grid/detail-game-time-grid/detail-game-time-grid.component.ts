import { Component, OnInit } from '@angular/core';
import { Grid } from '../grid.model';
import { Observable } from 'rxjs';
import { DetailGameTimeGridService } from './detail-game-time-grid.service';
import { GridService } from '../grid.service';
import { BetTypeLevelModel, TicketViewModel } from 'src/app/common/model';
import * as _ from 'lodash';

@Component({
  selector: 'app-detail-game-time-grid',
  templateUrl: './detail-game-time-grid.component.html',
  styleUrls: ['./detail-game-time-grid.component.scss']
})
export class DetailGameTimeGridComponent implements Grid {

  dataViewModel: TicketViewModel<BetTypeLevelModel>;

  constructor(private readonly service: DetailGameTimeGridService,
    private readonly gridService: GridService,) { }

  get title(): string {
    return this.gridService.detailGameTimeTitle;
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

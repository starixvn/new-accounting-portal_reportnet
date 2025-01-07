import { Component, OnInit } from '@angular/core';
import { Grid } from '../grid.model';
import { Observable } from 'rxjs';
import * as _ from 'lodash';
import { WinningProbabilityGridService } from './winning-probability-grid.service';
import { GridService } from '../grid.service';
import { BetTypeLevelModel, TicketViewModel } from '../../../app/common/model';

@Component({
  selector: 'app-winning-probability-grid',
  templateUrl: './winning-probability-grid.component.html',
  styleUrls: ['./winning-probability-grid.component.scss']
})
export class WinningProbabilityGridComponent implements Grid {

  dataViewModel: TicketViewModel<BetTypeLevelModel>;

  constructor(private readonly service: WinningProbabilityGridService,
    private readonly gridService: GridService,) { }

  get title(): string {
    return this.gridService.winningProbabilityTitle;
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

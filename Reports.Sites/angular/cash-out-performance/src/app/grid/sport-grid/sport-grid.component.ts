import { Component } from '@angular/core';
import { PaginationInstance, PaginatePipe, PaginationService as NgxPaginationService } from 'ngx-pagination';
import { PaginationService } from 'nap-components/pagination';
import { Observable } from 'rxjs';
import { SportLevelModel, TicketViewModel } from '../../../app/common/model';
import { Grid, GridType } from '../grid.model';
import { GridService } from '../grid.service';
import { SportGridService } from './sport-grid.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-sport-grid',
  templateUrl: './sport-grid.component.html',
  styleUrls: ['./sport-grid.component.scss']
})
export class SportGridComponent implements Grid {

  dataViewModel: SportLevelModel;
  originalPageSize = 100;

  constructor(
    private readonly service: SportGridService,
    private readonly gridService: GridService) {
  }

  get title(): string {
    return 'MAIN';
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
    this.gridService.filterModel.sportBettype = this.gridService.cachedSportBettype;
    this.service.exportExcel(this.gridService.filterModel);
  }

  get hasData(): boolean {
    return !_.isNil(this.dataViewModel) && this.dataViewModel.TotalItems > 0;
  }

  isShowNotAvailable(data: number) : boolean {
    return _.isNil(data);
  }

  showEventStatusLevel(sportType: number, sportName: string): void{
    this.gridService.eventTitle = sportName;
    const sportBettypeList =
      this.gridService.orginalSportBettype.split(',').filter(x => x.split('_')[0] === sportType.toString()).join(',');
    this.gridService.filterModel.sportBettype = sportBettypeList;
    this.gridService.filterModel.liveIndicator = this.gridService.cachedLiveIndicator;
    this.gridService.filterModel.pageSize = this.originalPageSize;
    this.gridService.filterModel.pageIndex = 1;
    this.gridService.cachedSportBettypeForSport = sportBettypeList;
    this.gridService.generate(GridType.Event);
  }
}

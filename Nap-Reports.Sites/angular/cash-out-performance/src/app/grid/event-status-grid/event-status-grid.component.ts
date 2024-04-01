import { Component } from '@angular/core';
import { EventStatusLevelModel, TicketViewModel } from '../../../app/common/model';
import { Grid, GridType } from '../grid.model';
import { GridService } from '../grid.service';
import { PaginationInstance, PaginatePipe, PaginationService as NgxPaginationService } from 'ngx-pagination';
import { PaginationService } from 'nap-components/pagination';
import { Observable } from 'rxjs';
import { EventStatusService } from './event-status.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-event-status-grid',
  templateUrl: './event-status-grid.component.html',
  styleUrls: ['./event-status-grid.component.scss']
})
export class EventStatusGridComponent implements Grid {

  dataViewModel: TicketViewModel<EventStatusLevelModel>;
  paginationInstance: PaginationInstance;
  originalPageSize = 100;

  private paginatePipe: PaginatePipe;

  constructor(
    private readonly service: EventStatusService,
    private readonly gridService: GridService,
    private readonly paginationService: PaginationService,
    private readonly ngxPaginationService: NgxPaginationService) {
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
  }

  get title(): string {
    return this.gridService.eventTitle;
  }

  query() {
    return new Observable(observer => {
      this.service.getTickets(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
        this.paginationInstance = this.paginationService.createPaginationInstance(this.dataViewModel.Total, 'event-status-pagination');
        this.onPageChange();
      });
      observer.complete();
    });
  }

  exportExcel(): void {
    this.gridService.filterModel.sportBettype = this.gridService.cachedSportBettypeForSport;
    this.service.exportExcel(this.gridService.filterModel);
  }

  onPageChange(): void {
    this.createPaging();
  }

  private createPaging(): void {
    if (this.gridService.filterModel.isPageChanged) {
      this.paginationInstance.currentPage =
        this.paginationInstance.itemsPerPage !== this.originalPageSize ? 1 : this.paginationInstance.currentPage;
      this.gridService.filterModel.pageIndex = this.paginationInstance.currentPage;
      this.gridService.filterModel.pageSize = this.paginationInstance.itemsPerPage;

      this.service.getTickets(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
        this.createPagingTransform();
      });
    }

    this.createPagingTransform();
  }

  private createPagingTransform(): void {
    const paginateData = this.paginatePipe.transform(this.dataViewModel.TicketList, this.paginationInstance);
    this.dataViewModel.TicketList = _.cloneDeep(paginateData);
    this.gridService.filterModel.isPageChanged = true;
    this.originalPageSize = this.paginationInstance.itemsPerPage;
  }

  get hasData(): boolean {
    return !_.isNil(this.dataViewModel) && this.dataViewModel.Total > 0;
  }

  isShowNotAvailable(data: number) : boolean {
    return _.isNil(data);
  }

  showBetTypeLevel(liveStatus: number): void{
    this.gridService.betTypeTitle = this.getliveNonLiveText(liveStatus);
    this.gridService.filterModel.liveIndicator = liveStatus;
    this.gridService.filterModel.sportBettype = this.gridService.cachedSportBettypeForSport;
    this.gridService.filterModel.pageSize = this.originalPageSize;
    this.gridService.filterModel.pageIndex = 1;
    this.gridService.generate(GridType.BetType);
  }

  getliveNonLiveText(liveStatus: number): string {
    const status = liveStatus === 0 ? 'Non-Live' : 'Live';
    return `${this.gridService.eventTitle} ${status}`;
  }
}

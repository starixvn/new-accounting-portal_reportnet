import { Component } from '@angular/core';
import { PaginationInstance, PaginatePipe, PaginationService as NgxPaginationService } from 'ngx-pagination';
import { PaginationService } from 'nap-components/pagination';
import { Observable } from 'rxjs';
import { Grid, GridType } from '../grid.model';
import { BettypeGridService } from './bettype-grid.service';
import { GridService } from '../grid.service';
import { BetTypeLevelModel, TicketViewModel } from '../../../app/common/model';
import * as _ from 'lodash';

@Component({
  selector: 'app-bettype-grid',
  templateUrl: './bettype-grid.component.html',
  styleUrls: ['./bettype-grid.component.scss']
})
export class BettypeGridComponent implements Grid {

  dataViewModel: TicketViewModel<BetTypeLevelModel>;
  paginationInstance: PaginationInstance;
  originalPageSize = 100;

  private paginatePipe: PaginatePipe;

  constructor(
    private readonly service: BettypeGridService,
    private readonly gridService: GridService,
    private readonly paginationService: PaginationService,
    private readonly ngxPaginationService: NgxPaginationService) {
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
  }

  get title(): string {
    return this.gridService.betTypeTitle;
  }

  query() {
    return new Observable(observer => {
      this.service.getTickets(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
        this.paginationInstance = this.paginationService.createPaginationInstance(this.dataViewModel.Total, 'bettype-pagination');
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
      this.gridService.filterModel.sportBettype = this.gridService.cachedSportBettypeForSport;

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

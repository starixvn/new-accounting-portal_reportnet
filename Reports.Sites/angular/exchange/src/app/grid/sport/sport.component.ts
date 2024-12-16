import { Component } from '@angular/core';
import { ExchangeItemModel, ExchangeModel, Grid, GridType, QueryModel } from '../grid.model';
import { Observable } from 'rxjs';
import { SportService } from './sport.service';
import { GridService } from '../grid.service';
import { PaginatePipe, PaginationInstance, PaginationService as NgxPaginationService } from 'ngx-pagination';
import { PaginationService } from 'nap-components/pagination';
import * as _ from 'lodash';

@Component({
  selector: 'app-sport',
  templateUrl: './sport.component.html',
  styleUrls: ['./sport.component.scss']
})
export class SportComponent implements Grid {
  exchangeModel: ExchangeModel;
  displayExchange: ExchangeItemModel[];
  paginationInstance: PaginationInstance;
  private paginatePipe: PaginatePipe;

  constructor(
    private readonly sportService: SportService,
    private readonly gridService: GridService,
    private paginationService: PaginationService,
    private ngxPaginationService: NgxPaginationService,
  ) {
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
   }

  title: string = this.gridService.sportTitle;

  query(): Observable<ExchangeModel> {
    return new Observable(observer => {
      this.sportService.query().subscribe(data => {
          this.exchangeModel = data;
          this.paginationInstance = this.paginationService.createPaginationInstance(this.exchangeModel.Entities.length, 'sport-pagination');
          this.onPageChange();
          observer.complete();
      });
    });
  }

  showEvent(sport: string): void {
    this.gridService.eventTitle = sport;
    const queryModel: QueryModel = {
      licenseeName: this.gridService.sportTitle,
      eventType: sport,
      drillLevel: 2
    }
    this.gridService.generate(GridType.Event, queryModel);
  }

  onPageChange() {
    this.createExchangePaging();
  }

  exportExcel(): void {
    this.sportService.exportExcel();
  }

  private createExchangePaging() {
    const paginateExchange = this.paginatePipe.transform(this.exchangeModel.Entities, this.paginationInstance);
    this.displayExchange = _.cloneDeep(paginateExchange);
  }
}

import { Component } from '@angular/core';
import { ExchangeItemModel, ExchangeModel, Grid, GridType } from '../grid.model';
import { Observable } from 'rxjs';
import { GridService } from '../grid.service';
import { PaginatePipe, PaginationInstance, PaginationService as NgxPaginationService } from 'ngx-pagination';
import { PaginationService } from 'nap-components/pagination';
import * as _ from 'lodash';
import { EventService } from './event.service';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.scss']
})
export class EventComponent implements Grid {
  exchangeModel: ExchangeModel;
  displayExchange: ExchangeItemModel[];
  paginationInstance: PaginationInstance;
  private paginatePipe: PaginatePipe;

  constructor(
    private readonly eventService: EventService,
    private readonly gridService: GridService,
    private paginationService: PaginationService,
    private ngxPaginationService: NgxPaginationService,
  ) {
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
   }

  title: string = this.gridService.eventTitle;

  query(): Observable<ExchangeModel> {
    return new Observable(observer => {
      this.eventService.query().subscribe(data => {
          this.exchangeModel = data;
          this.paginationInstance = this.paginationService.createPaginationInstance(this.exchangeModel.Entities.length, 'event-pagination');
          this.onPageChange();
          observer.complete();
      });
    });
  }

  onPageChange() {
    this.createExchangePaging();
  }

  exportExcel(): void {
    this.eventService.exportExcel();
  }

  private createExchangePaging() {
    const paginateExchange = this.paginatePipe.transform(this.exchangeModel.Entities, this.paginationInstance);
    this.displayExchange = _.cloneDeep(paginateExchange);
  }

}

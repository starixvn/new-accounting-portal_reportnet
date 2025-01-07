import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { PaginationInstance, PaginatePipe, PaginationService as NgxPaginationService } from 'ngx-pagination';
import { PaginationService } from 'nap-components/pagination';
import { FilterModel } from '../filter/filter.model';
import { CustomerPerformanceByDangerModel, SummaryEntity } from './grid.model';
import { GridService } from './grid.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
  providers:[GridService]
})
export class GridComponent implements OnChanges {

  @Input() filterModel: FilterModel;
  dataModel: SummaryEntity;
  displayData: CustomerPerformanceByDangerModel[];
  breadcrumbItems: string[];
  paginationInstance: PaginationInstance;
  private paginatePipe: PaginatePipe;

  constructor(
    private readonly service: GridService,
    private readonly paginationService: PaginationService,
    private readonly ngxPaginationService: NgxPaginationService) {
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!this.filterModel) {
      return;
    }

    this.service.getData(this.filterModel).subscribe(data => {
      this.dataModel = data;
      const breadcrumb = this.Title;
      this.breadcrumbItems = [breadcrumb];
      this.paginationInstance = this.paginationService.createPaginationInstance(this.dataModel.Entities.length);
      this.onPageChange();
    });
  }

  exportExcel() {
    this.service.exportExcel(this.filterModel);
  }

  get hasData(): boolean {
    return !_.isNil(this.dataModel) && this.dataModel.Entities.length > 0;
  }

  get Title(): string {
    return `SB Customer Performance By Dangers Report From ${this.filterModel.fromDate} To ${this.filterModel.toDate}`;
  }

  onPageChange() {
    this.createExchangePaging();
  }

    private createExchangePaging() {
    const paginateExchange = this.paginatePipe.transform(this.dataModel.Entities, this.paginationInstance);
    this.displayData = _.cloneDeep(paginateExchange);
  }
}

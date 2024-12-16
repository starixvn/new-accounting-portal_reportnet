import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';
import { UpdatedCreditModel } from './updated-credit.model';
import { UpdatedCreditService } from './updated-credit.service';
import { BreadcrumbService } from './../common/breadcrumb.service';
import { FilterModel } from './../filter/filter.model';
import { PaginationInstance, PaginationService } from 'nap-components/pagination';
import { PaginationService as NgxPaginationService, PaginatePipe } from 'ngx-pagination';

@Component({
  selector: 'app-updated-credit',
  templateUrl: './updated-credit.component.html',
  styleUrls: ['./updated-credit.component.scss']
})
export class UpdatedCreditComponent implements OnInit {
  breadcrumbItems: string[];
  updatedCreditData: UpdatedCreditModel[];
  displayUpdatedCreditData: UpdatedCreditModel[];
  paginationInstance: PaginationInstance;
  private paginatePipe: PaginatePipe;

  constructor(
    private filterModel: FilterModel,
    private breadcrumbService: BreadcrumbService,
    private updatedCreditService: UpdatedCreditService,
    private paginationService: PaginationService,
    private ngxPaginationService: NgxPaginationService) {
      this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
    }

  ngOnInit() {
    this.query(this.filterModel);
  }

  private query(filterModel: FilterModel) {
      this.updatedCreditService.query(filterModel).subscribe(data => {
        const breadcrumb = this.breadcrumbService.createBreadcrumb(filterModel);
        this.breadcrumbItems = [breadcrumb];
        this.updatedCreditData = data;
        this.paginationInstance = this.paginationService.createPaginationInstance(this.updatedCreditData.length);
        this.onPageChange();
      });
  }

  exportExcel() {
    this.updatedCreditService.exportExcel(this.filterModel);
  }

  onPageChange() {
    this.createPagingUpdatedCreditData();
  }

  private createPagingUpdatedCreditData() {
    const paginateUpdatedCreditData = this.paginatePipe.transform(this.updatedCreditData, this.paginationInstance);
    this.displayUpdatedCreditData = _.cloneDeep(paginateUpdatedCreditData);
  }
}

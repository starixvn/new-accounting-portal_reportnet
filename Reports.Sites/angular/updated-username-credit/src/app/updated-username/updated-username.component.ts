import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';
import { UpdatedUsernameModel } from './updated-username.model';
import { UpdatedUsernameService } from './updated-username.service';
import { BreadcrumbService } from './../common/breadcrumb.service';
import { FilterModel } from './../filter/filter.model';
import { PaginationService as NgxPaginationService, PaginatePipe } from 'ngx-pagination';
import { PaginationService, PaginationInstance } from 'nap-components/pagination';

@Component({
  selector: 'app-changed-username',
  templateUrl: './updated-username.component.html',
  styleUrls: ['./updated-username.component.scss']
})
export class UpdatedUsernameComponent implements OnInit {
  breadcrumbItems: string[];
  updatedUsernameData: UpdatedUsernameModel[];
  displayUpdatedUsernameData: UpdatedUsernameModel[];
  paginationInstance: PaginationInstance;
  private paginatePipe: PaginatePipe;

  constructor(
    private filterModel: FilterModel,
    private breadcrumbService: BreadcrumbService,
    private updateUsernameService: UpdatedUsernameService,
    private paginationService: PaginationService,
    private ngxPaginationService: NgxPaginationService) {
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
  }

  ngOnInit() {
    this.query(this.filterModel);
  }

  private query(filterModel: FilterModel) {
    this.updateUsernameService.query(filterModel).subscribe(
      data => {
        const breadcrumb = this.breadcrumbService.createBreadcrumb(filterModel);
        this.breadcrumbItems = [breadcrumb];
        this.updatedUsernameData = data;
        this.paginationInstance = this.paginationService.createPaginationInstance(this.updatedUsernameData.length);
        this.onPageChange();
      });
  }

  exportExcel() {
    this.updateUsernameService.exportExcel(this.filterModel);
  }

  onPageChange() {
    this.createPagingUpdatedCreditData();
  }

  private createPagingUpdatedCreditData() {
    const paginateUpdatedUsernameData = this.paginatePipe.transform(this.updatedUsernameData, this.paginationInstance);
    this.displayUpdatedUsernameData = _.cloneDeep(paginateUpdatedUsernameData);
  }
}

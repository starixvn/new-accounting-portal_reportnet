import { Component } from '@angular/core';
import * as _ from 'lodash';
import { LicenseeModel, Grid, LicenseeItemModel } from './../common/model';
import { PaginationInstance, PaginatePipe, PaginationService  as NgxPaginationService } from 'ngx-pagination';
import { PaginationService } from 'nap-components/pagination';
import { LicenseeGridService } from './licensee-grid.service';
import { GridFilterService } from '../common/grid-filter.service';
import { FilterModel } from '../filter/filter.model';

@Component({
  selector: 'app-licensee-grid',
  templateUrl: './licensee-grid.component.html',
  styleUrls: ['./licensee-grid.component.scss'],
  providers: [
    LicenseeGridService
  ]
})

export class LicenseeGridComponent implements Grid {
  breadcrumbItems: string[];
  licenseeData: LicenseeItemModel[] = [];
  displayLicensee: LicenseeModel[];
  paginationInstance: PaginationInstance;
  private paginatePipe: PaginatePipe;
  private filterModel: FilterModel;

  constructor(
    private gridFilterService: GridFilterService,
    private readonly licenseeGridService: LicenseeGridService,
    private paginationService: PaginationService,
    private ngxPaginationService: NgxPaginationService) {
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
  }

  query() {
    this.filterModel = this.gridFilterService.filterModel;
    this.licenseeGridService.query(this.filterModel).subscribe(data => {
      this.licenseeData = data.Licensees;
      const breadcrumb = this.breadcrumbTitle;
      this.breadcrumbItems = [breadcrumb];
      this.paginationInstance = this.paginationService.createPaginationInstance(data.TotalItems);
      this.onPageChange();
    });
  }

  exportExcel() {
    this.licenseeGridService.exportExcel(this.filterModel);
  }

  get hasExcel(): boolean {
    return this.hasData;
  }

  get hasData(): boolean {
    return !_.isNil(this.licenseeData) && this.licenseeData.length > 0;
  }

  onPageChange() {
    this.createPaging();
  }

  private createPaging() {
    if (this.filterModel.isPageChanged) {
      const itemsPerPage = this.paginationInstance.itemsPerPage;
      const isChangePageSize = itemsPerPage !== this.filterModel.pageSize;

      this.filterModel.pageIndex = isChangePageSize ? 1 : this.paginationInstance.currentPage;
      this.filterModel.pageSize = itemsPerPage;

      this.licenseeGridService.query(this.filterModel).subscribe(data => {
        this.licenseeData = data.Licensees;

        if (isChangePageSize) {
          this.paginationInstance.currentPage = 1;
        }

        this.createPagingTransform();
      });
    }

    this.createPagingTransform();
  }

  private createPagingTransform() {
    const paginateData = this.paginatePipe.transform(this.licenseeData, this.paginationInstance);
    this.displayLicensee = _.cloneDeep(paginateData);
    this.filterModel.isPageChanged = true;
  }

  private get breadcrumbTitle(): string {
    return `Abnormal Bet From From ${this.filterModel.fromDate} To ${this.filterModel.toDate}`;
  }

  isMixParlay(sport: string): boolean {
    return sport === '-';
  }
}

import { Component } from '@angular/core';
import * as _ from 'lodash';
import { PaginationInstance, PaginatePipe, PaginationService as NgxPaginationService } from 'ngx-pagination';
import { PaginationService } from 'nap-components/pagination';
import { Grid, MemberItemModel, MemberModel } from './../common/model';
import { MemberGridService } from './member-grid.service';
import { GridFilterService } from '../common/grid-filter.service';
import { FilterModel } from '../filter/filter.model';

@Component({
  selector: 'app-member-grid',
  templateUrl: './member-grid.component.html',
  styleUrls: ['./member-grid.component.scss'],
  providers: [
    MemberGridService
  ]
})
export class MemberGridComponent implements Grid {
  breadcrumbItems: string[];
  memberData: MemberItemModel[] = [];
  displayMember: MemberModel[];
  paginationInstance: PaginationInstance;
  private paginatePipe: PaginatePipe;
  private filterModel: FilterModel;

  constructor(
    private gridFilterService: GridFilterService,
    private readonly memberGridService: MemberGridService,
    private paginationService: PaginationService,
    private ngxPaginationService: NgxPaginationService) {
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
  }

  query() {
    this.filterModel = this.gridFilterService.filterModel;

    this.memberGridService.query(this.filterModel).subscribe(data => {
      const breadcrumb = this.breadcrumbTitle;
      this.breadcrumbItems = [breadcrumb];
      this.memberData = data.Members;
      this.paginationInstance = this.paginationService.createPaginationInstance(data.TotalItems);
      this.onPageChange();
    });
  }

  exportExcel() {
    this.memberGridService.exportExcel(this.filterModel);
  }

  get hasExcel(): boolean {
    return this.hasData;
  }

  get hasData(): boolean {
    return !_.isNil(this.memberData) && this.memberData.length > 0;
  }

  get title(): string {
    return 'Member';
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

      this.memberGridService.query(this.filterModel).subscribe(data => {
        this.memberData = data.Members;

        if (isChangePageSize) {
          this.paginationInstance.currentPage = 1;
        }

        this.createPagingTransform();
      });
    }

    this.createPagingTransform();
  }

  private createPagingTransform() {
    const paginateData = this.paginatePipe.transform(this.memberData, this.paginationInstance);
    this.displayMember = _.cloneDeep(paginateData);
    this.filterModel.isPageChanged = true;
  }

  private get breadcrumbTitle(): string {
    return `Abnormal Bet From From ${this.filterModel.fromDate} To ${this.filterModel.toDate}`;
  }

  isMixParlay(sport: string): boolean {
    return sport === '-';
  }
}

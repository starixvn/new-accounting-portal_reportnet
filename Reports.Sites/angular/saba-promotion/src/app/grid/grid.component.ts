import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { SabaPromotion } from './grid.model';
import { GridService } from './grid.service';
import { FilterModel } from '../filter/filter.model';
import { PaginationService as NgxPaginationService, PaginatePipe } from 'ngx-pagination';
import { PaginationInstance, PaginationService } from 'nap-components/pagination';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnChanges {
  @Input() filterModel!: FilterModel;
  data!: SabaPromotion;
  displayedData: SabaPromotion = { Items: [], Total: {
    Date: '',
    BetType: '',
    Site: '',
    HouseBonusExpense: 0
  } };
  paginationInstance!: PaginationInstance;
  paginatePipe!: PaginatePipe;
  itemsPerPage = 0;

  constructor(private readonly gridService: GridService,
    private readonly paginationService: PaginationService,
    private readonly ngxPaginationService: NgxPaginationService
  ) { 
    this.paginatePipe = new PaginatePipe(this.ngxPaginationService);
  }

  ngOnChanges(): void {
    if (this.filterModel !== undefined) {
      this.gridService.getSabaPromotions(this.filterModel).subscribe(data => {
        this.data = data;
        this.paginationInstance = this.paginationService.createPaginationInstance(this.data.Items.length);
        this.onPageChange();
      });
    }
  }

  onPageChange() {
    if (this.itemsPerPage !== this.paginationInstance.itemsPerPage) {
      this.paginationInstance.currentPage = 1;
    }
    const items = this.paginatePipe.transform(this.data.Items, this.paginationInstance);
    this.displayedData.Items = items;
    this.displayedData.Total = this.data.Total;
    this.itemsPerPage = this.paginationInstance.itemsPerPage;
  }

  exportExcel() {
    this.gridService.exportExcel(this.filterModel);
  }

}

import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { SabaPromotion } from './grid.model';
import { GridService } from './grid.service';
import { FilterModel } from '../filter/filter.model';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnChanges {
  @Input() filterModel!: FilterModel;
  data!: SabaPromotion;

  constructor(private readonly gridService: GridService) { }

  ngOnChanges(): void {
    if (this.filterModel !== undefined) {
      this.gridService.getSabaPromotions(this.filterModel).subscribe(data => this.data = data);
    }
  }

  exportExcel() {
    
  }

}

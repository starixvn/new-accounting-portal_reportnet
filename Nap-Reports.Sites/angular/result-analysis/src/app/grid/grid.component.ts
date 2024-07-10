import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { ResultAnalysisModel } from './grid.model';
import { GridService } from './grid.service';
import { CommonConstants } from '../common/constants';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnChanges {
  @Input() filterModel!: FilterModel;
  data!: ResultAnalysisModel;
  title: string = '';

  constructor(private readonly service: GridService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.filterModel !== undefined) {
      this.service.getData(this.filterModel).subscribe(data => this.data = data);
      this.title = `Result Analysis Report From ${this.filterModel.fromDate} To ${this.filterModel.toDate}`;
    }
  }

  exportExcel(): void {
    this.service.exportExcel(this.filterModel);
  }

  isShowTGResult(): boolean {
    return this.filterModel.sportId === CommonConstants.SOCCER;
  }
}

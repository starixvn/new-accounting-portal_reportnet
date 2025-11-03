import { Component } from '@angular/core';
import { Grid, GridType } from '../grid.model';
import { Observable } from 'rxjs';
import { ReportModel } from '../../../app/common/model';
import { GridService } from '../grid.service';

@Component({
  selector: 'app-match-grid',
  templateUrl: './match-grid.component.html',
  styleUrls: ['./match-grid.component.scss']
})
export class MatchGridComponent implements Grid {
  dataViewModel!: ReportModel;

  constructor(private readonly gridService: GridService
  ) { }

  get title(): string {
    return this.gridService.filterModel.leagueName;
  }
  
  query(): Observable<ReportModel> {
    return new Observable(observer => {
      this.gridService.getData(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
      });
      observer.complete();
    });
  }

  exportExcel(): void {
    this.gridService.exportExcel(this.gridService.filterModel);
  }
}

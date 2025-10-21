import { Component } from '@angular/core';
import { Grid } from '../grid.model';
import { Observable } from 'rxjs';
import { SportModel } from 'src/app/common/model';
import { GridService } from '../grid.service';
import { SportGridService } from '../sport-grid/sport-grid.service';

@Component({
  selector: 'app-bettype-grid',
  templateUrl: './bettype-grid.component.html',
  styleUrls: ['./bettype-grid.component.scss']
})
export class BetTypeGridComponent implements Grid {
  dataViewModel!: SportModel;

  constructor(private readonly gridService: GridService,
    private readonly service: SportGridService
  ) { }

  get title(): string {
    return this.gridService.filterModel.sportName;
  }
  
  query(): Observable<SportModel> {
    return new Observable(observer => {
      this.service.getSports(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
      });
      observer.complete();
    });
  }

  exportExcel(): void {
    this.service.exportExcel(this.gridService.filterModel);
  }
}

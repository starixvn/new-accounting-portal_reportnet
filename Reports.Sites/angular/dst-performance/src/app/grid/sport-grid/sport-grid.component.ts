import { Component } from '@angular/core';
import { Grid, GridType } from '../grid.model';
import { Observable } from 'rxjs';
import { SportModel } from 'src/app/common/model';
import { GridService } from '../grid.service';
import { SportGridService } from './sport-grid.service';

@Component({
  selector: 'app-sport-grid',
  templateUrl: './sport-grid.component.html',
  styleUrls: ['./sport-grid.component.scss']
})
export class SportGridComponent implements Grid {
  dataViewModel!: SportModel;

  constructor(private readonly gridService: GridService,
    private readonly service: SportGridService
  ) { }

  get title(): string {
    return 'Main';
  }
  
  query(): Observable<SportModel> {
    return new Observable(observer => {
      this.service.getSports(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
      });
      observer.complete();
    });
  }

  showBetType(sportId: number, sportName: string, matchCount: number): void {
    this.gridService.filterModel.sportName = sportName;
    this.gridService.generate!(GridType.BetType);
  }

  exportExcel(): void {
    this.service.exportExcel(this.gridService.filterModel);
  }
}

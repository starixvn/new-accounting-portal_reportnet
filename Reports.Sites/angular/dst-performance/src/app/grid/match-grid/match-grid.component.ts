import { Component } from '@angular/core';
import { Grid, GridType } from '../grid.model';
import { Observable } from 'rxjs';
import { SportModel } from 'src/app/common/model';
import { GridService } from '../grid.service';
import { MatchGridService } from './match-grid.service';

@Component({
  selector: 'app-match-grid',
  templateUrl: './match-grid.component.html',
  styleUrls: ['./match-grid.component.scss']
})
export class MatchGridComponent implements Grid {
  dataViewModel!: SportModel;

  constructor(private readonly gridService: GridService,
    private readonly service: MatchGridService
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

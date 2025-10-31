import { Component } from '@angular/core';
import { Grid, GridType } from '../grid.model';
import { Observable } from 'rxjs';
import { ReportModel } from '../../../app/common/model';
import { GridService } from '../grid.service';
import { LevelConstants } from '../../../app/common/constants';
import { FilterModel } from 'src/app/filter/filter.model';

@Component({
  selector: 'app-league-grid',
  templateUrl: './league-grid.component.html',
  styleUrls: ['./league-grid.component.scss']
})
export class LeagueGridComponent implements Grid {
  dataViewModel!: ReportModel;
  originalFilterModel!: FilterModel

  constructor(private readonly gridService: GridService
  ) { }

  get title(): string {
    return this.gridService.filterModel.betTypeName;
  }
  
  query(): Observable<ReportModel> {
    return new Observable(observer => {
      this.gridService.getData(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
        this.originalFilterModel = { ...this.gridService.filterModel };
      });
      observer.complete();
    });
  }

  showMatch(leagueId: number, leagueName: string): void {
    this.gridService.filterModel.leagueName = leagueName;
    this.gridService.filterModel.leagueId = leagueId;
    this.gridService.filterModel.level = LevelConstants.MATCH;
    this.gridService.generate!(GridType.Match);
  }

  exportExcel(): void {
    this.gridService.exportExcel(this.originalFilterModel);
  }
}

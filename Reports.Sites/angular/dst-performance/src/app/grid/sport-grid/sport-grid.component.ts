import { Component } from '@angular/core';
import { Grid, GridType } from '../grid.model';
import { Observable } from 'rxjs';
import { ReportModel } from '../../../app/common/model';
import { GridService } from '../grid.service';
import { LevelConstants } from '../../../app/common/constants';

@Component({
  selector: 'app-sport-grid',
  templateUrl: './sport-grid.component.html',
  styleUrls: ['./sport-grid.component.scss']
})
export class SportGridComponent implements Grid {
  dataViewModel!: ReportModel;
  fromDate!: string;
  toDate!: string;

  constructor(private readonly gridService: GridService
  ) { }

  get title(): string {
    return 'Main';
  }
  
  query(): Observable<ReportModel> {
    return new Observable(observer => {
      this.gridService.getData(this.gridService.filterModel).subscribe(data => {
        this.dataViewModel = data;
        this.fromDate = this.gridService.filterModel.fromDate;
        this.toDate = this.gridService.filterModel.toDate;
      });
      observer.complete();
    });
  }

  showBetType(sportId: number, sportName: string): void {
    this.gridService.filterModel.sportName = sportName;
    this.gridService.filterModel.sportId = sportId;
    this.gridService.filterModel.fromDate = this.fromDate;
    this.gridService.filterModel.toDate = this.toDate;
    this.gridService.filterModel.betType = undefined;
    this.gridService.filterModel.leagueId = undefined;
    this.gridService.filterModel.level = LevelConstants.BETTYPE;
    this.gridService.generate!(GridType.BetType);
  }

  exportExcel(): void {
    this.gridService.exportExcel(this.gridService.filterModel);
  }
}

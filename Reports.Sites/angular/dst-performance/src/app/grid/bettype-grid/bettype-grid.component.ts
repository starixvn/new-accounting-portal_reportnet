import { Component } from '@angular/core';
import { Grid, GridType } from '../grid.model';
import { Observable } from 'rxjs';
import { ReportModel } from '../../../app/common/model';
import { GridService } from '../grid.service';
import * as moment from 'moment';
import { LevelConstants } from '../../../app/common/constants';
import { FilterModel } from '../../../app/filter/filter.model';

@Component({
  selector: 'app-bettype-grid',
  templateUrl: './bettype-grid.component.html',
  styleUrls: ['./bettype-grid.component.scss']
})
export class BetTypeGridComponent implements Grid {
  dataViewModel!: ReportModel;
  originalFilterModel!: FilterModel;

  constructor(private readonly gridService: GridService
  ) { }

  get title(): string {
    return this.gridService.filterModel.sportName;
  }

  showLeague(betType: number, betTypeName: string, date: Date): void {
      this.gridService.filterModel.betTypeName = betTypeName;
      this.gridService.filterModel.betType = betType;
      this.gridService.filterModel.fromDate = moment(date).format('MMM DD, YYYY');
      this.gridService.filterModel.toDate = moment(date).format('MMM DD, YYYY');
      this.gridService.filterModel.leagueId = undefined;
      this.gridService.filterModel.level = LevelConstants.LEAGUE;
      this.gridService.generate!(GridType.League);
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

  exportExcel(): void {
    this.gridService.exportExcel(this.originalFilterModel);
  }
}

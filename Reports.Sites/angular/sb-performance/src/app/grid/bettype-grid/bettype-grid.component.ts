import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs-compat/Observable';
import { ReportTypeConstant } from '../grid.constant';
import { Grid, GridType } from '../grid.model';
import { GridService } from '../grid.service';
import { BetTypeDataModel } from './bettype-grid.model';
import { BettypeGridService } from './bettype-grid.service';

@Component({
  selector: 'app-bettype-grid',
  templateUrl: './bettype-grid.component.html',
  styleUrls: ['./bettype-grid.component.scss']
})
export class BettypeGridComponent implements Grid {
  data: BetTypeDataModel;
  sortColumnName = '';
  sortDirection = '';

  constructor(
    private betTypeGridService: BettypeGridService,
    private readonly gridService: GridService
  ) { }

  query() {
        return new Observable(observer => {
          this.betTypeGridService.query().subscribe(data => {
              this.data = data;
              observer.complete();
          });
      });
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        return this.gridService.betTypeTitle;
    }

    get isShowFullColumn(): boolean {
      return this.gridService.filterModel.isShowFullColumn;
    }

    get isShowBettypeDrillDown(): boolean {
      return this.gridService.mainTitle === ReportTypeConstant.TOP_LOSING_LEAGUES_TEXT;
    }

    get isHideActiveMember(): boolean {
      return this.gridService.mainTitle === ReportTypeConstant.TOP_WINNING_MEMBERS_TEXT;
    }

    sort(sortColumnName: string) {
      if (this.sortColumnName !== sortColumnName) {
        this.sortDirection = '';
      }

      this.sortColumnName = sortColumnName;
      this.sortDirection = this.sortDirection !== 'asc' ? 'asc' : 'desc';
      this.data.Entities = _.orderBy(this.data.Entities, this.sortColumnName, this.sortDirection !== 'asc' ? 'asc' : 'desc');
    }

    showUsername(id: string, bettype: string) {
      this.gridService.usernameTitle = bettype;
      this.gridService.generate(GridType.Username, { BetType: Number(id) });
    }
}

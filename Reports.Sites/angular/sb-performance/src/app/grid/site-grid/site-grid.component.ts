import { Component } from '@angular/core';
import { Grid, GridType } from '../grid.model';
import { SiteDataModel } from './site-grid.model';
import { SiteGridService } from './site-grid.service';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { GridService } from '../grid.service';
import { GridConstant, ReportTypeConstant } from '../grid.constant';

@Component({
  selector: 'app-site-grid',
  templateUrl: './site-grid.component.html',
  styleUrls: ['./site-grid.component.scss']
})
export class SiteGridComponent implements Grid {
  data: SiteDataModel;
  sortColumnName = '';
  sortDirection = '';

  constructor(
    private siteGridService: SiteGridService,
    private readonly gridService: GridService
  ) { }

  query() {
        return new Observable(observer => {
          this.siteGridService.query().subscribe(data => {
              this.data = data;
              observer.complete();
          });
      });
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        return this.gridService.siteTitle;
    }

    get isShowFullColumn(): boolean {
      return this.gridService.filterModel.isShowFullColumn;
    }

    get isShowNoMatchesColumn(): boolean {
      return this.gridService.mainTitle === ReportTypeConstant.TOP_LOSING_LEAGUES_TEXT;
    }

    sort(sortColumnName: string) {
      if (this.sortColumnName !== sortColumnName) {
        this.sortDirection = '';
      }

      this.sortColumnName = sortColumnName;
      this.sortDirection = this.sortDirection !== 'asc' ? 'asc' : 'desc';
      this.data.Entities = _.orderBy(this.data.Entities, this.sortColumnName, this.sortDirection !== 'asc' ? 'asc' : 'desc');
    }

    showLeagueOrEvent(id: string, site: string) {
      if (this.isShowNoMatchesColumn) {
        this.gridService.eventTitle = site;
        this.gridService.generate(GridType.Event, { SiteId: Number(id), LiveIndicator: null,
          MatchId: null, BetType: null, ShowBy: 'Event'});
        this.gridService.clickedFromColumn = GridConstant.CLICK_FROM_SITE;
      } else {
        this.gridService.leagueTitle = site;
        this.gridService.generate(GridType.League, { SiteId: Number(id), LeagueId: null, LiveIndicator: null, MatchId: null, ShowBy: null});
      }
    }

    showMatches(id: string, site: string) {
      this.gridService.matchTitle = site;
      this.gridService.generate(GridType.Match, {  SiteId: Number(id), LiveIndicator: null,
        BetType: null, ShowBy: 'Match', MatchId: null });
      this.gridService.clickedFromColumn = GridConstant.CLICK_FROM_MATCHES;
    }
}

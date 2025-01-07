import { MainService } from './main.service';
import { MainItemModel } from './../common/model';
import { Component } from '@angular/core';
import { QueryModelService } from '../common/queryModel.service';
import { GridService } from '../grid/grid.service';
import { ApiConstant, ReportTypeConstant } from '../grid/grid.constant';
import { Grid, GridType } from '../grid/grid.model';
import { Observable } from 'rxjs-compat/Observable';
import { DrillDownModel } from '../main-item/main-item.model';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [
    MainService,
    QueryModelService]
})
export class MainComponent implements Grid {
  siteData: MainItemModel;
  sportData: MainItemModel;
  usernameData: MainItemModel;
  leagueData: MainItemModel;
  isShowFullColumn = false;

  constructor(
    private readonly mainService: MainService,
    private readonly gridService: GridService
  ) { }

  query() {
      return new Observable(observer => {

        this.mainService.query(this.gridService.filterModel, true, false, false, false).subscribe(data => {
          this.siteData = data;
        });

        this.mainService.query(this.gridService.filterModel, false, true, false, false).subscribe(data => {
          this.sportData = data;
        });

        this.mainService.query(this.gridService.filterModel, false, false, true, false).subscribe(data => {
          this.leagueData = data;
        });

        this.mainService.query(this.gridService.filterModel, false, false, false, true).subscribe(data => {
          this.usernameData = data;
        });

        this.isShowFullColumn = this.gridService.filterModel.isShowFullColumn;

        observer.complete();
    });
  }

  sort(sortColumnName: string) {}

  get title(): string {
    return 'Main';
  }

  showGrid(data: DrillDownModel, reportType: number) {
    let gridType = GridType.Sport;
    let model: any;
    let mainTitle = ReportTypeConstant.MARGIN_BY_SITE_TEXT;
    switch ( reportType ) {
      case ReportTypeConstant.MARGIN_BY_SITE:
          this.gridService.api = ApiConstant.MARGIN_BY_SITE;
          this.gridService.sportTitle = data.name;
          model = {
            SiteId: data.id,
            SportId: null,
            LeagueId: null,
            LiveIndicator: null,
            MatchId: null
          };
          break;
      case ReportTypeConstant.MARGIN_BY_SPORT:
          this.gridService.api = ApiConstant.MARGIN_BY_SPORT;
          gridType = GridType.Site;
          this.gridService.siteTitle = data.name;
          mainTitle = ReportTypeConstant.MARGIN_BY_SPORT_TEXT;
          model = {
            SportId: data.id,
            SiteId: null,
            LeagueId: null,
            LiveIndicator: null,
            MatchId: null
          };
          break;
      case ReportTypeConstant.TOP_WINNING_MEMBERS:
          this.gridService.api = ApiConstant.TOP_WINNING_MEMBERS;
          gridType = GridType.Sport;
          this.gridService.sportTitle = data.name;
          mainTitle = ReportTypeConstant.TOP_WINNING_MEMBERS_TEXT;
          model = {
            CustId: data.id,
            SportId: null,
            LeagueId: null,
            LiveIndicator: null,
            MatchId: null
          };
          break;
      default:
          this.gridService.api = ApiConstant.TOP_LOSING_LEAGUES;
          gridType = GridType.Site;
          this.gridService.siteTitle = data.name;
          mainTitle = ReportTypeConstant.TOP_LOSING_LEAGUES_TEXT;
          model = {
            LeagueId: data.id,
            SiteId: null,
            LiveIndicator: null,
            MatchId: null,
            BetType: null
          };
          break;
    }
    this.gridService.mainTitle = mainTitle;
    this.gridService.generate(gridType, model);
  }
}

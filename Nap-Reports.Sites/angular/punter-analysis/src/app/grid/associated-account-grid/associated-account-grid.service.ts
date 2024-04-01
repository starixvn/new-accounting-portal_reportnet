import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { NumberFormatHelper, NumberFormatType } from 'nap-components/number-format';
import { AssociatedAccountGridModel, AssociatedAccountData } from './associated-account-grid.model';
import { SearchBy, FilterModelWrapper } from '../../filter/filter.model.main';
import { GridService } from '../grid.service';
import { SportTypeGridModel } from '../sport-type-grid/sport-type-grid.model';
import { GridType } from '../grid.model';
import { BetCountGridModel } from '../bet-count-grid/bet-count-grid.model';

@Injectable()
export class AssociatedAccountGridService {
  private url = 'api/punter-analysis/associated-account';
  model: AssociatedAccountGridModel;

  constructor(
    private httpService: HttpService,
    private gridService: GridService
  ) { }

  query(): Observable<AssociatedAccountData> {
    const queryModel = this.createQueryModel();
    return this.httpService.post<AssociatedAccountData>(this.url, queryModel);
  }

  private createQueryModel(): any {
    const filterModel = this.gridService.filterModel;
    const filterModelWrapper = new FilterModelWrapper(filterModel);
    const userLeagueModel = filterModelWrapper.userLeagueModel;
    return {
      custId: this.model.custId,
      userName: this.model.custName,
      leagueId: filterModelWrapper.leagueId,
      orderBy: userLeagueModel.orderBy,
      sortBy: userLeagueModel.sortBy,
      periodType: userLeagueModel.periodType,
      fromDate: filterModelWrapper.fromDateQueryString,
      toDate: filterModelWrapper.toDateQueryString,
      listCombinationId: filterModelWrapper.userLeagueWrapper.listCombinationId,
      listLeagueGroupId: filterModelWrapper.userLeagueWrapper.listLeagueGroupCombinationId
    };
  }

  createSportTypeGridModel(custId: number, custName: string): SportTypeGridModel {
    return {
      custId: custId,
      custName: custName
    };
  }

  createBetCountGridModel(custId: number, custName: string): BetCountGridModel {
    return {
      fromGridType: GridType.UserName,
      custId: custId,
      custName: custName
    };
  }

  exportExcel() {
    const url = `${this.url}/excel`;
    const filterModel = this.gridService.filterModel;
    const filterModelWrapper = new FilterModelWrapper(filterModel);
    const queryModel = this.createQueryModel();
    queryModel.title = `Associated accounts of Punter ${this.model.custName} ${filterModelWrapper.dateFromToTitle}`;
    const filterLines = [];
    if (filterModel.searchBy === SearchBy.User) {
      filterLines.push(`Depth: ${filterModelWrapper.depthTitle} - Account name: ${filterModelWrapper.userNameTitle}`);
    } else if (filterModel.searchBy === SearchBy.League) {
      filterLines.push('User Name: All');
    }
    const topRow = filterModelWrapper.userLeagueModel.topRow;
    const orderByTitle = filterModelWrapper.userLeagueWrapper.orderByTitle;
    const sortByTitle = filterModelWrapper.userLeagueWrapper.sortByTitle;
    const topRowString = NumberFormatHelper.format(topRow, NumberFormatType.Integer);
    filterLines.push(`Top rows: ${topRowString} - Order by: ${orderByTitle}, ${sortByTitle}`);
    filterLines.push(`Sport Type: ${filterModelWrapper.selectedSportTypes}`);
    if (filterModel.searchBy === SearchBy.League) {
      filterLines.push(`League: ${filterModelWrapper.leagueTitle}`);
    }
    queryModel.filter = filterLines.join('\n');
    this.httpService.download(url, queryModel).subscribe(response => {
      const fileName = `Associated_Accounts_Of_Punter_${this.model.custName}_${filterModelWrapper.dateFromToFileName}.xlsx`;
      Helper.downloadFile(response, fileName);
    });
  }
}

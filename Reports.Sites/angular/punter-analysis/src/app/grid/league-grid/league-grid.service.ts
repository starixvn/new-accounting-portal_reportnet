import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { DateHelper } from 'nap-components/date';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { SearchBy, FilterModelWrapper } from '../../filter/filter.model.main';
import { GridService } from '../grid.service';
import { LiveStatus } from '../grid.model';
import { CommonService } from '../common.service';
import { LeagueGridModel, LeagueData } from './league-grid.model';

@Injectable()
export class LeagueGridService {
    private url = 'api/punter-analysis/league';
    model: LeagueGridModel;

    constructor(
        private httpService: HttpService,
        private gridService: GridService,
        private commonService: CommonService
    ) { }

    query(): Observable<LeagueData> {
        const queryModel = this.createQueryModel();
        return this.httpService.post<LeagueData>(this.url, queryModel);
    }

    exportExcel() {
        const url = `${this.url}/excel`;
        const filterModel = this.gridService.filterModel;
        const filterModelWrapper = new FilterModelWrapper(filterModel);
        const queryModel = this.createQueryModel();
        queryModel.title = `Punter Analysis ${filterModelWrapper.dateFromToTitle}`;
        let filterLines: string[] = [];
        filterLines.push(`User Name: ${this.model.custName}`);
        filterLines.push(`Order by: ${filterModelWrapper.userLeagueWrapper.orderByTitle}, ${filterModelWrapper.userLeagueWrapper.sortByTitle}`);
        filterLines.push(`Sport Type: ${this.model.sportName}`);

        if (this.model.leagueGroupName && this.model.leagueGroupName !== '') {
            filterLines.push(`League Group: ${this.model.leagueGroupName}`);
        }

        if (this.model.betTypeGroupName && this.model.betTypeGroupName !== '') {
            filterLines.push(`Bet Type Group: ${this.model.betTypeGroupName}`);
        }

        filterLines.push(`Bet Type: ${this.model.betTypeName} - Status: ${this.commonService.getLiveStatusTitle(this.model.liveStatusId)}`);

        if (filterModel.searchBy === SearchBy.League) {
            filterLines.push(`League: ${filterModelWrapper.leagueTitle}`);
        }
        queryModel.filter = filterLines.join('\n');
        this.httpService.download(url, queryModel).subscribe(response => {
            const fileName = `Top_Punter_League_${filterModelWrapper.searchByFileName}_${filterModelWrapper.dateFromToFileName}.xlsx`;
            Helper.downloadFile(response, fileName);
        });
    }

    private createQueryModel(): any {
        const filterModel = this.gridService.filterModel;
        const filterModelWrapper = new FilterModelWrapper(filterModel);
        return {
            periodType: filterModelWrapper.userLeagueModel.periodType,
            fromDate: filterModelWrapper.fromDateQueryString,
            toDate: filterModelWrapper.toDateQueryString,
            listCombinationId: filterModelWrapper.userLeagueWrapper.listCombinationId,
            leagueId: filterModelWrapper.leagueId,
            custId: this.model.custId,
            sportId: this.model.sportId,
            betTypeId: this.model.betTypeId,
            liveStatusId: this.model.liveStatusId,
            listLeagueGroupId: filterModelWrapper.userLeagueWrapper.listLeagueGroupCombinationId,
            leagueGroupId: this.model.leagueGroupId
        };
    }
}

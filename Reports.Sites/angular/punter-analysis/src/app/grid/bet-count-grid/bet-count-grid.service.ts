import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { SearchBy, FilterModelWrapper } from '../../filter/filter.model.main';
import { GridService } from '../grid.service';
import { GridType } from '../grid.model';
import { CommonService } from '../common.service';
import { BetCountGridModel, BetCountData } from './bet-count-grid.model';

@Injectable()
export class BetCountGridService {
    model: BetCountGridModel;
    private url = 'api/punter-analysis/bet-count';

    constructor(
        private httpService: HttpService,
        private gridService: GridService,
        private commonService: CommonService
    ) { }

    query(): Observable<BetCountData> {
        const queryModel = this.createQueryModel();
        return this.httpService.post<BetCountData>(this.url, queryModel);
    }

    exportExcel() {
        const url = `${this.url}/excel`;
        const filterModel = this.gridService.filterModel;
        const wrapper = new FilterModelWrapper(filterModel);
        const queryModel = this.createQueryModel();
        queryModel.title = `Punter Analysis - ${this.model.custName} by bet count ${wrapper.dateFromToTitle}`;
        queryModel.filter = this.generateFilter(wrapper);
        this.httpService.download(url, queryModel).subscribe(response => {
            const gridTypeName = this.commonService.getGridTypeTitle(this.model.fromGridType);
            const fileName = `Top_Punter_${gridTypeName}_Bet_Count_${wrapper.searchByFileName}_${wrapper.dateFromToFileName}.xlsx`;
            Helper.downloadFile(response, fileName);
        });
    }

    getBreadcrumbTitle(): string {
        switch (this.model.fromGridType) {
            case GridType.UserName:
                return `Punter ${this.model.custName} by bet count`;
            case GridType.SportType:
                return this.model.sportName;
            case GridType.BetType:
            case GridType.GameBetType:
                return `${this.model.betTypeName} - ${this.commonService.getLiveStatusTitle(this.model.liveStatusId)}`;
            case GridType.Game:
                return this.model.leagueGroupName;
            case GridType.BetTypeGroup:
                return this.model.betTypeGroupName;
        }
    }

    private generateFilter(wrapper: FilterModelWrapper): string {
        const filterModel = this.gridService.filterModel;
        const filterLines: string[] = [];
        switch (this.model.fromGridType) {
            case GridType.UserName:
                filterLines.push(`Sport Type: ${wrapper.selectedSportTypes}`);
                break;
            case GridType.SportType:
                filterLines.push(`Sport Type: ${this.model.sportName}`);
                break;
            case GridType.BetType:
                filterLines.push(`Sport Type: ${this.model.sportName}`);
                const liveStatusTitle = this.commonService.getLiveStatusTitle(this.model.liveStatusId);
                filterLines.push(`Bet Type: ${this.model.betTypeName} - Status: ${liveStatusTitle}`);
                break;
            case GridType.Game:
                filterLines.push(`Sport Type: E-Sports`);
                filterLines.push(`League Group: ${this.model.leagueGroupName}`);
                break;
            case GridType.BetTypeGroup:
                filterLines.push(`Sport Type: E-Sports`);
                filterLines.push(`League Group: ${this.model.leagueGroupName}`);
                filterLines.push(`Bet Type Group: ${this.model.betTypeGroupName}`);
                break;
            case GridType.GameBetType:
                filterLines.push(`Sport Type: E-Sports`);
                filterLines.push(`League Group: ${this.model.leagueGroupName}`);
                const liveStatus = this.commonService.getLiveStatusTitle(this.model.liveStatusId);
                filterLines.push(`Bet Type Group: ${this.model.betTypeGroupName}`);
                filterLines.push(`Bet Type: ${this.model.betTypeName} - Status: ${liveStatus}`);
                break;
        }
        if (filterModel.searchBy === SearchBy.League) {
            filterLines.push(`League: ${wrapper.leagueTitle}`);
        }
        return filterLines.join('\n');
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
            leagueGroupId: this.model.leagueGroupId,
            betTypeGroupId: this.model.betTypeGroupId
        };
    }
}

import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { TreeviewItem } from 'ngx-treeview';
import { Grid, LiveStatus } from '../grid.model';
import { CommonService } from '../common.service';
import { BreadcrumbFilter } from '../../breadcrumb/breadcrumb.model';
import { LeagueItem, PeriodGroupItem, LeagueData } from './league-grid.model';
import { LeagueGridService } from './league-grid.service';

@Component({
    selector: 'nap-league-grid',
    templateUrl: './league-grid.component.html',
    styleUrls: ['../grid.component.scss']
})
export class LeagueGridComponent implements Grid, BreadcrumbFilter {
    private orginalData: LeagueData;
    data: LeagueData;

    constructor(
        private service: LeagueGridService,
        private commonService: CommonService
    ) { }

    query(): Observable<any> {
        return Observable.create(observer => {
            this.service.query().subscribe(data => {
                this.orginalData = data;
                this.data = _.cloneDeep(data);
                observer.complete();
            });
        });
    }

    exportExcel() {
        this.service.exportExcel();
    }

    get hasExcel(): boolean {
        return this.hasData;
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        const model = this.service.model;
        const liveStatusTitle = this.commonService.getLiveStatusTitle(model.liveStatusId);
        return `${model.betTypeName} - ${liveStatusTitle}`;
    }

    getFilterColumns(): string[] {
        return this.data.GroupNames;
    }

    filterColumns(selectedColumns: string[]) {
        if (this.hasData) {
            this.data.GroupNames = selectedColumns;
            this.data.Entities.forEach(entity => {
                entity.PeriodGroups = [];
            });
            this.data.Total.PeriodGroups = [];
            selectedColumns.forEach(column => {
                const columnIndex = this.orginalData.GroupNames.indexOf(column);
                this.data.Entities.forEach((entity, index) => {
                    entity.PeriodGroups.push(this.orginalData.Entities[index].PeriodGroups[columnIndex]);
                });
                this.data.Total.PeriodGroups.push(this.orginalData.Total.PeriodGroups[columnIndex]);
            });
        }
    }
}

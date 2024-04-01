import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
import { FilterService } from './filter.service';
import { TimePeriod, FilterModel } from './filter.model';
import { CONFIG } from '../common/config';
import { DateHelper } from 'nap-components/date';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import * as _ from 'lodash';
import { ToastService } from 'nap-components/toast';

@Component({
    selector: 'app-filter',
    templateUrl: './filter.component.html',
    styleUrls: ['./filter.component.scss'],
    providers: [
        FilterService
    ]
})
export class FilterComponent {
    @Output() onGenerated = new EventEmitter<FilterModel>();
    timePeriodSelectItems = this.filterService.getTimeSelectItems();
    timePeriod: TimePeriod = TimePeriod.LastMonth;
    minStartDate: NgbDateStruct = { year: 2014, month: 1, day: 1 };
    maxEndDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.today);
    startDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(
        moment.utc(CONFIG.date.today)
            .subtract(1, 'month')
            .startOf('month'));
    endDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(
        moment.utc(CONFIG.date.today)
            .subtract(1, 'month')
            .endOf('month'));

    constructor(private filterService: FilterService, private toastService: ToastService) { }

    onTimePeriodChange() {
        let endDate: moment.Moment;
        let startDate: moment.Moment;
        switch (this.timePeriod) {
            case TimePeriod.Today:
                endDate = moment.utc(CONFIG.date.today);
                startDate = moment.utc(CONFIG.date.today);
                break;
            case TimePeriod.Yesterday:
                endDate = moment.utc(CONFIG.date.yesterday);
                startDate = moment.utc(CONFIG.date.yesterday);
                break;
            case TimePeriod.CurrentWeek:
                startDate = moment.utc(CONFIG.date.today).startOf('isoWeek');
                endDate = moment.utc(CONFIG.date.today);
                break;
            case TimePeriod.LastWeek:
                startDate = moment.utc(CONFIG.date.today).startOf('isoWeek').subtract(1, 'week');
                endDate = moment.utc(startDate).endOf('isoWeek');
                break;
            case TimePeriod.CurrentMonth:
                startDate = moment.utc(CONFIG.date.today).startOf('month');
                endDate = moment.utc(CONFIG.date.today);
                break;
            case TimePeriod.LastMonth:
                startDate = moment.utc(CONFIG.date.today).subtract(1, 'month').startOf('month');
                endDate = moment.utc(startDate).endOf('month');
                break;
            case TimePeriod.SinceLastMonth:
                startDate = moment.utc(CONFIG.date.today).subtract(1, 'month').startOf('month');
                endDate = moment.utc(CONFIG.date.today);
                break;
            case TimePeriod.SpecificDate:
                break;
            case TimePeriod.LastThreeMonths:
                startDate = moment.utc(CONFIG.date.today).subtract(3, 'month').startOf('month');
                endDate = moment.utc(CONFIG.date.today).subtract(1, 'month').endOf('month');
                break;
            case TimePeriod.LastSixMonths:
                startDate = moment.utc(CONFIG.date.today).subtract(6, 'month').startOf('month');
                endDate = moment.utc(CONFIG.date.today).subtract(1, 'month').endOf('month');
                break;
        }

        if (!_.isNil(startDate) && !_.isNil(endDate)) {
            this.startDate = DateHelper.convertMomentToDateStruct(startDate);
            this.endDate = DateHelper.convertMomentToDateStruct(endDate);
        }
    }

    onDateChange() {
        this.timePeriod = TimePeriod.SpecificDate;
    }

    onGenerate(username: string, reportBy: string) {
        if (!this.isValid) {
            return;
        }

        const filterModel = this.createFilterModel(username, reportBy);
        this.onGenerated.emit(filterModel);
    }

    private createFilterModel(usename: string, reportBy: string) {
        const fromDate = DateHelper.convertDateStructToMoment(this.startDate);
        const toDate = DateHelper.convertDateStructToMoment(this.endDate);
        const model = new FilterModel(fromDate, toDate, usename, reportBy);

        return model;
    }

    private get isValid(): boolean {
        const maxDate = DateHelper.convertDateStructToMoment(this.startDate).add(6, 'month');
        if (!DateHelper.convertDateStructToMoment(this.endDate).isBefore(maxDate)) {
            this.toastService.activate(`Date range selection is limited to 6 months to protect system's performance!`);
            return false;
        }

        return true;
    }
}

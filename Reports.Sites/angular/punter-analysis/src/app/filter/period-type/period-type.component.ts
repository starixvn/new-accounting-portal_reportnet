import { Component, Input, OnChanges, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import * as _ from 'lodash';
import * as moment from 'moment';
import { SelectItem } from 'nap-components/dropdown-select';
import { DateHelper } from 'nap-components/date';
import { PeriodType } from '../filter.model.main';
import { PeriodTypeEventModel } from './period-type.model';

@Component({
    selector: 'nap-period-type',
    templateUrl: './period-type.component.html',
    styleUrls: [
        '../filter.component.scss',
        'period-type.component.scss'
    ]
})
export class PeriodTypeComponent implements OnChanges {
    @Input() minDate: NgbDateStruct;
    @Input() maxDate: NgbDateStruct;
    @Output() periodTypeChange = new EventEmitter<PeriodTypeEventModel>();
    periodType = PeriodType.Daily;
    PeriodType = PeriodType;
    periodTypeOptions = this.getPeriodTypeOptions();

    dailyFromDate: NgbDateStruct;
    dailyToDate: NgbDateStruct;
    weeklyFromDate: NgbDateStruct;
    weeklyMaxFromDate: NgbDateStruct;
    weeklyToDate: NgbDateStruct;
    monthlyDate: NgbDateStruct;
    annuallyDate: NgbDateStruct;
    monthRangeFromDate: NgbDateStruct;
    monthRangeToDate: NgbDateStruct;

    private emittedModel: PeriodTypeEventModel;
    private todayNgbDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment().startOf('day'));
    constructor() {
        this.initDefaultDate(this.todayNgbDate);
    }

    ngOnChanges(changes: SimpleChanges) {
        const maxDateChange = changes['maxDate'];
        if (maxDateChange) {
            const maxDateStruct: NgbDateStruct = maxDateChange.currentValue;
            const maxDate = DateHelper.convertDateStructToMoment(maxDateStruct);
            const todayDate = DateHelper.convertDateStructToMoment(this.todayNgbDate);
            if (maxDate.isBefore(todayDate)) {
                this.initDefaultDate(maxDateStruct);
            }
        }

        const minDateChange = changes['minDate'];
        if (minDateChange) {
            this.weeklyMaxFromDate = this.getWeeklyMaxFromDate();
        }

        this.onDailyDateChange();
    }

    onTypeChange() {
        switch (this.periodType) {
            case PeriodType.Daily:
                this.onDailyDateChange();
                break;
            case PeriodType.Weekly:
                this.onWeeklyDateChange();
                break;
            case PeriodType.Monthly:
                this.onMonthlyDateChange();
                break;
            case PeriodType.MonthRange:
                this.onMonthRangeDateChange();
                break;
            case PeriodType.Annually:
                this.onAnnuallyDateChange();
                break;
        }
    }

    onDailyDateChange() {
        this.emitChange({
            periodType: this.periodType,
            fromDate: this.dailyFromDate,
            toDate: this.dailyToDate
        });
    }

    onWeeklyDateChange() {
        const weeklyFromDate = DateHelper.getFirstDayOfWeek(this.weeklyFromDate);
        const weeklyToDate = DateHelper.getFirstDayOfWeek(this.weeklyToDate);
        this.emitChange({
            periodType: this.periodType,
            fromDate: weeklyFromDate,
            toDate: weeklyToDate
        });
    }

    onWeeklyToDateChange() {
        this.weeklyMaxFromDate = this.getWeeklyMaxFromDate();
        this.onWeeklyDateChange();
    }

    onMonthlyDateChange() {
        const monthlyDate = { year: this.monthlyDate.year, month: this.monthlyDate.month, day: 1 };
        this.emitChange({
            periodType: this.periodType,
            fromDate: monthlyDate,
            toDate: monthlyDate
        });
    }

    onAnnuallyDateChange() {
        const annuallyDate = { year: this.annuallyDate.year, month: 1, day: 1 };
        this.emitChange({
            periodType: this.periodType,
            fromDate: annuallyDate,
            toDate: annuallyDate
        });
    }

    onMonthRangeDateChange() {
        const monthlyFromDate = { year: this.monthRangeFromDate.year, month: this.monthRangeFromDate.month, day: 1 };
        const monthlyToDate = { year: this.monthRangeToDate.year, month: this.monthRangeToDate.month, day: 1 };
        this.emitChange({
            periodType: this.periodType,
            fromDate: monthlyFromDate,
            toDate: monthlyToDate
        });
    }

    private emitChange(model: PeriodTypeEventModel) {
        let shouldEmit = true;
        if (this.emittedModel !== undefined) {
            shouldEmit = this.emittedModel.periodType !== model.periodType
                || !_.isEqual(this.emittedModel.fromDate, model.fromDate)
                || !_.isEqual(this.emittedModel.toDate, model.toDate);
        }

        if (shouldEmit) {
            this.emittedModel = model;
            this.periodTypeChange.emit(model);
        }
    }

    private initDefaultDate(date: NgbDateStruct) {
        this.dailyFromDate = _.clone(date);
        this.dailyToDate = _.clone(date);
        this.weeklyFromDate = _.clone(date);
        this.weeklyToDate = _.clone(date);
        this.monthlyDate = _.clone(date);
        this.annuallyDate = _.clone(date);
        this.monthRangeFromDate = _.clone(date);
        this.monthRangeToDate = _.clone(date);
    }

    private getWeeklyMaxFromDate(): NgbDateStruct {
        if (this.weeklyToDate) {
            const lastDayOfWeek = DateHelper.getLastDayOfWeek(this.weeklyToDate);
            if (this.maxDate) {
                const maxNgbDate = DateHelper.convertDateStructToMoment(this.maxDate);
                const lastNgDayOfWeek = DateHelper.convertDateStructToMoment(lastDayOfWeek);
                if (maxNgbDate.isAfter(lastNgDayOfWeek)) {
                    return DateHelper.convertMomentToDateStruct(lastNgDayOfWeek);
                } else {
                    return this.maxDate;
                }
            } else {
                return lastDayOfWeek;
            }
        }

        return this.maxDate;
    }

    private getPeriodTypeOptions(): SelectItem[] {
        return [{
            text: 'Daily',
            value: PeriodType.Daily
        }, {
            text: 'Weekly',
            value: PeriodType.Weekly
        }, {
            text: 'Monthly',
            value: PeriodType.Monthly
        }, {
            text: 'Annually',
            value: PeriodType.Annually
        }, {
            text: 'Month Range',
            value: PeriodType.MonthRange
        }];
    }
}

import { Component, Input, OnChanges, Output, EventEmitter, SimpleChanges } from '@angular/core';
import * as _ from 'lodash';
import * as moment from 'moment';
import { SelectItem } from 'nap-components/dropdown-select';
import { DateHelper, NapDateStruct } from 'nap-components/date';
import { PeriodTypeEventModel } from './period-type.model';
import { PeriodType } from '../filter.model';

@Component({
  selector: 'nap-period-type',
  templateUrl: './period-type.component.html',
  styleUrls: [
    '../filter.component.scss',
    'period-type.component.scss'
  ]
})
export class PeriodTypeComponent implements OnChanges {
  @Input() minDate: NapDateStruct;
  @Input() maxDate: NapDateStruct;
  @Output() periodTypeChange = new EventEmitter<PeriodTypeEventModel>();
  periodType = PeriodType.Daily;
  PeriodType = PeriodType;
  periodTypeOptions = this.getPeriodTypeOptions();

  dailyFromDate: NapDateStruct;
  dailyToDate: NapDateStruct;
  monthlyDate: NapDateStruct;
  monthlyEndDate: NapDateStruct;
  annuallyDate: NapDateStruct;
  annuallyEndDate: NapDateStruct;

  private emittedModel: PeriodTypeEventModel;
  private todayNgbDate: NapDateStruct = DateHelper.convertMomentToDateStruct(moment().startOf('day'));
  private maxDateStruct: NapDateStruct;
  constructor() {
    this.initDefaultDate(this.todayNgbDate);
  }

  ngOnChanges(changes: SimpleChanges) {
    const maxDateChange = changes['maxDate'];
    if (maxDateChange) {
      const maxDateStruct: NapDateStruct = maxDateChange.currentValue;
      this.maxDateStruct = maxDateStruct;
      const maxDate = DateHelper.convertDateStructToMoment(maxDateStruct);
      const todayDate = DateHelper.convertDateStructToMoment(this.todayNgbDate);
      if (maxDate.isBefore(todayDate)) {
        this.initDefaultDate(maxDateStruct);
      }
    }

    this.onDailyDateChange();
  }

  onTypeChange() {
    switch (this.periodType) {
      case PeriodType.Daily:
        this.onDailyDateChange();
        break;
      case PeriodType.Monthly:
        this.onMonthlyDateChange();
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

  onMonthlyDateChange() {
    const maxDateValue =  DateHelper.convertDateStructToMoment(this.maxDateStruct);
    const monthlyDate = { year: this.monthlyDate.year, month: this.monthlyDate.month, day: 1 };

    const startMonth = DateHelper.convertDateStructToMoment(monthlyDate);
    const endMonth = moment(startMonth).endOf('month');
    let endMonthDateStruct = DateHelper.convertMomentToDateStruct(endMonth);

    if (maxDateValue.isBefore(endMonth)) {
      endMonthDateStruct = DateHelper.convertMomentToDateStruct(maxDateValue);
    }

    const endMonthDate = {year: endMonthDateStruct.year, month: endMonthDateStruct.month, day: endMonthDateStruct.day};

    this.emitChange({
      periodType: this.periodType,
      fromDate: monthlyDate,
      toDate: endMonthDate
    });
  }

  onAnnuallyDateChange() {
    const maxDateValue =  DateHelper.convertDateStructToMoment(this.maxDateStruct);
    const annuallyDate = { year: this.annuallyDate.year, month: 1, day: 1 };

    const startYear = moment({years: this.annuallyDate.year, months: 1, date: 1});
    let endYear = moment(startYear).endOf('year');

    if (maxDateValue.isBefore(endYear)) {
      endYear = maxDateValue;
    }

    const endYearDateStruct = DateHelper.convertMomentToDateStruct(endYear);
    const endYearDate = {year: endYearDateStruct.year, month: endYearDateStruct.month, day: endYearDateStruct.day};

    this.emitChange({
      periodType: this.periodType,
      fromDate: annuallyDate,
      toDate: endYearDate
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

  private initDefaultDate(date: NapDateStruct) {
    const maxDateValue =  DateHelper.convertDateStructToMoment(date);
    const dateValue = DateHelper.convertDateStructToMoment(date);
    let endMonth = moment(dateValue).endOf('month');
    let endYear = moment(dateValue).endOf('year');

    if (maxDateValue.isBefore(endMonth)) {
      endMonth = maxDateValue;
    }

    if (maxDateValue.isBefore(endYear)) {
      endYear = maxDateValue;
    }

    const endMonthDateStruct = DateHelper.convertMomentToDateStruct(endMonth);
    const endYearDateStruct = DateHelper.convertMomentToDateStruct(endYear);

    this.dailyFromDate = _.clone(date);
    this.dailyToDate = _.clone(date);
    this.monthlyDate = _.clone(date);
    this.monthlyEndDate = _.clone(endMonthDateStruct);
    this.annuallyDate = _.clone(date);
    this.annuallyEndDate = _.clone(endYearDateStruct);
  }

  private getPeriodTypeOptions(): SelectItem[] {
    return [
      {
        text: 'Daily',
        value: PeriodType.Daily
      },
      {
        text: 'Monthly',
        value: PeriodType.Monthly
      }, {
        text: 'Annually',
        value: PeriodType.Annually
      }];
  }
}

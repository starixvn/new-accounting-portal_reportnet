import { NapDateStruct, DateHelper, DateFormat } from 'nap-components/date';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import * as moment from 'moment';
import { CONFIG } from '../common/date-config';
import { FilterModel, SelectItem } from './filter.model';
import { FilterService } from './filter.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
  providers: [
    FilterService]
})
export class FilterComponent implements OnInit {
  @Output() generated = new EventEmitter<FilterModel>();
  minStartDate: NapDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.minDate);
  maxEndDate: NapDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.today);
  startDate: NapDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  endDate: NapDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  timeRangeOptions: SelectItem[] = [
    new SelectItem('Whole Day', '-1'),
    new SelectItem('00 - 02', '2'),
    new SelectItem('00 - 04', '4'),
    new SelectItem('00 - 06', '6'),
    new SelectItem('00 - 08', '8'),
    new SelectItem('00 - 10', '10'),
    new SelectItem('00 - 12', '12'),
    new SelectItem('00 - 14', '14'),
    new SelectItem('00 - 16', '16'),
    new SelectItem('00 - 18', '18'),
    new SelectItem('00 - 20', '20'),
    new SelectItem('00 - 22', '22')
  ];
  timeRangeSelected: SelectItem = null;
  timeRangeDisabled: true;
  margin = 3.00;
  topRowOptions: SelectItem[] = [
    new SelectItem('10', '10'),
    new SelectItem('20', '20'),
    new SelectItem('50', '50'),
    new SelectItem('100', '100'),
    new SelectItem('200', '200'),
    new SelectItem('300', '300'),
    new SelectItem('400', '400'),
    new SelectItem('500', '500')
  ];
  topRowSelected: SelectItem = null;
  topByOptions: SelectItem[] = [
    new SelectItem('Margin', 'Margin'),
    new SelectItem('Turnover/ Real Turnover', 'RealTurnover'),
    new SelectItem('Win Loss/ Real Income', 'RealIncome')
  ];
  topBySelected: SelectItem = null;
  turnoverOptions: SelectItem[] = [
    new SelectItem('Normal', '0'),
    new SelectItem('No Draw', '1'),
  ];
  turnover = '0';

  isShowFullColumn = false;
  isDisableTimeRangeOptions = false;

  isExcludeParlay = true;

  constructor(private readonly filterService: FilterService) { }

  ngOnInit() {
    this.filterService.showFullColumn().subscribe(data => {
      this.isShowFullColumn = data;
    });

    this.timeRangeSelected = this.timeRangeOptions[0];
    this.topRowSelected = this.topRowOptions[2];
    this.topBySelected = this.topByOptions[0];
  }

  onDateChangeFrom() {
    let startDate: moment.Moment;
    let endDate: moment.Moment;
    let tempStartDate: moment.Moment;
    let tempEndDate: moment.Moment;

    startDate = DateHelper.convertDateStructToMoment(this.startDate);
    endDate = DateHelper.convertDateStructToMoment(this.endDate);
    tempStartDate = moment.utc(startDate);
    tempEndDate = moment.utc(endDate);
    const maxEndDate = endDate;

    if (maxEndDate.isBefore(endDate)) {
      tempEndDate = moment.utc(maxEndDate);
    }

    this.startDate = DateHelper.convertMomentToDateStruct(tempStartDate);
    this.endDate = DateHelper.convertMomentToDateStruct(tempEndDate);

    if (tempStartDate.isBefore(tempEndDate)) {
      this.timeRangeSelected = this.timeRangeOptions[0];
      this.isDisableTimeRangeOptions = true;
    } else {
      this.isDisableTimeRangeOptions = false;
    }
  }

  onDateChangeTo() {
    let startDate: moment.Moment;
    let endDate: moment.Moment;
    let tempStartDate: moment.Moment;
    let tempEndDate: moment.Moment;

    startDate = DateHelper.convertDateStructToMoment(this.startDate);
    endDate = DateHelper.convertDateStructToMoment(this.endDate);
    tempStartDate = moment.utc(startDate);
    tempEndDate = moment.utc(endDate);
    const minStartDate = endDate.subtract(2, 'day');

    if (minStartDate.isAfter(startDate)) {
      tempStartDate = moment.utc(minStartDate);
    }

    this.startDate = DateHelper.convertMomentToDateStruct(tempStartDate);
    this.endDate = DateHelper.convertMomentToDateStruct(tempEndDate);

    if (tempStartDate.isBefore(tempEndDate)) {
      this.timeRangeSelected = this.timeRangeOptions[0];
      this.isDisableTimeRangeOptions = true;
    } else {
      this.isDisableTimeRangeOptions = false;
    }
  }

  onGenerateClick() {
    const model = this.createModel();
    this.generated.emit(model);
  }

  private createModel(): FilterModel {
    const fromDate = DateHelper.convertDateStructToMoment(this.startDate);
    const toDate = DateHelper.convertDateStructToMoment(this.endDate);
    const model: FilterModel = {
      fromDate: DateHelper.formatMomentDate(fromDate, DateFormat.Date),
      toDate: DateHelper.formatMomentDate(toDate, DateFormat.Date),
      timeRange: Number(this.timeRangeSelected.value),
      margin: this.margin,
      topRows: Number(this.topRowSelected.value),
      isShowFullColumn: this.isShowFullColumn,
      topBy: this.topBySelected.value,
      excludeDrawTickets: this.turnover === '1',
      excludeParlay: this.isExcludeParlay
    };

    return model;
  }
}

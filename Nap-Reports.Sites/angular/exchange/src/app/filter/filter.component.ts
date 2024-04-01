import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import {
  TreeviewItem, TreeviewConfig, TreeviewEventParser,
  DownlineTreeviewEventParser, DownlineTreeviewItem
} from 'ngx-treeview';
import { DateHelper, DateFormat } from 'nap-components/date';
import * as moment from 'moment';
import { FilterService } from './filter.service';
import { TimePeriod, FilterModel } from './filter.model';
import { CONFIG } from '../common/config';
import * as _ from 'lodash';
import { ToastService } from 'nap-components/toast';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
  providers: [
    FilterService,
    { provide: TreeviewEventParser, useClass: DownlineTreeviewEventParser }
  ]
})
export class FilterComponent implements OnInit {
  @Output() generated = new EventEmitter<FilterModel>();
  timePeriodSelectItems = this.filterService.getTimeSelectItems();
  timePeriod: TimePeriod = TimePeriod.Yesterday;
  minStartDate: NgbDateStruct = { year: 2020, month: 1, day: 1 };
  maxEndDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.yesterday);
  startDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));
  endDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));
  licenseeTreeviewItems: TreeviewItem[] = [];
  sportTreeviewItems: TreeviewItem[] = [];
  treeviewConfig = TreeviewConfig.create({
    hasCollapseExpand: true,
    hasAllCheckBox: true,
    hasFilter: false,
    maxHeight: 500
  });
  private selectedLicenseeTreeviewItems: DownlineTreeviewItem[] = [];
  private selectedSportTreeviewItems: DownlineTreeviewItem[]  = [];

  constructor(private filterService: FilterService, private toastService: ToastService) { }

  ngOnInit() {
    this.getLicensee();
    this.getSport();
  }

  onGenerate() {
    if (!this.isValid) {
      return;
    }

    const filterModel = this.createFilterModel();
    this.generated.emit(filterModel);
  }

  onTimePeriodChange() {
    let endDate: moment.Moment;
    let startDate: moment.Moment;
    switch (this.timePeriod) {
      case TimePeriod.Yesterday:
        endDate = moment.utc(CONFIG.date.yesterday);
        startDate = moment.utc(CONFIG.date.yesterday);
        break;
      case TimePeriod.CurrentWeek:
        startDate = moment.utc(CONFIG.date.today).startOf('isoWeek');
        endDate = moment.utc(CONFIG.date.today);

        if (moment(startDate).isBefore(endDate)) {
          endDate = moment.utc(CONFIG.date.yesterday);
        }
        break;
      case TimePeriod.LastWeek:
        startDate = moment.utc(CONFIG.date.today).startOf('isoWeek').subtract(1, 'week');
        endDate = moment.utc(startDate).endOf('isoWeek');
        break;
      case TimePeriod.CurrentMonth:
        startDate = moment.utc(CONFIG.date.today).startOf('month');
        endDate = moment.utc(CONFIG.date.today);

        if (moment(startDate).isBefore(endDate)) {
          endDate = moment.utc(CONFIG.date.yesterday);
        }
        break;
      case TimePeriod.LastMonth:
        startDate = moment.utc(CONFIG.date.today).subtract(1, 'month').startOf('month');
        endDate = moment.utc(startDate).endOf('month');
        break;
      case TimePeriod.SinceLastMonth:
        startDate = moment.utc(CONFIG.date.today).subtract(1, 'month').startOf('month');
        endDate = moment.utc(CONFIG.date.today);

        if (moment(startDate).isBefore(endDate)) {
          endDate = moment.utc(CONFIG.date.yesterday);
        }
        break;
      case TimePeriod.SpecificDate:
        break;
      case TimePeriod.LastThreeMonths:
        startDate = moment.utc(CONFIG.date.today).subtract(3, 'month').startOf('month');
        endDate = moment.utc(CONFIG.date.today).subtract(1, 'month').endOf('month');
        break;
      case TimePeriod.SinceLastYear:
        startDate = moment.utc(CONFIG.date.today).subtract(1, 'year');
        endDate = moment.utc(CONFIG.date.yesterday);
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

  onLicenseeSelectedChange(downlineTreeviewItems: DownlineTreeviewItem[]) {
    this.selectedLicenseeTreeviewItems = downlineTreeviewItems;
  }

  onSportSelectedChange(downlineTreeviewItems: DownlineTreeviewItem[]) {
    this.selectedSportTreeviewItems = downlineTreeviewItems;
  }

  getLicensee() {
    this.filterService.getLicensee().subscribe(licenseeTreeviewItems => this.licenseeTreeviewItems = licenseeTreeviewItems);
  }

  getSport() {
    this.filterService.getSport().subscribe(sportTreeViewItems => this.sportTreeviewItems = sportTreeViewItems);
  }

  private createFilterModel() {
    const fromDate = DateHelper.convertDateStructToMoment(this.startDate);
    const toDate = DateHelper.convertDateStructToMoment(this.endDate);
    const model: FilterModel = {
       fromDate:  DateHelper.formatMomentDate(fromDate, DateFormat.Date),
       toDate:  DateHelper.formatMomentDate(toDate, DateFormat.Date),
       licenseeName: this.selectedLicenseeTreeviewItems.map(licenseeItem => licenseeItem.item.text).join(','),
       eventType: this.selectedSportTreeviewItems.map(sportItem => sportItem.item.text).join(','),
       drillLevel: 0
    };

    return model;
  }

  private get isValid(): boolean {
    const maxDate = DateHelper.convertDateStructToMoment(this.startDate).add(1, 'year');
    if (!DateHelper.convertDateStructToMoment(this.endDate).isBefore(maxDate)) {
        this.toastService.activate(`Date range selection is limited to 1 year to protect system's performance!`);
        return false;
    }

    return true;
  }
}

import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DateHelper, DateFormat } from 'nap-components/date';
import * as moment from 'moment';
import { FilterModel, Currency, Site, Odds } from './filter.model';
import { CONFIG } from '../common/config';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { FilterService } from './filter.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
  providers: [
    FilterService
  ]
})
export class FilterComponent implements OnInit {
  @Output() generated = new EventEmitter<FilterModel>();
  minStartDate: NgbDateStruct = { year: 2017, month: 1, day: 1 };
  maxEndDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.yesterday);
  startDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));
  endDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));
  currencies: Currency[];
  sites: Site[];
  odds: Odds[];

  constructor(private filterService: FilterService) { }

  ngOnInit() {
    this.getCurrency();
    this.getSite();
    this.getOdds();
  }

  onGenerate(siteType: number, currencyId: number, oddsType: number) {
    const filterModel = this.createFilterModel(siteType, currencyId, oddsType);
    this.generated.emit(filterModel);
  }

  onDateChangeFrom() {
    let startDate: moment.Moment;
    let endDate: moment.Moment;
    let tempStartDate: moment.Moment;
    let tempEndDate: moment.Moment;

    startDate = DateHelper.convertDateStructToMoment(this.startDate);
    endDate =  DateHelper.convertDateStructToMoment(this.endDate);
    tempStartDate = moment.utc(startDate);
    tempEndDate =  moment.utc(endDate);
    const maxEndDate = startDate.add(3, 'month');

    if (maxEndDate.isBefore(endDate)) {
      tempEndDate = moment.utc(maxEndDate);
    }

    this.startDate = DateHelper.convertMomentToDateStruct(tempStartDate);
    this.endDate = DateHelper.convertMomentToDateStruct(tempEndDate);
  }

  onDateChangeTo() {
    let startDate: moment.Moment;
    let endDate: moment.Moment;
    let tempStartDate: moment.Moment;
    let tempEndDate: moment.Moment;

    startDate = DateHelper.convertDateStructToMoment(this.startDate);
    endDate =  DateHelper.convertDateStructToMoment(this.endDate);
    tempStartDate = moment.utc(startDate);
    tempEndDate = moment.utc(endDate);
    const minStartDate = endDate.subtract(3, 'month');

    if (minStartDate.isAfter(startDate)) {
      tempStartDate = moment.utc(minStartDate);
    }

    this.startDate = DateHelper.convertMomentToDateStruct(tempStartDate);
    this.endDate = DateHelper.convertMomentToDateStruct(tempEndDate);
  }

  private getOdds() {
    this.filterService.getOdds().subscribe(odds => this.odds = odds);
  }

  private getCurrency() {
    this.filterService.getCurrency().subscribe(currencies => this.currencies = currencies);
  }

  private getSite() {
    this.filterService.getSite().subscribe(sites => this.sites = sites);
  }

  private createFilterModel(siteType: number, currencyId: number, oddsType: number) {
    const fromDate = DateHelper.convertDateStructToMoment(this.startDate);
    const toDate = DateHelper.convertDateStructToMoment(this.endDate);
    const model: FilterModel = {
       fromDate:  DateHelper.formatMomentDate(fromDate, DateFormat.Date),
       toDate:  DateHelper.formatMomentDate(toDate, DateFormat.Date),
       siteType: siteType,
       currencyId: currencyId,
       oddsType: oddsType
    };

    return model;
  }
}

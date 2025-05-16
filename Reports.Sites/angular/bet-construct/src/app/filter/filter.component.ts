import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FilterModel } from './filter.model';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { DateHelper } from 'nap-components/date'
import * as moment from "moment";
import { CONFIG } from '../common/date-config';
import { SelectItem } from 'nap-components/dropdown-select';
import { FilterService } from './filter.service';
import { DateType, TurnoverType } from '../common/model';
import { ToastService } from 'nap-components/toast';
import { CommonConstants } from '../common/constants';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {
  @Output() generated = new EventEmitter<FilterModel>();

  minStartDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today).subtract(1, 'month'));;
  maxEndDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  startDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  endDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  turnoverOptions: SelectItem[] = [];
  turnover = TurnoverType.Normal;
  ticketStatusSelectItems = this.filterService.getStatusSelectItems();
  gmtSelectItems = this.filterService.getGmtSelectItems();
  gmt = 0;
  ticketStatus: number | undefined;
  transactionDate: boolean = false;
  dateOptions: SelectItem[] = [];
  dateType = DateType.WinlossDate;

  constructor(private readonly filterService: FilterService,
    private readonly toastService: ToastService
  ) { }

  ngOnInit(): void {
    this.turnoverOptions = this.filterService.getTurnoverOptions();
    this.dateOptions = this.filterService.getDateTypeSelectItems();
  }

  onGenerateClick() {
    if (this.isValid()) {
      const model = this.createModel();
      this.generated.emit(model);
    }
  }

  private createModel(): FilterModel {
    const fromDate = DateHelper.convertDateStructToMoment(this.startDate);
    const toDate = DateHelper.convertDateStructToMoment(this.endDate);
    const gmtOffset = this.dateType === DateType.WinlossDate ? -4 * 60 : this.gmt * 60;
    const model: FilterModel = {
      fromDate: fromDate.utcOffset(gmtOffset, true).format(CommonConstants.DATE_GMT),
      toDate: toDate.utcOffset(gmtOffset, true).format(CommonConstants.DATE_GMT),
      sportId: undefined,
      sportName: '',
      noDraw: this.turnover === TurnoverType.NoDraw,
      breadCrumb: [],
      liveStatus: this.ticketStatus,
      filterType: this.dateType
    };
    return model;
  }

  private isValid(): boolean {
    const maxDate = DateHelper.convertDateStructToMoment(this.startDate).add(1, 'month');
    if (!DateHelper.convertDateStructToMoment(this.endDate).isBefore(maxDate)) {
        this.toastService.activate(`Date range should not exceed 1 month.`);
        return false;
    }

    return true;
  }

}

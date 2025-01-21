import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FilterModel, PromotionType } from './filter.model';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { CONFIG } from '../common/date-config';
import { DateFormat, DateHelper } from 'nap-components/date'
import { ToastService } from 'nap-components/toast';
import { TreeviewConfig } from 'ngx-treeview';
import { FilterService } from './filter.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {
  @Output() generated = new EventEmitter<FilterModel>();

  minStartDate: NgbDateStruct = { year: 2024, month: 1, day: 1 };
  maxEndDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));
  startDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));
  endDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));
  promotionTypes: PromotionType[] = [];
  promotionTypeTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedPromotionTypes: number[] = [];

  constructor(private readonly toastService: ToastService,
    private readonly filterService: FilterService
  ) { }

  ngOnInit(): void {
    this.filterService.getPromotionTypes().subscribe(promotionTypes => this.promotionTypes = promotionTypes);
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
    const model: FilterModel = {
      fromDate: DateHelper.formatMomentDate(fromDate, DateFormat.Date),
      toDate: DateHelper.formatMomentDate(toDate, DateFormat.Date),
      betTypeIds: this.selectedPromotionTypes.join(',')
    };
    return model;
  }

  private isValid(): boolean {
    const maxDate = DateHelper.convertDateStructToMoment(this.startDate).add(1, 'year');
    if (!DateHelper.convertDateStructToMoment(this.endDate).isBefore(maxDate)) {
        this.toastService.activate(`Date range should not exceed 1 year.`);
        return false;
    }

    if (this.selectedPromotionTypes.join(',').length === 0) {
      this.toastService.activate(`Please select at least one Promotion Type!`);
      return false;
    } 

    return true;
  }

}

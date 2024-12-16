import { Injectable } from '@angular/core';
import { FilterModel } from './../filter/filter.model';
import { DateFormat, DateHelper } from 'nap-components/date';

@Injectable()
export class BreadcrumbService {
    createBreadcrumb(filterModel: FilterModel) {
        return `Exchange WinLoss From ${filterModel.fromDate} To ${filterModel.toDate}`;
    }
}

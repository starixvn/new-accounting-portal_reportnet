import { Injectable } from '@angular/core';
import { FilterModel } from './../filter/filter.model';
import { DateFormat, DateHelper } from 'nap-components/date';

@Injectable()
export class BreadcrumbService {
    createBreadcrumb(filterModel: FilterModel) {
        const fromDate = DateHelper.formatMomentDate(filterModel.fromDate, DateFormat.Date);
        const toDate = DateHelper.formatMomentDate(filterModel.toDate, DateFormat.Date);

        return `${filterModel.reportBy} report From ${fromDate} To ${toDate}`;
    }
}

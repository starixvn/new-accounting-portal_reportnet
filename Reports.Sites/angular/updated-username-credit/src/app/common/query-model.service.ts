import { Injectable } from '@angular/core';
import { FilterModel } from './../filter/filter.model';
import { DateHelper, DateFormat } from 'nap-components/date';
import { QueryModel } from './query.model';

@Injectable()
export class QueryModelService {
    public createQueryModel(filterModel: FilterModel): QueryModel {
        const fromDate =  DateHelper.formatMomentDate(filterModel.fromDate, DateFormat.Date);
        const toDate =  DateHelper.formatMomentDate(filterModel.toDate, DateFormat.Date);
        const model = new QueryModel(fromDate, toDate, filterModel.userName);

        return model;
    }
}

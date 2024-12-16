import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs/Observable';
import { UpdatedCreditModel } from './updated-credit.model';
import { FilterModel } from './../filter/filter.model';
import { QueryModelService } from './../common/query-model.service';
import { DateHelper, DateFormat } from 'nap-components/date';
import { Helper } from 'nap-components/helper';

@Injectable()
export class UpdatedCreditService {
    private url = 'api/updated-credit';

    constructor(private httpService: HttpService, private queryModelService: QueryModelService) { }

    query(filterModel: FilterModel): Observable<UpdatedCreditModel[]> {
        const url = `${this.url}/query`;
        const model = this.queryModelService.createQueryModel(filterModel);
        return this.httpService.post<UpdatedCreditModel[]>(url, model);
    }

    exportExcel(filterModel: FilterModel) {
        const url = `${this.url}/excel`;
        const fromDate =  DateHelper.formatMomentDate(filterModel.fromDate, DateFormat.Date).replace(',', '').replace(/ /g, '_');
        const toDate =  DateHelper.formatMomentDate(filterModel.toDate, DateFormat.Date).replace(',', '').replace(/ /g, '_');
        const queryModel = this.queryModelService.createQueryModel(filterModel);
        this.httpService.download(url, queryModel).subscribe(response => {
            const fileName = `Updated_Credit_report_From_${fromDate}_To_${toDate}.xlsx`;
            Helper.downloadFile(response, fileName);
        });
    }
}

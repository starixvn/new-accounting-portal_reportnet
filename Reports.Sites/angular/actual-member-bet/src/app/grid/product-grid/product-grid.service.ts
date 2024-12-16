import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { ProductData } from './product-grid.model';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { Observable } from 'rxjs/Observable';
import { Helper } from 'nap-components/helper';

@Injectable()
export class ProductGridService {
    private url = 'api/actual-member-bet/product';
    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    query(): Observable<ProductData> {
        const queryModel = this.gridService.filterModel;
        return this.httpService.post<ProductData>(this.url, queryModel);
    }

    exportExcel() {
        const url = `${this.url}/excel`;
        const excelQueryModel = this.gridService.filterModel;
        const fromDate = moment(excelQueryModel.fromDate).format('YYYY_MM_DD');
        const toDate = moment(excelQueryModel.toDate).format('YYYY_MM_DD');
        this.httpService.download(url, this.gridService.filterModel).subscribe(response => {
            const fileName = `Actual_Member_Bet_Report_From_${fromDate}_To_${toDate}.xlsx`;
            Helper.downloadFile(response, fileName);
        });
    }
}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import * as moment from 'moment';
import { GridService } from '../grid.service';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { BettypeData } from './bettype-grid.model';

@Injectable()
export class BettypeGridService {
    private url = 'api/actual-member-bet/bettype';

    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    query(): Observable<BettypeData> {
        const queryModel = this.createQueryModel();
        return this.httpService.post<BettypeData>(this.url, queryModel);
    }

    exportExcel() {
        const url = `${this.url}/excel`;
        const excelQueryModel = this.createExcelQueryModel();
        const fromDate = moment(excelQueryModel.fromDate).format('YYYY_MM_DD');
        const toDate = moment(excelQueryModel.toDate).format('YYYY_MM_DD');
        this.httpService.download(url, excelQueryModel).subscribe(response => {
            const fileName = `Actual_Member_Bet_Report_From_${fromDate}_To_${toDate}.xlsx`;
            Helper.downloadFile(response, fileName);
        });
    }

    private createQueryModel(): any {
        const filterModel = this.gridService.filterModel;
        const keyId = this.gridService.keyId;
        return {
            fromDate: filterModel.fromDate,
            toDate: filterModel.toDate,
            currencyId: filterModel.currencyId,
            oddsType: filterModel.oddsType,
            siteType: filterModel.siteType,
            keyId: keyId
        };
    }

    private createExcelQueryModel(): any {
        const filterModel = this.gridService.filterModel;
        const keyId = this.gridService.keyId;
        const productName = this.gridService.productName;
        return {
            fromDate: filterModel.fromDate,
            toDate: filterModel.toDate,
            currencyId: filterModel.currencyId,
            oddsType: filterModel.oddsType,
            siteType: filterModel.siteType,
            keyId: keyId,
            productName: productName
        };
    }
}

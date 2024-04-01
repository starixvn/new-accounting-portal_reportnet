import { QueryModel, WinLossModel, VirtualRateModel } from './../common/model';
import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { FilterModel } from '../filter/filter.model';
import { Observable } from 'rxjs';
import { QueryModelService } from '../common/queryModel.service';
import { Helper } from 'nap-components/helper';


@Injectable()
export class SummaryGridService {
  constructor(
    private readonly httpService: HttpService,
    private readonly queryModelService: QueryModelService
  ) { }

  query(filterModel: FilterModel): Observable<WinLossModel> {
    const url = 'api/sma-win-loss/summary';
    const queryModel = this.createQueryModel(filterModel);
    return this.httpService.post<WinLossModel>(url, queryModel);
  }



  queryVirtualRate(custId: number): Observable<VirtualRateModel[]> {
    const url = 'api/sma-win-loss/virtual-rate';
    return this.httpService.post<VirtualRateModel[]>(url, { custId });
  }

  exportExcel(title: string, filterModel: FilterModel) {
    const url = 'api/sma-win-loss/summary/excel';
    const excelQueryModel = this.createExcelQueryModel(title, filterModel);
    this.httpService.download(url, excelQueryModel).subscribe(response => {
      const fileName = 'SMA_Win_Loss.xlsx';
      Helper.downloadFile(response, fileName);
    });
  }

  private createExcelQueryModel(title: string, filterModel: FilterModel): QueryModel {
    return this.queryModelService.createExcelQueryModel(title, filterModel);
  }

  private createQueryModel(filterModel: FilterModel): QueryModel {
    return this.queryModelService.createQueryModel(filterModel);
  }

}

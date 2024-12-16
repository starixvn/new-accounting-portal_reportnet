import { MainItemModel, QueryModel } from './../common/model';
import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { FilterModel } from '../filter/filter.model';
import { Observable } from 'rxjs';
import { QueryModelService } from '../common/queryModel.service';

@Injectable()
export class MainService {
  private readonly url = 'api/sb-performance/main';

  constructor(
    private readonly httpService: HttpService,
    private readonly queryModelService: QueryModelService
  ) { }

  query(
    filterModel: FilterModel,
    isShowSite: boolean,
    isShowSport: boolean,
    isShowLeague: boolean,
    isShowCust: boolean
  ): Observable<MainItemModel> {
    const queryModel = this.createQueryModel(filterModel, isShowSite, isShowSport, isShowLeague, isShowCust);
    return this.httpService.post<MainItemModel>(this.url, queryModel);
  }

  private createQueryModel(
    filterModel: FilterModel,
    isShowSite: boolean,
    isShowSport: boolean,
    isShowLeague: boolean,
    isShowCust: boolean): QueryModel {
    return this.queryModelService.createQueryModel(filterModel, isShowSite, isShowSport, isShowLeague, isShowCust);
  }
}

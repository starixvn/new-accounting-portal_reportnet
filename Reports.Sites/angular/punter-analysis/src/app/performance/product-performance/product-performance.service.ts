import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { FilterModel } from '../../filter/filter.model';
import { FilterModelWrapper } from '../../filter/filter.model.main';
import { PerformanceService } from '../performance.service';
import { SportsbookStatistic } from './product-performance.model';

@Injectable()
export class ProductPerformanceService {
    private url = 'api/punter-analysis/performance';

    constructor(
        private httpService: HttpService,
        private performanceService: PerformanceService
    ) { }

    query(): Observable<SportsbookStatistic> {
        const url = `${this.url}/sportsbook`;
        const queryModel = this.createQueryModel();
        return this.httpService.post<SportsbookStatistic>(url, queryModel);
    }

    private createQueryModel(): any {
        const filterModel = this.performanceService.filterModel;
        const filterModelWrapper = new FilterModelWrapper(filterModel);
        return {
            fromDate: filterModelWrapper.fromDateQueryString,
            toDate: filterModelWrapper.toDateQueryString,
            depth: filterModel.performance.depth.role,
            userName: filterModel.performance.userName
        };
    }
}

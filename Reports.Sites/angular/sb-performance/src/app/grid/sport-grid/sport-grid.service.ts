import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { SportDataModel } from './sport-grid.model';

@Injectable({
  providedIn: 'root'
})
export class SportGridService {
    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    query(): Observable<SportDataModel> {
        const queryModel = this.gridService.queryModel;
        return this.httpService.post<SportDataModel>(this.gridService.api, queryModel);
    }
}

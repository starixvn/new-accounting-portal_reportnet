import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { LeagueDataModel } from './league-grid.model';

@Injectable({
  providedIn: 'root'
})
export class LeagueGridService {
    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    query(): Observable<LeagueDataModel> {
        const queryModel = this.gridService.queryModel;
        return this.httpService.post<LeagueDataModel>(this.gridService.api, queryModel);
    }

}

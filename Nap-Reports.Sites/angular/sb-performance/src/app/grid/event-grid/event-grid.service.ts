import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs-compat/Observable';
import { GridService } from '../grid.service';
import { EventDataModel } from './event-grid.model';

@Injectable({
  providedIn: 'root'
})
export class EventGridService {

    constructor(
        private httpService: HttpService,
        private gridService: GridService
    ) { }

    query(): Observable<EventDataModel> {
        const queryModel = this.gridService.queryModel;
        return this.httpService.post<EventDataModel>(this.gridService.api, queryModel);
    }

}

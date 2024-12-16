import { TestBed, inject } from '@angular/core/testing';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { AssociatedAccountGridService } from './associated-account-grid.service';
import { fakeUserFilterModel } from '../../filter/filter.model.spec';
import { AppModule } from '../../app.module';

describe('AssociatedAccountGridService', () => {

    const expectedQueryModel = {
        custId: 1,
        userName: 'Customer 1',
        leagueId: '',
        orderBy: 1,
        sortBy: 1,
        periodType: 1,
        fromDate: '2016-01-01T00:00:00',
        toDate: '2016-02-02T00:00:00',
        listCombinationId: '1,2',
        listLeagueGroupId: '1,2'
    };
    const expectedExcelModel = {
        custId: 1,
        userName: 'Customer 1',
        title: 'Associated Account of Punter Customer 1 from Jan 01, 2016 to Feb 02, 2016 (Daily)',
        filter: `User Name: Customer 1\nTop rows: 9,999 - Order by: WinLoss, Ascending\nSport Type: SportType 1, SportType 2`
    };

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        });
    });

    beforeEach(inject([GridService], (gridService: GridService) => {
        gridService.filterModel = fakeUserFilterModel;
    }));

    beforeEach(inject([AssociatedAccountGridService], (service: AssociatedAccountGridService) => {
        service.model = {
            custId: 1,
            custName: 'Customer 1'
        };
    }));

    it('query', inject([HttpService, AssociatedAccountGridService], (httpService: HttpService, service: AssociatedAccountGridService) => {
        const spy = spyOn(httpService, 'post').and.returnValue(Observable.of(null));

        service.query();
        const mostRecentCall = spy.calls.mostRecent();
        const args = mostRecentCall.args;
        expect(args[0]).toBe('api/punter-analysis/associated-account');
        expect(args[1]).toEqual(expectedQueryModel);
    }));
});

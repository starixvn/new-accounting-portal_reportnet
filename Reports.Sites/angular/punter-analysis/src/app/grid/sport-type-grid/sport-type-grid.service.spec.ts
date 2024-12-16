import { TestBed, inject } from '@angular/core/testing';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { Helper } from 'nap-components/helper';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { SportTypeGridService } from './sport-type-grid.service';
import { SportTypeGridModel } from './sport-type-grid.model';
import { fakeUserFilterModel, fakeLeagueFilterModel } from '../../filter/filter.model.spec';
import { FilterModel } from '../../filter/filter.model';
import { AppModule } from '../../app.module';


describe('SportTypeGridService', () => {

    const fakeModel: SportTypeGridModel = {
        custId: 1,
        custName: 'Customer 1'
    };

    const expectedQueryModel = {
        user: {
            periodType: 1,
            orderBy: 1,
            sortBy: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '',
            custId: 1,
            listLeagueGroupId: '1,2'
        },
        league: {
            periodType: 1,
            orderBy: 1,
            sortBy: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '1',
            custId: 1,
            listLeagueGroupId: '1,2'
        }
    };
    const expectedExcelModel = {
        user: _.extend({}, expectedQueryModel.user, {
            title: 'Punter Analysis from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            filter: 'User Name: Customer 1\nOrder by: WinLoss, Ascending\nSport Type: SportType 1, SportType 2'
        }),
        league: _.extend({}, expectedQueryModel.league, {
            title: 'Punter Analysis from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            filter: 'User Name: Customer 1\nOrder by: WinLoss, Ascending\nSport Type: SportType 1\nLeague: League 1'
        })
    };

    const expectedExcelFileName = {
        user: 'Top_Punter_Sporttype_Search_by_User_from_Jan_01_2016_to_Feb_02_2016.xlsx',
        league: 'Top_Punter_Sporttype_Search_by_League_from_Jan_01_2016_to_Feb_02_2016.xlsx'
    };

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        });
    });

    beforeEach(inject([SportTypeGridService], (service: SportTypeGridService) => {
        service.model = fakeModel;
    }));

    it('query', inject([HttpService, GridService, SportTypeGridService],
        (httpService: HttpService, gridService: GridService, service: SportTypeGridService) => {
            const spy = spyOn(httpService, 'post').and.returnValue(Observable.of(null));

            function expectModel(fakeFilterModel: FilterModel, expectedModel: any) {
                gridService.filterModel = fakeFilterModel;
                service.query();
                const mostRecentCall = spy.calls.mostRecent();
                const args = mostRecentCall.args;
                expect(args[0]).toBe('api/punter-analysis/sport-type');
                expect(args[1]).toEqual(expectedModel);
            }

            expectModel(fakeUserFilterModel, expectedQueryModel.user);
            expectModel(fakeLeagueFilterModel, expectedQueryModel.league);
        }));

    it('exportExcel', inject([HttpService, GridService, SportTypeGridService],
        (httpService: HttpService, gridService: GridService, service: SportTypeGridService) => {
            const downloadSpy = spyOn(httpService, 'download').and.returnValue(Observable.of(null));
            const saveSpy = spyOn(Helper, 'downloadFile').and.callFake(() => { });

            function expectModel(fakeFilterModel: FilterModel, expectedExcel: any, expectedFileName: string) {
                gridService.filterModel = fakeFilterModel;
                service.exportExcel();
                let mostRecentCall = downloadSpy.calls.mostRecent();
                let args = mostRecentCall.args;

                expect(args[0]).toBe('api/punter-analysis/sport-type/excel');
                expect(args[1]).toEqual(expectedExcel);

                mostRecentCall = saveSpy.calls.mostRecent();
                args = mostRecentCall.args;
                expect(args[0]).toBe(null);
                expect(args[1]).toBe(expectedFileName);
            }

            expectModel(fakeUserFilterModel, expectedExcelModel.user, expectedExcelFileName.user);
            expectModel(fakeLeagueFilterModel, expectedExcelModel.league, expectedExcelFileName.league);
        }));
});

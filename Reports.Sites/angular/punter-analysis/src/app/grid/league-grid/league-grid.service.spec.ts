import { TestBed, inject } from '@angular/core/testing';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { Helper } from 'nap-components/helper';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { LiveStatus } from '../grid.model';
import { CommonService } from '../common.service';
import { LeagueGridService } from './league-grid.service';
import { LeagueGridModel } from './league-grid.model';
import { FilterModel } from '../../filter/filter.model';
import { fakeUserFilterModel, fakeLeagueFilterModel } from '../../filter/filter.model.spec';
import { AppModule } from '../../app.module';


describe('LeagueGridService', () => {

    const fakeModel: LeagueGridModel = {
        custId: 1,
        custName: 'Customer 1',
        betTypeId: 1,
        betTypeName: 'Bet Type 1',
        liveStatusId: 2,
        sportId: 1,
        sportName: 'Sport 1'
    };

    const expectedQueryModel = {
        user: {
            periodType: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '',
            custId: 1,
            sportId: 1,
            betTypeId: 1,
            liveStatusId: 2,
            listLeagueGroupId: '1,2',
            leagueGroupId: undefined
        },
        league: {
            periodType: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '1',
            custId: 1,
            sportId: 1,
            betTypeId: 1,
            liveStatusId: 2,
            listLeagueGroupId: '1,2',
            leagueGroupId: undefined
        }
    };

    const expectedExcelModel = {
        user: _.extend({}, expectedQueryModel.user, {
            title: `Punter Analysis from Jan 01, 2016 to Feb 02, 2016 (Daily)`,
            filter: `User Name: Customer 1\nOrder by: WinLoss, Ascending\nSport Type: Sport 1\nBet Type: Bet Type 1 - Status: Non-Live`
        }),
        league: _.extend({}, expectedQueryModel.league, {
            title: 'Punter Analysis from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            // tslint:disable-next-line:max-line-length
            filter: `User Name: Customer 1\nOrder by: WinLoss, Ascending\nSport Type: Sport 1\nBet Type: Bet Type 1 - Status: Non-Live\nLeague: League 1`
        })
    };

    const expectedExcelFileName = {
        user: 'Top_Punter_League_Search_by_User_from_Jan_01_2016_to_Feb_02_2016.xlsx',
        league: 'Top_Punter_League_Search_by_League_from_Jan_01_2016_to_Feb_02_2016.xlsx'
    };

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        });
    });

    beforeEach(inject([LeagueGridService], (service: LeagueGridService) => {
        service.model = _.clone(fakeModel);
    }));

    it('getLiveStatusTitle should return "Live" if liveStatusId is LiveStatus.Live',
        inject([CommonService, LeagueGridService], (commonService: CommonService, service: LeagueGridService) => {
            const liveStatusId = LiveStatus.Live;
            service.model.liveStatusId = liveStatusId;
            const title = commonService.getLiveStatusTitle(liveStatusId);
            expect(title).toEqual('Live');
        }));

    it('getLiveStatusTitle should return "Non-Live" if liveStatusId is LiveStatus.NonLive',
        inject([CommonService, LeagueGridService], (commonService: CommonService, service: LeagueGridService) => {
            const liveStatusId = LiveStatus.NonLive;
            const title = commonService.getLiveStatusTitle(liveStatusId);
            expect(title).toEqual('Non-Live');
        }));


    it('getLiveStatusTitle should return "Non-Live & Live" if liveStatusId is not LiveStatus.Live or LiveStatus.NonLive',
        inject([CommonService, LeagueGridService], (commonService: CommonService, service: LeagueGridService) => {
            const liveStatusId = undefined;
            service.model.liveStatusId = liveStatusId;
            const title = commonService.getLiveStatusTitle(liveStatusId);
            expect(title).toEqual('Non-Live & Live');
        }));

    it('query', inject([HttpService, GridService, LeagueGridService],
        (httpService: HttpService, gridService: GridService, service: LeagueGridService) => {
            const spy = spyOn(httpService, 'post').and.returnValue(Observable.of(null));

            function expectModel(fakeFilterModel: FilterModel, expectedModel: any) {
                gridService.filterModel = fakeFilterModel;
                service.query();
                const mostRecentCall = spy.calls.mostRecent();
                const args = mostRecentCall.args;
                expect(args[0]).toBe('api/punter-analysis/league');
                expect(args[1]).toEqual(expectedModel);
            }

            expectModel(fakeUserFilterModel, expectedQueryModel.user);
            expectModel(fakeLeagueFilterModel, expectedQueryModel.league);
        }));

    it('exportExcel', inject([HttpService, GridService, LeagueGridService],
        (httpService: HttpService, gridService: GridService, service: LeagueGridService) => {
            const downloadSpy = spyOn(httpService, 'download').and.returnValue(Observable.of(null));
            const saveSpy = spyOn(Helper, 'downloadFile').and.callFake(() => { });

            function expectModel(fakeFilterModel: FilterModel, expectedExcel: any, expectedFileName: string) {
                gridService.filterModel = fakeFilterModel;
                service.exportExcel();
                let mostRecentCall = downloadSpy.calls.mostRecent();
                let args = mostRecentCall.args;

                expect(args[0]).toBe('api/punter-analysis/league/excel');
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

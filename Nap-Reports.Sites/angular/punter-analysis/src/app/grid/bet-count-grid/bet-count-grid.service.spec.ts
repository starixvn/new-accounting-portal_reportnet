import { TestBed, inject } from '@angular/core/testing';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { Helper } from 'nap-components/helper';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { GridType, LiveStatus } from '../grid.model';
import { BetCountGridService } from './bet-count-grid.service';
import { BetCountGridModel } from './bet-count-grid.model';
import { fakeUserFilterModel, fakeLeagueFilterModel } from '../../filter/filter.model.spec';
import { FilterModel } from '../../filter/filter.model';
import { AppModule } from '../../app.module';

describe('BetCountGridService', () => {

    const fakeModel = {
        main: {
            fromGridType: GridType.UserName,
            custId: 1,
            custName: 'Customer 1'
        },
        sportType: {
            fromGridType: GridType.SportType,
            custId: 1,
            custName: 'Customer 1',
            sportId: 1,
            sportName: 'SportType 1',
        },
        betType: {
            fromGridType: GridType.BetType,
            custId: 1,
            custName: 'Customer 1',
            sportId: 1,
            sportName: 'SportType 1',
            betTypeId: 1,
            betTypeName: 'BetType 1',
            liveStatusId: LiveStatus.NonLive
        }
    };

    const fakeUserQueryModel = {
        main: {
            periodType: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '',
            custId: 1,
            sportId: undefined,
            betTypeId: undefined,
            liveStatusId: undefined,
            listLeagueGroupId: '1,2',
            leagueGroupId: undefined,
            betTypeGroupId: undefined
        },
        sportType: {
            periodType: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '',
            custId: 1,
            sportId: 1,
            betTypeId: undefined,
            liveStatusId: undefined,
            listLeagueGroupId: '1,2',
            leagueGroupId: undefined,
            betTypeGroupId: undefined
        },
        betType: {
            periodType: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '',
            custId: 1,
            sportId: 1,
            betTypeId: 1,
            liveStatusId: LiveStatus.NonLive,
            listLeagueGroupId: '1,2',
            leagueGroupId: undefined,
            betTypeGroupId: undefined
        }
    };

    const fakeUserExcelModel = {
        main: _.extend({}, fakeUserQueryModel.main, {
            title: 'Punter Analysis - Customer 1 by bet count from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            filter: 'Sport Type: SportType 1, SportType 2'
        }),
        sportType: _.extend({}, fakeUserQueryModel.sportType, {
            title: 'Punter Analysis - Customer 1 by bet count from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            filter: 'Sport Type: SportType 1'
        }),
        betType: _.extend({}, fakeUserQueryModel.betType, {
            title: 'Punter Analysis - Customer 1 by bet count from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            filter: 'Sport Type: SportType 1\nBet Type: BetType 1 - Status: Non-Live'
        })
    };

    const fakeUserExcelFileName = {
        main: 'Top_Punter_Summary_Bet_Count_Search_by_User_from_Jan_01_2016_to_Feb_02_2016.xlsx',
        sportType: 'Top_Punter_Sporttype_Bet_Count_Search_by_User_from_Jan_01_2016_to_Feb_02_2016.xlsx',
        betType: 'Top_Punter_Bettype_Bet_Count_Search_by_User_from_Jan_01_2016_to_Feb_02_2016.xlsx',
    };

    const fakeLeagueQueryModel = {
        main: {
            periodType: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '1',
            custId: 1,
            sportId: undefined,
            betTypeId: undefined,
            liveStatusId: undefined,
            listLeagueGroupId: '1,2',
            leagueGroupId: undefined,
            betTypeGroupId: undefined
        },
        sportType: {
            periodType: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '1',
            custId: 1,
            sportId: 1,
            betTypeId: undefined,
            liveStatusId: undefined,
            listLeagueGroupId: '1,2',
            leagueGroupId: undefined,
            betTypeGroupId: undefined
        },
        betType: {
            periodType: 1,
            fromDate: '2016-01-01T00:00:00',
            toDate: '2016-02-02T00:00:00',
            listCombinationId: '1,2',
            leagueId: '1',
            custId: 1,
            sportId: 1,
            betTypeId: 1,
            liveStatusId: LiveStatus.NonLive,
            listLeagueGroupId: '1,2',
            leagueGroupId: undefined,
            betTypeGroupId: undefined
        }
    };

    const fakeLeagueExcelModel = {
        main: _.extend({}, fakeLeagueQueryModel.main, {
            title: 'Punter Analysis - Customer 1 by bet count from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            filter: 'Sport Type: SportType 1\nLeague: League 1'
        }),
        sportType: _.extend({}, fakeLeagueQueryModel.sportType, {
            title: 'Punter Analysis - Customer 1 by bet count from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            filter: 'Sport Type: SportType 1\nLeague: League 1'
        }),
        betType: _.extend({}, fakeLeagueQueryModel.betType, {
            title: 'Punter Analysis - Customer 1 by bet count from Jan 01, 2016 to Feb 02, 2016 (Daily)',
            filter: 'Sport Type: SportType 1\nBet Type: BetType 1 - Status: Non-Live\nLeague: League 1'
        })
    };

    const fakeLeagueExcelFileName = {
        main: 'Top_Punter_Summary_Bet_Count_Search_by_League_from_Jan_01_2016_to_Feb_02_2016.xlsx',
        sportType: 'Top_Punter_Sporttype_Bet_Count_Search_by_League_from_Jan_01_2016_to_Feb_02_2016.xlsx',
        betType: 'Top_Punter_Bettype_Bet_Count_Search_by_League_from_Jan_01_2016_to_Feb_02_2016.xlsx'
    };

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        });
    });

    it('query', inject([HttpService, GridService, BetCountGridService],
        (httpService: HttpService, gridService: GridService, service: BetCountGridService) => {
            const spy = spyOn(httpService, 'post').and.returnValue(Observable.of(null));

            function expectModel(model: BetCountGridModel, filterModel: FilterModel, expectedModel: any) {
                service.model = model;
                gridService.filterModel = filterModel;
                service.query();
                const mostRecentCall = spy.calls.mostRecent();
                const args = mostRecentCall.args;
                expect(args[0]).toBe('api/punter-analysis/bet-count');
                expect(args[1]).toEqual(expectedModel);
            }

            expectModel(fakeModel.main, fakeUserFilterModel, fakeUserQueryModel.main);
            expectModel(fakeModel.sportType, fakeUserFilterModel, fakeUserQueryModel.sportType);
            expectModel(fakeModel.betType, fakeUserFilterModel, fakeUserQueryModel.betType);
            expectModel(fakeModel.main, fakeLeagueFilterModel, fakeLeagueQueryModel.main);
            expectModel(fakeModel.sportType, fakeLeagueFilterModel, fakeLeagueQueryModel.sportType);
            expectModel(fakeModel.betType, fakeLeagueFilterModel, fakeLeagueQueryModel.betType);
        }));

    it('exportExcel', inject([HttpService, GridService, BetCountGridService],
        (httpService: HttpService, gridService: GridService, service: BetCountGridService) => {
            const downloadSpy = spyOn(httpService, 'download').and.returnValue(Observable.of(null));
            const saveSpy = spyOn(Helper, 'downloadFile').and.callFake(() => { });

            function expectModel(model: BetCountGridModel, filterModel: FilterModel, expectedExcel: any, expectedFileName: string) {
                service.model = model;
                gridService.filterModel = filterModel;
                service.exportExcel();
                let mostRecentCall = downloadSpy.calls.mostRecent();
                let args = mostRecentCall.args;

                expect(args[0]).toBe('api/punter-analysis/bet-count/excel');
                expect(args[1]).toEqual(expectedExcel);

                mostRecentCall = saveSpy.calls.mostRecent();
                args = mostRecentCall.args;
                expect(args[0]).toBe(null);
                expect(args[1]).toBe(expectedFileName);
            }

            expectModel(fakeModel.main, fakeUserFilterModel, fakeUserExcelModel.main, fakeUserExcelFileName.main);
            expectModel(fakeModel.sportType, fakeUserFilterModel, fakeUserExcelModel.sportType, fakeUserExcelFileName.sportType);
            expectModel(fakeModel.betType, fakeUserFilterModel, fakeUserExcelModel.betType, fakeUserExcelFileName.betType);
            expectModel(fakeModel.main, fakeLeagueFilterModel, fakeLeagueExcelModel.main, fakeLeagueExcelFileName.main);
            expectModel(fakeModel.sportType, fakeLeagueFilterModel, fakeLeagueExcelModel.sportType, fakeLeagueExcelFileName.sportType);
            expectModel(fakeModel.betType, fakeLeagueFilterModel, fakeLeagueExcelModel.betType, fakeLeagueExcelFileName.betType);
        }));
});

import * as _ from 'lodash';
import {
    SearchBy, PeriodType, OrderBy
} from './filter.model.main';

import { FilterModel } from './filter.model';
import { UserFilterModel } from './filter.model.user';
import { LeagueFilterModel } from './filter.model.league';
import { PerformanceFilterModel } from './filter.model.performance';

import { RoleId, SortBy } from 'nap-components/model';
import { TreeviewItem } from 'ngx-treeview';

const commonModel = {
    searchBy: SearchBy.User,
    searchByOptions: [
        { text: 'User', value: SearchBy.User },
        { text: 'League', value: SearchBy.League },
        { text: 'Performance', value: SearchBy.Performance }
    ],
    fromDate: { year: 2016, month: 1, day: 1 },
    toDate: { year: 2016, month: 2, day: 2 }
};

const commonUserLeagueModel = {
    topRow: 9999,
    orderBy: OrderBy.WinLoss,
    orderByOptions: [
        { text: 'WinLoss', value: OrderBy.WinLoss }
    ],
    sortBy: SortBy.Ascending,
    sortByOptions: [
        { text: 'Ascending', value: SortBy.Ascending }
    ],
    periodType: PeriodType.Daily,
    selectedBetTypeTreeviewItems: [
        {
            item: new TreeviewItem({ text: 'BetType 1', value: 1 }),
            parent: {
                item: new TreeviewItem({ text: 'SportType 1', value: 1 }),
                parent: {
                    item: new TreeviewItem({ text: 'Parent SportType 1', value: 1 }),
                    parent: null
                }
            }
        },
        {
            item: new TreeviewItem({ text: 'BetType 2', value: 2 }),
            parent: {
                item: new TreeviewItem({ text: 'SportType 2', value: 1 }),
                parent: {
                    item: new TreeviewItem({ text: 'Parent SportType 2', value: 1 }),
                    parent: null
                }
            }
        }
    ],
    selectedLeagueGroupTreeviewItems: [
        {
            item: new TreeviewItem({ text: 'Dota 2', value: 1 }),
            parent: { item: new TreeviewItem({ text: 'E-Sports', value: 1 }), parent: null }
        },
        {
            item: new TreeviewItem({ text: 'King of Glory', value: 2 }),
            parent: { item: new TreeviewItem({ text: 'E-Sports', value: 1 }), parent: null }
        }
    ]
};

const leagueModel: LeagueFilterModel = _.extend({}, commonUserLeagueModel, {
    leagueId: '1',
    leagueName: 'League 1',
    sportType: 1,
    sportOptions: [
        { label: 'SportType 1', value: 1 }
    ]
});

const performanceModel: PerformanceFilterModel = {
    depth: { role: RoleId.Agent, text: 'Agent' },
    userName: 'ABC',
    isUserNameRequired: true
};

const userModel: UserFilterModel = _.extend({}, commonUserLeagueModel, performanceModel);

export let fakeUserFilterModel: FilterModel = _.extend({}, commonModel, {
    searchBy: SearchBy.User,
    user: userModel,
    showLeagueGroupFilter: false
});


export let fakeLeagueFilterModel: FilterModel = _.extend({}, commonModel, {
    searchBy: SearchBy.League,
    league: leagueModel,
    showLeagueGroupFilter: false
});

export let fakePerformanceFilterModel: FilterModel = _.extend({}, commonModel, {
    searchBy: SearchBy.League,
    performance: performanceModel,
    showLeagueGroupFilter: false
});

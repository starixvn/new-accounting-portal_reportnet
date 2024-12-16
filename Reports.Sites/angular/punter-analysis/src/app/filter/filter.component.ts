import { fakeData } from './../grid/bet-type-grid/bet-type-grid.model.spec';
import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import {
    TreeviewItem, TreeviewConfig, TreeviewEventParser,
    DownlineTreeviewEventParser, DownlineTreeviewItem
} from 'ngx-treeview';
import * as _ from 'lodash';
import * as moment from 'moment';
import { IOption } from 'nap-components/select';
import { SelectItem } from 'nap-components/dropdown-select';
import { ToastService } from 'nap-components/toast';
import { TypeaheadQuery } from 'nap-components/dropdown-typeahead';
import { RoleId, SortBy } from 'nap-components/model';
import { DateHelper, DateRange } from 'nap-components/date';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import {
    SearchBy, PeriodType, OrderBy, FilterModelWrapper
} from './filter.model.main';

import { FilterModel } from './filter.model';
import { UserFilterModel } from './filter.model.user';
import { LeagueFilterModel } from './filter.model.league';
import { PerformanceFilterModel } from './filter.model.performance';
import { Depth } from './filter.model.depth';
import { SportsbookSportTypeBetType } from './filter.model.sportsbook-sporttype-bettype';

import { FilterService } from './filter.service';
import { PeriodTypeEventModel } from './period-type/period-type.model';

@Component({
    selector: 'nap-filter',
    templateUrl: './filter.component.html',
    styleUrls: [
        './filter.component.scss'
    ],
    providers: [
        FilterService,
        { provide: TreeviewEventParser, useClass: DownlineTreeviewEventParser }
    ]
})
export class FilterComponent implements OnInit {
    @Output() generate = new EventEmitter<FilterModel>();
    @Output() reset = new EventEmitter();
    RoleId = RoleId;
    SearchBy = SearchBy;
    searchBy = SearchBy.User;
    searchByOptions = this.getSearchByOptions();
    depthOptions = this.getDepthOptions();
    depth = this.depthOptions[0];
    topRows = 10;
    userName = '';
    userNameTypeaheadQuery = this.buildUserNameTypeaheadQuery();
    sortBy = SortBy.Descending;
    sortByOptions = this.getSortByOptions();
    orderBy = OrderBy.WinLoss;
    orderByOptions = this.getOrderByOptions();
    minDate: NgbDateStruct;
    maxDate: NgbDateStruct;
    productTreeviewItems: TreeviewItem[] = [];
    productTreeviewConfig: TreeviewConfig = {
        hasAllCheckBox: true,
        hasCollapseExpand: true,
        hasFilter: true,
        hasDivider: false,
        decoupleChildFromParent: false,
        maxHeight: 500
    };
    leagueGroupTreeviewItems: TreeviewItem[] = [];
    leagueGroupTreeviewConfig: TreeviewConfig = {
        hasAllCheckBox: true,
        hasCollapseExpand: true,
        hasFilter: true,
        hasDivider: false,
        decoupleChildFromParent: false,
        maxHeight: 500
    };

    sportType: number;
    sportOptions: IOption[];
    leagueId: string;
    leagueName = '';
    leagueTypeaheadQuery = this.buildLeagueTypeaheadQuery();
    betTypeTreeviewItems: TreeviewItem[] = [];
    betTypeTreeviewConfig: TreeviewConfig = {
        hasAllCheckBox: true,
        hasCollapseExpand: false,
        hasFilter: true,
        hasDivider: false,
        decoupleChildFromParent: false,
        maxHeight: 500
    };

    monthRangeFromDate: NgbDateStruct;
    monthRangeToDate: NgbDateStruct;
    showLeagueFilter = false;
    countingQueryLeagueGroup = 0;

    private selectedBetTypeTreeviewItems: DownlineTreeviewItem[];
    private periodTypeEventModel: PeriodTypeEventModel;
    private sportbooksSportTypeBetTypeData: SportsbookSportTypeBetType[];
    private selectedLeagueGroupTreeviewItems: DownlineTreeviewItem[];

    constructor(
        private service: FilterService,
        private toastService: ToastService
    ) {
        const todayNgbDate = DateHelper.convertMomentToDateStruct(moment().startOf('day'));
        this.minDate = { year: todayNgbDate.year - 1, month: todayNgbDate.month, day: todayNgbDate.day };
        this.maxDate = todayNgbDate;
    }

    get isUserNameRequired(): boolean {
        switch (this.searchBy) {
            case SearchBy.User:
                return this.depth.role !== RoleId.Member;
            case SearchBy.League:
                return false;
            case SearchBy.Performance:
                return true;
        }
    }

    get userNamePlaceholder(): string {
        switch (this.depth.role) {
            case RoleId.Super:
                return 'Super name';
            case RoleId.Master:
                return 'Master name';
            case RoleId.Agent:
                return 'Agent name';
            case RoleId.Member:
                return this.searchBy === SearchBy.User ? 'All' : 'Member name';
        }
    }

    ngOnInit() {
        this.service.queryDataWarehouseDateRange().subscribe(model => {
            this.minDate = DateHelper.convertMomentToDateStruct(moment.utc(model.MinDate));
            this.maxDate = DateHelper.convertMomentToDateStruct(moment.utc(model.MaxDate));
            this.monthRangeToDate = { year: this.maxDate.year, month: this.maxDate.month, day: 1 };
            let momentFromDate = DateHelper.convertDateStructToMoment(this.maxDate).subtract(5, 'months');
            const momentMinDate = DateHelper.convertDateStructToMoment(this.minDate);
            if (momentFromDate.isBefore(momentMinDate)) {
                momentFromDate = momentMinDate;
            }
            const fromDate = DateHelper.convertDateToDateStruct(momentFromDate.toDate());
            this.monthRangeFromDate = { year: fromDate.year, month: fromDate.month, day: 1 };
        });
        this.service.queryProductTreeviewItems().subscribe(treeItems => this.productTreeviewItems = treeItems);
    }

    onSearchByChange() {
        this.reset.emit();
        switch (this.searchBy) {
            case SearchBy.User:
                if (this.depth.role === RoleId.Super || this.depth.role === RoleId.Master) {
                    this.depth = this.depthOptions[0];
                }
                break;
            case SearchBy.League:
                if (!this.sportOptions) {
                    this.service.querySportbooksSportTypeBetType().subscribe(data => {
                        this.sportbooksSportTypeBetTypeData = data;
                        this.loadSports();
                    });
                }

                this.showLeagueFilter = false;
                break;
        }
        this.depthOptions = this.getDepthOptions();
    }

    onPeriodTypeChange(model: PeriodTypeEventModel) {
        this.periodTypeEventModel = model;
    }

    onProductSelectedChange(downlineTreeviewItems: DownlineTreeviewItem[]) {
        this.selectedBetTypeTreeviewItems = downlineTreeviewItems;
        this.showLeagueFilter = this.isShowLeagueFilter();

        if (this.showLeagueFilter && this.countingQueryLeagueGroup === 0) {
            this.queryLeagueGroup();
            this.countingQueryLeagueGroup++;
        } else {
            this.countingQueryLeagueGroup = 0;
        }
    }

    onLeagueGroupSelectedChange(downlineTreeviewItems: DownlineTreeviewItem[]) {
        this.selectedLeagueGroupTreeviewItems = downlineTreeviewItems;
    }

    onSportChange() {
        this.betTypeTreeviewItems = this.sportbooksSportTypeBetTypeData
            .filter(item => item.SportType === this.sportType)
            .map(item => new TreeviewItem({
                text: item.BetTypeName,
                value: item.CombinationId.toString()
            }));
    }

    onBetTypeSelectedChange(downlineTreeviewItems: DownlineTreeviewItem[]) {
        this.selectedBetTypeTreeviewItems = downlineTreeviewItems;
    }

    onGenerate() {
        this.reset.emit();
        if (this.searchBy === SearchBy.League) {
            const searchText = this.leagueName.trim();
            if (searchText === '') {
                this.toastService.activate('League name field is required!');
                return;
            } else {
                this.service.querySportTypeLeague(this.sportType, searchText, true).subscribe(leagues => {
                    if (leagues.length === 0) {
                        this.toastService.activate('League is not available.');
                    } else {
                        this.leagueId = leagues[0].LeagueId;
                        this.validateBeforeGenerating();
                    }
                });
            }
        } else {
            this.validateBeforeGenerating();
        }
    }

    private validateBeforeGenerating() {
        const model = this.createModel();
        const wrapper = new FilterModelWrapper(model);
        const validateMessage = wrapper.validate();
        if (validateMessage) {
            this.toastService.activate(validateMessage);
            return;
        }

        this.service.checkDatabaseAvailable().subscribe(isAvailable => {
            if (isAvailable) {
                this.generate.emit(model);
            } else {
                this.toastService.activate('Data is being processed.... Please try again after 10 minutes!');
            }
        });
    }

    private loadSports() {
        let sportName = '';
        this.sportOptions = [];
        this.sportbooksSportTypeBetTypeData.forEach(item => {
            if (item.SportName !== sportName) {
                sportName = item.SportName;
                this.sportOptions.push({
                    label: item.SportName,
                    value: item.SportType
                });
            }
        });
        setTimeout(() => {
            this.sportType = this.sportOptions[0].value;
        });
    }

    private getExactDateRange(): DateRange {
        const minDate = DateHelper.convertDateStructToMoment(this.minDate);
        let fromDate = DateHelper.convertDateStructToMoment(this.periodTypeEventModel.fromDate);
        let toDate = DateHelper.convertDateStructToMoment(this.periodTypeEventModel.toDate);
        if (fromDate.isBefore(minDate)) {
            fromDate = moment.utc(minDate);
        }
        switch (this.periodTypeEventModel.periodType) {
            case PeriodType.Weekly:
                toDate = toDate.endOf('week');
                break;
            case PeriodType.Monthly:
            case PeriodType.MonthRange:
                toDate = toDate.endOf('month');
                break;
            case PeriodType.Annually:
                toDate = toDate.endOf('year');
                break;
        }

        const maxDate = DateHelper.convertDateStructToMoment(this.maxDate);
        if (toDate.isAfter(maxDate)) {
            toDate = maxDate;
        }
        if (fromDate.isAfter(toDate)) {
            fromDate = moment.utc(toDate);
        }
        return {
            fromDate: DateHelper.convertDateToDateStruct(fromDate.toDate()),
            toDate: DateHelper.convertDateToDateStruct(toDate.toDate())
        };
    }

    private buildUserNameTypeaheadQuery(): TypeaheadQuery {
        return new TypeaheadQuery(term => {
            return this.service.queryUserNameSuggestion(term, this.depth.role);
        });
    }

    private buildLeagueTypeaheadQuery(): TypeaheadQuery {
        return new TypeaheadQuery(term => {
            let searchText = term.replace(/,/g, '');
            let words = searchText.split(' ');
            words = words.filter(word => word.trim() !== '');
            searchText = words.join(',');
            return this.service.querySportTypeLeague(this.sportType, searchText, false)
                .map(list => {
                    return list.map(item => item.LeagueName)
                });
        });
    }

    private createModel(): FilterModel {
        const model: FilterModel = {
            searchBy: this.searchBy,
            searchByOptions: this.searchByOptions,
            fromDate: this.searchBy === SearchBy.Performance ? this.monthRangeFromDate : this.periodTypeEventModel.fromDate,
            toDate: this.searchBy === SearchBy.Performance ? this.monthRangeToDate : this.periodTypeEventModel.toDate,
            showLeagueGroupFilter: this.showLeagueFilter
        };
        switch (this.searchBy) {
            case SearchBy.User:
                model.user = this.createUserModel();
                break;
            case SearchBy.League:
                model.league = this.createLeagueModel();
                break;
            case SearchBy.Performance:
                model.performance = this.createPerformanceModel();
                break;
        }

        return model;
    }

    private createUserModel(): UserFilterModel {
        return {
            topRow: this.topRows,
            orderBy: this.orderBy,
            orderByOptions: this.orderByOptions,
            sortBy: this.sortBy,
            sortByOptions: this.sortByOptions,
            periodType: this.periodTypeEventModel.periodType,
            selectedBetTypeTreeviewItems: this.selectedBetTypeTreeviewItems,
            selectedLeagueGroupTreeviewItems: this.showLeagueFilter ? this.selectedLeagueGroupTreeviewItems : [],
            depth: this.depth,
            userName: _.trim(this.userName),
            isUserNameRequired: this.isUserNameRequired
        };
    }

    private createLeagueModel(): LeagueFilterModel {
        return {
            topRow: this.topRows,
            orderBy: this.orderBy,
            orderByOptions: this.orderByOptions,
            sortBy: this.sortBy,
            sortByOptions: this.sortByOptions,
            periodType: this.periodTypeEventModel.periodType,
            selectedBetTypeTreeviewItems: this.selectedBetTypeTreeviewItems,
            leagueId: this.leagueId,
            leagueName: this.leagueName,
            sportType: this.sportType,
            sportOptions: this.sportOptions,
            selectedLeagueGroupTreeviewItems: this.showLeagueFilter ? this.selectedLeagueGroupTreeviewItems : []
        };
    }

    private createPerformanceModel(): PerformanceFilterModel {
        return {
            depth: this.depth,
            userName: _.trim(this.userName),
            isUserNameRequired: this.isUserNameRequired
        };
    }

    private getSearchByOptions(): SelectItem[] {
        return [{
            text: 'User',
            value: SearchBy.User
        }, {
            text: 'League',
            value: SearchBy.League
        }, {
            text: 'Performance',
            value: SearchBy.Performance
        }];
    }

    private getDepthOptions(): Depth[] {
        let result: Depth[] = [{
            text: 'Agent',
            role: RoleId.Agent
        }, {
            text: 'Member',
            role: RoleId.Member
        }];
        if (this.searchBy === SearchBy.Performance) {
            const list: Depth[] = [{
                text: 'Super',
                role: RoleId.Super
            }, {
                text: 'Master',
                role: RoleId.Master
            }];
            result = _.concat(list, result);
        }
        return result;
    }

    private getSortByOptions(): SelectItem[] {
        return [{
            text: 'Ascending',
            value: SortBy.Ascending
        }, {
            text: 'Descending',
            value: SortBy.Descending
        }];
    }

    private getOrderByOptions(): SelectItem[] {
        return [{
            text: 'Win Loss',
            value: OrderBy.WinLoss
        }, {
            text: 'Turnover',
            value: OrderBy.Turnover
        }, {
            text: 'Margin',
            value: OrderBy.Margin
        }];
    }

    private isShowLeagueFilter(): boolean {
        let showFilter = false;
        this.selectedBetTypeTreeviewItems.forEach(betTypeItem => {
            const sportTypeItem = betTypeItem.parent.parent;
            if (sportTypeItem && sportTypeItem.item.text === 'E-Sports') {
                showFilter = true;
            }
        });

        return showFilter;
    }

    private queryLeagueGroup() {
        this.service.queryLeagueGroupTreeviewItems().subscribe(treeItems => this.leagueGroupTreeviewItems = treeItems);
    }
}

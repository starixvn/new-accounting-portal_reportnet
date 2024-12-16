import * as _ from 'lodash';
import { DateFormat, DateHelper } from 'nap-components/date';
import { NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import { FilterModel } from './filter.model';
import { PerformanceFilterModel } from './filter.model.performance';
import { CommonUserLeagueFilterModel } from './filter.model.common-user-league';

export enum SearchBy {
    User = 1,
    League = 2,
    Performance = 3
}

export enum PeriodType {
    Daily = 1,
    Weekly = 2,
    Monthly = 3,
    Annually = 4,
    MonthRange = 5
}

export enum OrderBy {
    WinLoss = 1,
    Turnover = 2,
    Margin = 3
}


export class UserLeagueFilterModelWrapper {
    constructor(private model: CommonUserLeagueFilterModel) { }

    get periodTitle(): string {
        switch (this.model.periodType) {
            case PeriodType.Daily:
                return 'Daily';
            case PeriodType.Weekly:
                return 'Weekly';
            case PeriodType.Monthly:
            case PeriodType.MonthRange:
                return 'Monthly';
            case PeriodType.Annually:
                return 'Annually';
        }
    }

    get listCombinationId(): string {
        return this.model.selectedBetTypeTreeviewItems.map(betTypeItem => betTypeItem.item.value).join(',');
    }

    get orderByTitle(): string {
        return this.model.orderByOptions.filter(option => option.value === this.model.orderBy)[0].text;
    }

    get sortByTitle(): string {
        return this.model.sortByOptions.filter(option => option.value === this.model.sortBy)[0].text;
    }

    get listLeagueGroupCombinationId(): string {
        return this.model.selectedLeagueGroupTreeviewItems.map(leagueItem => leagueItem.item.value).join(',');
    }
}


export class FilterModelWrapper {
    readonly userLeagueModel: CommonUserLeagueFilterModel;
    readonly userLeagueWrapper: UserLeagueFilterModelWrapper;
    readonly userPerformanceModel: PerformanceFilterModel;

    static getDateFromToString(periodType: PeriodType, fromDate: NgbDateStruct, toDate: NgbDateStruct): string {
        switch (periodType) {
            case PeriodType.Daily:
            case PeriodType.Weekly:
                const fromDateString = DateHelper.formatMomentDate(DateHelper.convertDateStructToMoment(fromDate), DateFormat.Date);
                const toDateString = DateHelper.formatMomentDate(DateHelper.convertDateStructToMoment(toDate), DateFormat.Date);
                return `from ${fromDateString} to ${toDateString}`;
            case PeriodType.Monthly:
                const monthDateString = DateHelper.formatMomentDate(DateHelper.convertDateStructToMoment(fromDate), DateFormat.Month);
                return `in ${monthDateString}`;
            case PeriodType.MonthRange:
                const fromMonthDateString = DateHelper.formatMomentDate(DateHelper.convertDateStructToMoment(fromDate), DateFormat.Month);
                const toMonthDateString = DateHelper.formatMomentDate(DateHelper.convertDateStructToMoment(toDate), DateFormat.Month);
                return `from ${fromMonthDateString} to ${toMonthDateString}`;
            case PeriodType.Annually:
                const yearDateString = DateHelper.formatMomentDate(DateHelper.convertDateStructToMoment(fromDate), DateFormat.Year);
                return `in ${yearDateString}`;
        }

        return '';
    }

    constructor(
        private filterModel: FilterModel
    ) {
        this.userLeagueModel = this.filterModel.user || this.filterModel.league;
        this.userLeagueWrapper = new UserLeagueFilterModelWrapper(this.userLeagueModel);
        this.userPerformanceModel = this.filterModel.user || this.filterModel.performance;
    }

    validate(): string {
        const momentFromDate = DateHelper.convertDateStructToMoment(this.filterModel.fromDate);
        const momentToDate = DateHelper.convertDateStructToMoment(this.filterModel.toDate);
        if (this.userLeagueModel) {
            switch (this.userLeagueModel.periodType) {
                case PeriodType.Daily:
                case PeriodType.Weekly:
                    if (momentFromDate.add(1, 'month').add(-1, 'day').isBefore(momentToDate)) {
                        return 'Date range should not exceed 1 month!';
                    }
                    break;
            }
        } else {
            if (momentFromDate.add(1, 'year').add(-1, 'day').isBefore(momentToDate)) {
                return 'Date range should not exceed 1 year!';
            }
        }

        if (this.userPerformanceModel && this.userPerformanceModel.isUserNameRequired && this.userPerformanceModel.userName === '') {
            return 'Account name field is required!';
        }

        if (this.filterModel.user && this.filterModel.user.selectedBetTypeTreeviewItems.length === 0) {
            return 'Please select at least one Product!';
        }

        if (this.filterModel.user
            && this.filterModel.user.selectedLeagueGroupTreeviewItems.length === 0
            && this.filterModel.showLeagueGroupFilter) {
            return 'Please select at least one league group!';
        }

        if (this.filterModel.league) {
            if (this.filterModel.league.selectedBetTypeTreeviewItems.length === 0) {
                return 'Please select at least one Bet Type!';
            }
        }

        return undefined;
    }

    get depthTitle(): string {
        const commonModel = this.filterModel.user || this.filterModel.performance;
        return commonModel.depth.text;
    }

    get userNameTitle(): string {
        if (this.filterModel.user) {
            const userName = this.filterModel.user.userName;
            if (!_.isNil(userName) && userName !== '') {
                return userName;
            }

            return 'All';
        }
    }

    get fromDateQueryString(): string {
        return DateHelper.convertDateStructToString(this.filterModel.fromDate);
    }

    get toDateQueryString(): string {
        return DateHelper.convertDateStructToString(this.filterModel.toDate);
    }

    get selectedSportTypes(): string {
        if (this.filterModel.user) {
            const sportTypes: string[] = [];
            this.filterModel.user.selectedBetTypeTreeviewItems.forEach(betTypeItem => {
                let sportType = betTypeItem.parent.item.text;

                if (betTypeItem.parent.parent && betTypeItem.parent.parent.item.text === 'E-Sports') {
                    sportType = 'E-Sports';
                }

                if (sportTypes.indexOf(sportType) === -1) {
                    sportTypes.push(sportType);
                }
            });
            return sportTypes.join(', ');
        } else if (this.filterModel.league) {
            return this.filterModel.league.sportOptions.filter(item => item.value === this.filterModel.league.sportType)[0].label;
        }
    }

    get leagueTitle(): string {
        return this.filterModel.league.leagueName;
    }

    get leagueId(): string {
        return this.filterModel.league ? this.filterModel.league.leagueId : '';
    }

    get searchByTitle(): string {
        return this.filterModel.searchByOptions.filter(option => option.value === this.filterModel.searchBy)[0].text;
    }

    get searchByFileName(): string {
        return `Search_by_${this.searchByTitle}`;
    }

    get dateFromToTitle(): string {
        const dateFromToDateString = FilterModelWrapper.getDateFromToString(
            this.userLeagueModel.periodType,
            this.filterModel.fromDate,
            this.filterModel.toDate);
        return `${dateFromToDateString} (${this.userLeagueWrapper.periodTitle})`;
    }

    get dateFromToFileName(): string {
        let dateFromToDateString = FilterModelWrapper.getDateFromToString(
            this.userLeagueModel.periodType,
            this.filterModel.fromDate,
            this.filterModel.toDate);
        dateFromToDateString = dateFromToDateString.replace(/ /g, '_').replace(/,/g, '');
        return dateFromToDateString;
    }
}

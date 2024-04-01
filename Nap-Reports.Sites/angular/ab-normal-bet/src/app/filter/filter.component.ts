import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { DateHelper, NapDateStruct, DateFormat } from 'nap-components/date';
import { FilterService } from './filter.service';
import { FilterModel, FilterItemModel, SiteModel } from './filter.model';
import { TreeviewConfig, DropdownTreeviewComponent } from 'ngx-treeview';
import { CONFIG } from '../common/date-config';
import * as moment from 'moment';
import { ToastService } from 'nap-components/toast';
import { PeriodTypeEventModel } from './period-type/period-type.model';
import { TypeaheadQuery } from 'nap-components/dropdown-typeahead';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {
  @ViewChild('siteDropdownTreeview') siteDropdownTreeview: DropdownTreeviewComponent;
  @ViewChild('betTypeDropdownTreeview') betTypeDropdownTreeview: DropdownTreeviewComponent;
  @ViewChild('sportDropdownTreeview') sportDropdownTreeview: DropdownTreeviewComponent;
  @ViewChild('reasonDropdownTreeview') reasonDropdownTreeview: DropdownTreeviewComponent;
  reasons: FilterItemModel[] = [];
  reasonTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedReasons: number[] = [];
  sites: SiteModel[] = [];
  siteTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedSites: number[] = [];
  sports: FilterItemModel[] = [];
  sportTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedSports: number[] = [];
  betTypes: FilterItemModel[] = [];
  betTypeTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedBetTypes: number[] = [];
  @Output() generated = new EventEmitter<FilterModel>();
  isExcludeTest = true;
  memberStr = '';
  showLicenseeControl = this.memberStr === '';
  minStartDate: NapDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.yesterday.add(-2, 'year'));
  maxEndDate: NapDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.yesterday);
  startDate: NapDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));
  endDate: NapDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.yesterday));

  sportIds = '0';
  periodTypeEventModel: PeriodTypeEventModel;
  currencies: FilterItemModel[] = [];
  selectedCurrency: FilterItemModel;
  leagueId: string;
  leagueName = '';
  leagueNameTypeaheadQuery = this.buildLeagueNameTypeaheadQuery();
  leagueNamePlaceholder = 'League name';

  constructor(
    private readonly filterSerivce: FilterService,
    private readonly toastService: ToastService) { }

  ngOnInit() {
    this.filterSerivce.getReason().subscribe(reasons => {
      this.reasons = reasons;
    });

    this.filterSerivce.getSite().subscribe(sites => {
      this.sites = sites;
    });

    this.filterSerivce.getSport().subscribe(sports => {
      this.sports = sports;
    });

    this.filterSerivce.getCurrency().subscribe(currencies => {
      this.currencies = currencies;
      this.selectedCurrency = currencies[0];
    });
  }

  onGenerateClick() {
    if (!this.isValid) {
      return;
    }

    this.checkLeagueAndGenerate();
  }

  onPeriodTypeChange(model: PeriodTypeEventModel) {
    this.periodTypeEventModel = model;
  }

  onMemberChange(memberValue: string) {
    this.showLicenseeControl = memberValue === '';
  }

  onSportSelectedChange(selectedSports: number[]) {
    this.selectedSports = selectedSports;
    if (selectedSports.length > 0) {
      this.sportIds = selectedSports.join(',');
      this.filterSerivce.getBetType(this.sportIds).subscribe(betTypes => {
        this.betTypes = betTypes;
      });
    }
  }

  private createModel(): FilterModel {
    const fromDate = DateHelper.convertDateStructToMoment(this.periodTypeEventModel.fromDate);
    const toDate = DateHelper.convertDateStructToMoment(this.periodTypeEventModel.toDate);

    const model: FilterModel = {
      fromDate: DateHelper.formatMomentDate(fromDate, DateFormat.Date),
      toDate: DateHelper.formatMomentDate(toDate, DateFormat.Date),
      sport: this.sports.length === this.selectedSports.length ? [0] : this.selectedSports,
      bettype: this.betTypes.length === this.selectedBetTypes.length ? [0] : this.selectedBetTypes,
      reason: this.reasons.length === this.selectedReasons.length ? [0] : this.selectedReasons,
      licensee: this.sites.length === this.selectedSites.length ? [-1] : this.selectedSites,
      isTest: this.isExcludeTest,
      member: this.memberStr,
      leagueId: this.leagueId,
      pageIndex: 0,
      pageSize: 100,
      isPageChanged: false,
      currencyId: this.selectedCurrency.Id.toString()
    };

    return model;
  }

  private get isValid(): boolean {
    const fromDate = DateHelper.convertDateStructToMoment(this.periodTypeEventModel.fromDate);
    const toDate = DateHelper.convertDateStructToMoment(this.periodTypeEventModel.toDate);
    const maxDate = fromDate.add(1, 'year');

    if (!toDate.isBefore(maxDate)) {
      this.toastService.activate(`Date range should not exceed 1 year!`);
      return false;
    }

    if (this.selectedReasons.length === 0) {
      this.toastService.activate(`Please select at least one reason!`);
      return false;
    }

    if (this.selectedSites.length === 0) {
      this.toastService.activate(`Please select at least one licensee!`);
      return false;
    }

    if (this.selectedSports.length === 0) {
      this.toastService.activate(`Please select at least one sport!`);
      return false;
    }

    if (this.selectedBetTypes.length === 0) {
      this.toastService.activate(`Please select at least one bet type!`);
      return false;
    }

    return true;
  }

  private checkLeagueAndGenerate() {
    const searchText = this.leagueName.trim();
    if (searchText === '') {
      this.leagueId = '0';
      this.createModelAndGenerate();
    } else {
      this.filterSerivce.queryLeagueSuggestion(this.selectedSports.join(','), searchText, true).subscribe(leagues => {
        if (leagues.length === 0) {
          this.toastService.activate('League is not available.');
          return;
        } else {
          this.leagueId = leagues[0].Id;
        }

       this.createModelAndGenerate();
      });
    }
  }

  private createModelAndGenerate() {
    const model = this.createModel();
    this.generated.emit(model);
  }

  private buildLeagueNameTypeaheadQuery(): TypeaheadQuery {
    return new TypeaheadQuery(term => {
      let searchText = term.replace(/,/g, '');
      let words = searchText.split(' ');
      words = words.filter(word => word.trim() !== '');
      searchText = words.join(',');
      return this.filterSerivce.queryLeagueSuggestion(this.selectedSports.join(','), searchText, false)
        .map(list => {
          return list.map(item => item.Name);
        });
    });
  }
}

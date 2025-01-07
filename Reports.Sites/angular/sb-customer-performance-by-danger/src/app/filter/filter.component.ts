import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import * as moment from 'moment';
import * as _ from 'lodash';
import { DateFormat, DateHelper, NapDateStruct } from 'nap-components/date';
import { SelectItem } from 'nap-components/dropdown-select';
import { TypeaheadQuery } from 'nap-components/dropdown-typeahead';
import { ToastService } from 'nap-components/toast';
import { DropdownTreeviewComponent, TreeviewConfig, TreeviewItem } from 'ngx-treeview';
import { CONFIG } from '../common/date-config';
import { FilterModel, LeagueGroup, Site, Sport } from './filter.model';
import { FilterService } from './filter.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
  providers: [FilterService]
})
export class FilterComponent implements OnInit {
  @ViewChild('siteDropdownTreeview') siteDropdownTreeview: DropdownTreeviewComponent;
  @ViewChild('leagueGroupDropdownTreeview') leagueGroupDropdownTreeview: DropdownTreeviewComponent;
  @ViewChild('sportDropdownTreeview') sportDropdownTreeview: DropdownTreeviewComponent;
  @ViewChild('dangerLevelDropdownTreeview') dangerLevelDropdownTreeview: DropdownTreeviewComponent;
  @Output() generated = new EventEmitter<FilterModel>();
  @Output() reset = new EventEmitter();
  today: NapDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.today);
  minStartDate: NapDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.balanceUpDate);
  maxEndDate: NapDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.today);
  fromDate: NapDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  toDate: NapDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  username = '';
  sportTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedSports: string[] = [];
  sports: Sport[] = [];
  siteTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedSites: string[] = [];
  sites: Site[] = [];
  leagueName = '';
  leagueId: string;
  leagueNameTypeaheadQuery = this.buildLeagueNameTypeaheadQuery();
  leagueNamePlaceholder = 'All Leagues';

  leagueGroupTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedleagueGroups: string[] = [];
  leagueGroups: LeagueGroup[] = [];
  topRows: number = 1000;
  statusOptions: SelectItem[] = [];
  status: string = 'All';
  dangerLevelTreeviewItems: TreeviewItem[];
  selectedDangerLevel: string[];
  dangerLevelTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });

  constructor(private readonly filterService: FilterService, private readonly toastService: ToastService) { }

  ngOnInit() {
    this.getSite();
    this.getSport();
    this.getLeagueGroup();
    this.getStatusOptions();
    this.getDangerLevel();
  }

  onGenerateClick() {
    this.OnGenerate();
  }

  onSportSelectedChange($event) {
    this.selectedSports = $event;

    if (this.selectedSports.length > 0) {
      let sportBettypes = this.selectedSports.join(',');
      this.filterService.getLeagueGroup(sportBettypes).subscribe(leagueGroups => {
        this.leagueGroups = leagueGroups;
      });
    }
  }

  private getSport() {
    this.filterService.getSport().subscribe(sports => {
      this.sports = sports;
    });
  }

  private getDangerLevel() {
    this.dangerLevelTreeviewItems = this.filterService.getDangerLevel();
  }

  onFromDateChange() {
    let winlostDate: moment.Moment;
    winlostDate = DateHelper.convertDateStructToMoment(this.fromDate);
    winlostDate = moment.utc(winlostDate);
    this.fromDate = DateHelper.convertMomentToDateStruct(winlostDate);
  }

  onToDateChange() {
    let winlostDate: moment.Moment;
    winlostDate = DateHelper.convertDateStructToMoment(this.toDate);
    winlostDate = moment.utc(winlostDate);
    this.toDate = DateHelper.convertMomentToDateStruct(winlostDate);
  }

  private getSite() {
    this.filterService.getSite().subscribe(sites => this.sites = sites);
  }

  private getLeagueGroup() {
    this.filterService.getLeagueGroup('All').subscribe(leagueGroups => this.leagueGroups = leagueGroups);
  }

  private getStatusOptions() {
    this.statusOptions = this.filterService.getStatusOptions();
  }

  private OnGenerate() {
    this.reset.emit();

    const model = this.createModel();

    if (!this.isValid) {
      return;
    }

    this.generated.emit(model);
  }

  private createModel(): FilterModel {
    const fromDate = DateHelper.convertDateStructToMoment(this.fromDate);
    const toDate = DateHelper.convertDateStructToMoment(this.toDate);

    const model: FilterModel = {
      fromDate: DateHelper.formatMomentDate(fromDate, DateFormat.Date),
      toDate: DateHelper.formatMomentDate(toDate, DateFormat.Date),
      divisions: this.selectedSites.join(','),
      dangerLevels: this.selectedDangerLevel.join(','),
      sportBettypes: this.selectedSports.join(','),
      leagueName: this.leagueName.length === 0 ? 'All Leagues' : this.leagueName,
      leagueGroupIds: this.selectedleagueGroups.join(','),
      liveStatus: this.status,
      topUsers: this.topRows
    };

    return model;
  }

  private get isValid(): boolean {
    const fromDate = DateHelper.convertDateStructToMoment(this.fromDate);
    const toDate = DateHelper.convertDateStructToMoment(this.toDate);
    const maxDate = fromDate.add(7, 'days');

    if (!toDate.isBefore(maxDate)) {
      this.toastService.activate(`Date range should not exceed 7 days!`);
      return false;
    }

    if (this.selectedDangerLevel.length === 0) {
      this.toastService.activate(`Please select at least one danger level!`);
      return false;
    }

    if (this.topRows >= 10000) {
      this.toastService.activate(`Top rows value must less than 10.000`);
      return false;
    }

    if (this.selectedSports.length === 0) {
      this.toastService.activate(`Please select at least one sport!`);
      return false;
    }

    if (this.selectedleagueGroups.length === 0) {
      this.toastService.activate(`Please select at least one league group!`);
      return false;
    }

    if (this.selectedSites.length === 0) {
      this.toastService.activate(`Please select at least one division!`);
      return false;
    }

    return true;
  }

  private buildLeagueNameTypeaheadQuery(): TypeaheadQuery {
    return new TypeaheadQuery(term => {
      let searchText = term.replace(/,/g, '');
      let words = searchText.split(' ');
      words = words.filter(word => word.trim() !== '');
      searchText = words.join(',');
      let sportBettypes = this.selectedSports.join(',');
      return this.filterService.queryLeagueSuggestion(sportBettypes, searchText);
    });
  }
}

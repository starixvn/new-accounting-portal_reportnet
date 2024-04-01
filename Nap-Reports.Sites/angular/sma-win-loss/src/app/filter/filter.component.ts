import { TreeviewConfig, DropdownTreeviewComponent } from 'ngx-treeview';
import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { SelectItem, FilterModel, Currency, Sport, Site, LeagueGroup, CommonItem, BetFrom, LeagueGroupModel } from './filter.model';
import { NapDateStruct, DateHelper } from 'nap-components/date';
import { CONFIG } from '../common/config';
import { FilterService } from './fiter.service';
import { ToastService } from 'nap-components/toast';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
  providers: [FilterService]
})
export class FilterComponent implements OnInit {
  @ViewChild('currencyDropdownTreeview') currencyDropdownTreeview: DropdownTreeviewComponent;
  @ViewChild('siteDropdownTreeview') siteDropdownTreeview: DropdownTreeviewComponent;
  @ViewChild('leagueGroupDropdownTreeview') leagueGroupDropdownTreeview: DropdownTreeviewComponent;
  @Output() generated = new EventEmitter<FilterModel>();
  today: NapDateStruct = DateHelper.convertMomentToDateStruct(CONFIG.date.today);
  yearOptions: SelectItem[] = [];
  yearSelected: SelectItem;
  username = '';
  betTypeOptions: SelectItem[] = [
    new SelectItem('All', 0),
    new SelectItem('Virtual Rate', 1)
  ];
  currencyTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedCurrencies: number[] = [];
  betTypeSelected: SelectItem = null;
  currencies: Currency[] = [];
  levelOptions: SelectItem[] = [
    new SelectItem('Super', 4),
    new SelectItem('N/A', -1)
  ];
  levelSelected: SelectItem = null;
  isSearchDownlines = false;
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
  selectedSites: number[] = [];
  sites: Site[] = [];
  leagueName = '';
  isEnableSearchExactly = false;

  leagueGroupTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: true,
    maxHeight: 500
  });
  selectedleagueGroups: string[] = [];
  leagueGroups: LeagueGroupModel = new LeagueGroupModel(0, []);
  commonItemTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedAccountStatus: string[] = [];
  accountStatus: CommonItem[] = [];
  selectedVRSettings: string[] = [];
  vrSettings: CommonItem[] = [];
  availableDateData = '';
  liveStatus: CommonItem[] = [];
  selectedLiveStatus: CommonItem[] = [];

  betFromTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedBetFroms: string[] = [];
  betFroms: BetFrom[] = [];
  permissionProfileId: number = CONFIG.permission.permissionProfileId;

  constructor(private readonly filterService: FilterService, private readonly toastService: ToastService) { }

  ngOnInit() {
    this.getYear();
    this.getCurrency();
    this.getSite();
    this.getSport();
    this.getLeagueGroup();
    this.getAccountStatus();
    this.getVRSettings();
    this.yearSelected = this.yearOptions[0];
    this.betTypeSelected = this.betTypeOptions[0];
    this.levelSelected = this.levelOptions[0];
    this.getAvailableDateData();
    this.getBetFroms();
    this.getLiveStatus();
  }

  onGenerateClick() {
    var permissionProfileId = CONFIG.permission.permissionProfileId;
    this.OnGenerate();
  }

  onLevelSelected($event) {
    this.levelSelected = $event;
    this.isEnableSearchExactly = $event.Value === -1;

    if ($event.Value !== -1) {
      this.username = '';
    }
  }

  private getCurrency() {
    this.filterService.getCurrency().subscribe(currencies => this.currencies = currencies);
  }

  private getSport() {
    this.filterService.getSport().subscribe(sports => this.sports = sports);
  }

  private getSite() {
    this.filterService.getSite().subscribe(sites => this.sites = sites);
  }

  private getLeagueGroup() {
    this.filterService.getLeagueGroup().subscribe(leagueGroups => this.leagueGroups = leagueGroups);
  }

  private getAvailableDateData() {
    this.filterService.getAvailableDateData().subscribe(date => this.availableDateData = `* Data is ready up to ${date}`);
  }

  private getAccountStatus() {
    this.accountStatus = this.filterService.getAccountStatus();
  }

  private getVRSettings() {
    this.vrSettings = this.filterService.getVRSettings();
  }

  private getYear() {
    for (let i = this.today.year; i > 2018; i--) {
      this.yearOptions.push(new SelectItem(i.toString(), i));
    }
  }

  private OnGenerate() {
    const model = this.createModel();

    if (!this.isValid) {
      return;
    }

    this.filterService.checkDatabaseAvailable().subscribe(isAvailable => {
      if (isAvailable) {
        this.generated.emit(model);
      } else {
        this.toastService.activate('Data is being processed.... Please try again after 10 minutes!');
      }
    });
  }

  private createModel(): FilterModel {
    const model: FilterModel = {
      year: this.yearSelected.Value,
      custLevel: this.levelSelected.Value,
      username: this.isEnableSearchExactly ? this.username : '',
      isDownline: this.isSearchDownlines,
      currencies: this.selectedCurrencies.length === this.currencies.length ?
      'All' :
       this.selectedCurrencies.join(','),
      isAppliedVR: this.betTypeSelected.Value,
      pageSize: 200,
      pageIndex: 1,
      sports: this.selectedSports.length === this.sports.length ?
      'All' :
      this.selectedSports.join(','),
      leagueIds: this.selectedleagueGroups.length === this.leagueGroups.NumberItems ?
      'All' :
       this.selectedleagueGroups.join(','),
      divisions: this.selectedSites.length === this.sites.length ?
      'All' :
       this.selectedSites.join(','),
      accountStatus: this.selectedAccountStatus.length === this.accountStatus.length ?
      'All' :
      this.selectedAccountStatus.join(','),
      vrSettings: this.selectedVRSettings.length === this.vrSettings.length ?
      'All' :
      this.selectedVRSettings.join(','),
      isPageChanged: false,
      liveStatus: this.selectedLiveStatus.length === this.liveStatus.length ?
      'All' :
      this.selectedLiveStatus.join(','),
      betFromIds: this.selectedBetFroms.length === this.betFroms.length ?
      'All' :
      this.selectedBetFroms.join(','),
      permissionProfileId: CONFIG.permission.permissionProfileId
    };

    return model;
  }

  private get isValid(): boolean {
    if (this.selectedSports.length === 0) {
      this.toastService.activate(`Please select at least one sport!`);
      return false;
    }

    if (this.selectedCurrencies.length === 0) {
      this.toastService.activate(`Please select at least one currency!`);
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

    if (this.selectedAccountStatus.length === 0) {
      this.toastService.activate(`Please select at least one Account Status!`);
      return false;
    }

    if (this.selectedVRSettings.length === 0) {
      this.toastService.activate(`Please select at least one VR Setting!`);
      return false;
    }

    if (this.selectedLiveStatus.length === 0) {
      this.toastService.activate(`Please select at least one Status!`);
      return false;
    }

    if (this.selectedBetFroms.length === 0) {
      this.toastService.activate(`Please select at least one Betfrom!`);
      return false;
    }

    return true;
  }

  private getLiveStatus() {
    this.liveStatus = this.filterService.getLiveStatus();
  }

  private getBetFroms() {
    this.filterService.getBetFrom().subscribe(betFroms => this.betFroms = betFroms);
  }
}

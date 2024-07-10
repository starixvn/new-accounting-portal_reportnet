import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { DateFormat, DateHelper } from 'nap-components/date'
import * as moment from "moment";
import { CONFIG } from '../common/date-config';
import { TreeviewConfig } from 'ngx-treeview';
import { FilterModel, LeagueModel } from './filter.model';
import { TypeaheadQuery } from 'nap-components/dropdown-typeahead';
import { FilterService } from './filter.service';
import { SelectItem } from 'nap-components/dropdown-select';
import * as _ from 'lodash';
import { ToastService } from 'nap-components/toast';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {
  @Output() generated = new EventEmitter<FilterModel>();

  sport: string = '1';
  sportSelectItems: SelectItem[] = [];
  minStartDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today).subtract(29, 'day'));;
  maxEndDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  startDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  endDate: NgbDateStruct = DateHelper.convertMomentToDateStruct(moment.utc(CONFIG.date.today));
  leagueGroups: LeagueModel[] = [];
  leagueGroupTreeviewConfig = TreeviewConfig.create({
    hasFilter: true,
    hasAllCheckBox: true,
    hasCollapseExpand: false,
    maxHeight: 500
  });
  selectedLeagueGroups: number[] = [];
  leagueName = '';
  leagueNamePlaceholder = 'All';
  leagueNameTypeaheadQuery = this.buildLeagueNameTypeaheadQuery();
  
  constructor(private readonly filterService: FilterService,
    private readonly toastService: ToastService) { }

  ngOnInit(): void {
    this.sportSelectItems = this.filterService.getSportSelectItems();
    this.filterService.queryLeagueSuggestion(this.sport, '').subscribe(data => this.leagueGroups = data);
  }

  onSportChange(): void {
    this.filterService.queryLeagueSuggestion(this.sport, '').subscribe(data => this.leagueGroups = data);
  }

  onGenerateClick() {
    if (this.selectedLeagueGroups.join(',').length === 0) {
      this.toastService.activate(`Please select at least one League Group!`);
      return;
    } else {
      const model: FilterModel = {
        fromDate: DateHelper.formatMomentDate(DateHelper.convertDateStructToMoment(this.startDate), DateFormat.Date),
        toDate: DateHelper.formatMomentDate(DateHelper.convertDateStructToMoment(this.endDate), DateFormat.Date),
        leagueGroupIds: this.selectedLeagueGroups.join(','),
        sportId: Number(this.sport),
        leagueName: this.leagueName == '' ? undefined : this.leagueName
      };
      this.generated.emit(model);
    }
  }

  private buildLeagueNameTypeaheadQuery(): TypeaheadQuery {
    return new TypeaheadQuery(term => {
      let searchText = term.replace(/,/g, '');
      let words = searchText.split(' ');
      words = words.filter(word => word.trim() !== '');
      searchText = words.join(',');
      return this.filterService.queryLeagueSuggestion(this.sport, searchText)
        .map(list => {
          list = list.filter(x => x.LeagueId !== -1);
          list = _.orderBy(list, ['LengthOfLeagueName', 'LeagueName'], ['asc', 'asc']);
          return _.take(list, 20).map(item => item.LeagueName);
        });
    });
  }
}

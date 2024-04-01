import { Injectable } from '@angular/core';
import { SelectItem } from 'nap-components/dropdown-select';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';
import { FilterType } from '../common/model';
import { FilterGroupItemModel, FilterItemModel, LeagueModel, TicketStatus, TimePeriod, SiteModel, ExchageRateValues } from './filter.model';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  private readonly url = 'api/cash-out-performance/';

  constructor(private readonly httpService: HttpService) { }

  getSports(): Observable<FilterGroupItemModel[]> {
    const url = this.url + 'sports';
    return this.httpService.post<FilterGroupItemModel[]>(url);
  }

  getLicensees(): Observable<SiteModel[]> {
    const url = this.url + 'licensees';
    return this.httpService.post<SiteModel[]>(url);
  }

  getTimeSelectItems(): SelectItem[] {
    return [
        { text: 'Yesterday', value: TimePeriod.Yesterday },
        { text: 'Current Week', value: TimePeriod.CurrentWeek },
        { text: 'Last Week', value: TimePeriod.LastWeek },
        { text: 'Current Month', value: TimePeriod.CurrentMonth },
        { text: 'Last Month', value: TimePeriod.LastMonth },
        { text: 'Since Last Month', value: TimePeriod.SinceLastMonth },
        { text: 'Specific Date', value: TimePeriod.SpecificDate },
        { text: 'Last 3 Months', value: TimePeriod.LastThreeMonths }
    ];
   }

   getStatusSelectItems(): SelectItem[] {
    return [
        { text: 'All', value:  TicketStatus.All},
        { text: 'Live', value: TicketStatus.Live },
        { text: 'Non-Live', value: TicketStatus.NonLive }
    ];
   }

   getExchangeRateSelectItems(): SelectItem[] {
    return [
        { text: 'System Rate', value: ExchageRateValues.SystemRate },
        { text: 'Daily Oanda Rate', value: ExchageRateValues.OandaRate}
    ];
   }

   getGlobalShowTimeFilterItems(): FilterItemModel[] {
    return [
        { Id: 1, Name: '12:00 - 12:59'},
        { Id: 2, Name: '13:00 - 13:59'},
        { Id: 3, Name: '14:00 - 14:59'},
        { Id: 4, Name: '15:00 - 15:59'},
        { Id: 5, Name: '16:00 - 16:59'},
        { Id: 6, Name: '17:00 - 17:59'},
        { Id: 7, Name: '18:00 - 18:59'},
        { Id: 8, Name: '19:00 - 19:59'},
        { Id: 9, Name: '20:00 - 20:59'},
        { Id: 10, Name: '21:00 - 21:59'},
        { Id: 11, Name: '22:00 - 22:59'},
        { Id: 12, Name: '23:00 - 23:59'},
        { Id: 13, Name: '00:00 - 00:59'},
        { Id: 14, Name: '01:00 - 01:59'},
        { Id: 15, Name: '02:00 - 02:59'},
        { Id: 16, Name: '03:00 - 03:59'},
        { Id: 17, Name: '04:00 - 04:59'},
        { Id: 18, Name: '05:00 - 05:59'},
        { Id: 19, Name: '06:00 - 06:59'},
        { Id: 20, Name: '07:00 - 07:59'},
        { Id: 21, Name: '08:00 - 08:59'},
        { Id: 22, Name: '09:00 - 09:59'},
        { Id: 23, Name: '10:00 - 10:59'},
        { Id: 24, Name: '11:00 - 11:59'},
    ];
   }

   queryLeagueSuggestion(sportId: string, searchText: string, searchExactly: boolean): Observable<LeagueModel[]> {
    const url = `${this.url}/leagues`;
    return this.httpService.post<LeagueModel[]>(url, {
      sportId,
      searchText,
      searchExactly
    });
  }

  getLeagueGroups(): Observable<FilterItemModel[]> {
    const url = this.url + 'leagueGroups';
    return this.httpService.post<FilterItemModel[]>(url);
  }

  showFullColumn(): Observable<boolean> {
    const url = this.url + 'common/check-full-view';
    return this.httpService.post<boolean>(url);
  }

  getFilterByOptions(): SelectItem[] {
    return [{
        text: 'Settled Date',
        value: FilterType.SettledDate
    }, {
        text: 'Cashed-out Date',
        value: FilterType.CashedOutDate
    }];
  }
}

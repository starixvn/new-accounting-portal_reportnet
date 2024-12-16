import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { CommonItem, Currency, Site, Sport, BetFrom, LeagueGroupModel } from './filter.model';
import { Observable } from 'rxjs';

@Injectable()
export class FilterService {

  private url = 'api/sma-win-loss/filter';
  constructor(
    private httpService: HttpService
  ) { }

  getCurrency(): Observable<Currency[]> {
    const url = `${this.url}/currency`;
    return this.httpService.post<Currency[]>(url);
  }

  getSite(): Observable<Site[]> {
    const url = `${this.url}/site`;
    return this.httpService.post<Site[]>(url);
  }

  getSport(): Observable<Sport[]> {
    const url = `${this.url}/sport`;
    return this.httpService.post<Sport[]>(url);
  }

  getLeagueGroup(): Observable<LeagueGroupModel> {
    const url = `${this.url}/league-group`;
    return this.httpService.post<LeagueGroupModel>(url);
  }

  checkDatabaseAvailable(): Observable<boolean> {
    const url = `${this.url}/available-database`;
    return this.httpService.post<boolean>(url);
  }

  getAvailableDateData(): Observable<string> {
    const url = `${this.url}/available-date-data`;
    return this.httpService.post<string>(url);
  }

  getAccountStatus(): CommonItem[] {
    return [
      { Text: 'Open', Value: '1' },
      { Text: 'Closed', Value: '2' },
      { Text: 'Suspended', Value: '3' },
      { Text: 'Disabled', Value: '4' }
    ];
  }

  getVRSettings(): CommonItem[] {
    return [
      { Text: '+200', Value: '200' },
      { Text: '+100', Value: '100' },
      { Text: '+50', Value: '50' },
      { Text: '+5', Value: '5' },
      { Text: '0', Value: '0' },
      { Text: '-50', Value: '-50' },
      { Text: '-60', Value: '-60' },
      { Text: '-70', Value: '-70' },
      { Text: '-80', Value: '-80' },
      { Text: '-85', Value: '-85' },
      { Text: '-88', Value: '-88' },
      { Text: '-90', Value: '-90' },
      { Text: '-92', Value: '-92' },
      { Text: 'Other +VR', Value: 'Other+VR' },
      { Text: 'Other -VR', Value: 'Other-VR' },
      { Text: 'Disabled', Value: 'Disabled' }
    ];
  }

  getBetFrom(): Observable<BetFrom[]> {
    const url = `${this.url}/bet-from`;
    return this.httpService.post<BetFrom[]>(url);
  }

  getLiveStatus(): CommonItem[] {
    return [
      { Text: 'Live', Value: '1' },
      { Text: 'Non-Live', Value: '0' },
      { Text: 'Mix Live & Non-Live', Value: '2' }
    ];
  }
}

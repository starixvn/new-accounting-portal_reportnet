import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { TreeviewItem } from 'ngx-treeview';
import { HttpService, HttpOptions, DefaultHttpOptions } from 'nap-components/http';
import { SelectItem } from 'nap-components/dropdown-select';
import { FilterModel, Licensee, Sport, TimePeriod } from './filter.model';

@Injectable()
export class FilterService {
  constructor(
    private httpService: HttpService
  ) { }

  getLicensee(): Observable<TreeviewItem[]> {
    const url = 'api/exchange/filter/licensee';
    return this.httpService.post<Licensee[]>(url).map(result => {
      return this.buildLicenseeTreeViewItems(result);
    });
  }

  getSport(): Observable<TreeviewItem[]> {
    const url = 'api/exchange/filter/sport';
    return this.httpService.post<Sport[]>(url).map(result => {
     return this.buildSportTreeViewItems(result);
    });
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
        { text: 'Last 3 Months', value: TimePeriod.LastThreeMonths },
        { text: 'Since Last Year', value: TimePeriod.SinceLastYear }
    ];
   }

   private buildLicenseeTreeViewItems(licensees: Licensee[]): TreeviewItem[] {
    const licenseeTreeviewItems: TreeviewItem[] = [];
    licensees.forEach(licensee => {
      const treeViewItem = new TreeviewItem({
        text: licensee.LicenseeName,
        value: licensee.LicenseeId.toString()
      });

      licenseeTreeviewItems.push(treeViewItem);
    });

    return licenseeTreeviewItems;
   }

   private buildSportTreeViewItems(sports: Sport[]): TreeviewItem[] {
    const sportTreeviewItems: TreeviewItem[] = [];
    sports.forEach(sport => {
      const treeViewItem = new TreeviewItem({
        text: sport.SportName,
        value: sport.SportType.toString()
      });

      sportTreeviewItems.push(treeViewItem);
    });

    return sportTreeviewItems;
   }
}

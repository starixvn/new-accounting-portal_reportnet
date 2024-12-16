import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Observable } from 'rxjs-compat/Observable';
import { Grid } from '../grid.model';
import { GridService } from '../grid.service';
import { UsernameDataModel } from './username-grid.model';
import { UsernameGridService } from './username-grid.service';

@Component({
  selector: 'app-username-grid',
  templateUrl: './username-grid.component.html',
  styleUrls: ['./username-grid.component.scss']
})
export class UsernameGridComponent implements Grid {
  data: UsernameDataModel;
  sortColumnName = '';
  sortDirection = '';

  constructor(
    private usernameGridService: UsernameGridService,
    private readonly gridService: GridService
  ) { }

  query() {
        return new Observable(observer => {
          this.usernameGridService.query().subscribe(data => {
              this.data = data;
              observer.complete();
          });
      });
    }

    get hasData(): boolean {
        return !_.isNil(this.data) && this.data.Entities.length > 0;
    }

    get title(): string {
        return this.gridService.usernameTitle;
    }

    get isShowFullColumn(): boolean {
      return this.gridService.filterModel.isShowFullColumn;
    }

    sort(sortColumnName: string) {
      if (this.sortColumnName !== sortColumnName) {
        this.sortDirection = '';
      }

      this.sortColumnName = sortColumnName;
      this.sortDirection = this.sortDirection !== 'asc' ? 'asc' : 'desc';
      this.data.Entities = _.orderBy(this.data.Entities, this.sortColumnName, this.sortDirection !== 'asc' ? 'asc' : 'desc');
    }

}

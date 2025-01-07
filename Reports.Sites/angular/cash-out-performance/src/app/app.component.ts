import { Component } from '@angular/core';
import { FilterModel } from './filter/filter.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'cash-out-performance';
  filterModel: FilterModel;

  reset() {
    this.filterModel = undefined;
  }

  generate(filterModel: FilterModel) {
    this.filterModel = filterModel;
  }
}

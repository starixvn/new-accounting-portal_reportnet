import { Component } from '@angular/core';
import { FilterModel } from './filter/filter.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  filterModel!: FilterModel;

  generate(filterModel: FilterModel) {
    this.filterModel = filterModel;
  }
}

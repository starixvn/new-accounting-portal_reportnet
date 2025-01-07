import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TreeviewModule } from 'ngx-treeview';
import { NumericTextboxModule } from 'ngx-numeric-textbox';
import { DropdownTypeaheadModule } from 'nap-components/dropdown-typeahead';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { HttpModule } from 'nap-components/http';
import { DateModule } from 'nap-components/date';
import { NumberFormatModule } from 'nap-components/number-format';
import { LegendModule, LegendService } from 'nap-components/legend';
import { FilterComponent } from './filter.component';
import { FilterService } from './filter.service';
import { FormsModule } from '@angular/forms';
import { FilterItemTreeviewPipe } from './filter-item-treeview.pipe';
import { CommonModule } from '@angular/common';
import { PeriodTypeComponent } from './period-type/period-type.component';
import { PeriodTypeModule } from './period-type/period-type.module';
import { SiteTreeviewPipe } from './site-treeview.pipe';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    DateModule,
    NumericTextboxModule,
    NumberFormatModule,
    LegendModule.forRoot(),
    TreeviewModule.forRoot(),
    DropdownTypeaheadModule,
    DropdownSelectModule,
    PeriodTypeModule
  ],
  declarations: [
    FilterComponent,
    FilterItemTreeviewPipe,
    SiteTreeviewPipe
  ],
  providers: [
    FilterService
  ],
  exports: [
    FilterComponent
  ]
})
export class FilterModule {
  constructor(
    private readonly legendService: LegendService
  ) {
    legendService.resolveUrl = (id) => {
      return `https://nap.oneops.net/Reports-Legend-API/api/legend/${id}`;
    };
  }
}

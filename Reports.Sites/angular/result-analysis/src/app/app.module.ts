import { APP_INITIALIZER, Injectable, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { FilterItemTreeviewPipe } from './filter/filter-item-treeview.pipe';
import { HttpModule } from 'nap-components/http';
import { SpinnerModule } from 'nap-components/spinner';
import { ToastModule } from 'nap-components/toast';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { AuthenticationModule } from 'nap-components/authentication';
import { CustomNavbarService } from './custom-navbar-service';
import { CONFIG, DateConfig } from './common/date-config';
import * as moment from 'moment';
import { AppService } from './app.service';
import { FormsModule } from '@angular/forms';
import { TreeviewModule } from 'ngx-treeview';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { DropdownTypeaheadModule } from 'nap-components/dropdown-typeahead';
import { DateModule } from 'nap-components/date';
import { CommonModule } from '@angular/common';
import { GridComponent } from './grid/grid.component';
import { NumberFormatModule } from 'nap-components/number-format';
import { SummaryGridComponent } from './grid/summary-grid/summary-grid.component';
import { ItemGridComponent } from './grid/item-grid/item-grid.component';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { NgxPaginationModule } from 'ngx-pagination';
import { PaginationModule, PaginationConfig } from 'nap-components/pagination';
import { ItemQuarterGridComponent } from './grid/item-quarter-grid/item-quarter-grid.component';

declare module "@angular/core" {
  interface ModuleWithProviders<T = any> {
    ngModule: Type<T>;
    providers?: Provider[];
  }
}

@Injectable()
export class CustomPaginationConfig extends PaginationConfig {
    pageSizes = [1000, 2000, 3000, 4000, 5000];
    itemsPerPage = 1000;
}

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
    FilterItemTreeviewPipe,
    GridComponent,
    SummaryGridComponent,
    ItemGridComponent,
    ItemQuarterGridComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    SpinnerModule,
    ToastModule,
    NavbarModule,
    AuthenticationModule,
    CommonModule,
    FormsModule,
    TreeviewModule.forRoot(),
    DropdownSelectModule,
    DropdownTypeaheadModule,
    DateModule,
    NumberFormatModule,
    BreadcrumbModule,
    NgxPaginationModule,
    PaginationModule.forRoot()
  ],
  providers: [
    { provide: NavbarService, useClass: CustomNavbarService },
    { provide: PaginationConfig, useClass: CustomPaginationConfig },
    AppService,
    {
      provide: APP_INITIALIZER,
      useFactory: initConfiguration,
      deps: [AppService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function initConfiguration(service: AppService): Function {
  return () => new Promise(resolve => {
    service.getToday().subscribe(model => {
      const today = moment.utc(model);
      CONFIG.date = new DateConfig(today);
      resolve(true);
    });
  });
}

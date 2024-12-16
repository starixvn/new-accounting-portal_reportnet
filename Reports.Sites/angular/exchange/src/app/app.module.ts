import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { DateModule } from 'nap-components/date';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { TreeviewModule } from 'ngx-treeview';

import { ToastService, ToastModule } from 'nap-components/toast';
import { SpinnerService, SpinnerModule } from 'nap-components/spinner';
import { NumberFormatModule } from 'nap-components/number-format';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { PaginationModule, PaginationConfig } from 'nap-components/pagination';
import { CustomPaginationConfig, DateConfig, CONFIG } from './common/config';
import { CustomNavbarService } from './custom-navbar-service';
import { AppService } from './app.service';
import * as moment from 'moment';
import { GridDrillDownComponent } from './grid/grid.component';
import { LicenseeComponent } from './grid/licensee/licensee.component';
import { SportComponent } from './grid/sport/sport.component';
import { EventComponent } from './grid/event/event.component';

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
    GridDrillDownComponent,
    LicenseeComponent,
    SportComponent,
    EventComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    TreeviewModule.forRoot(),
    DropdownSelectModule,
    DateModule,
    PaginationModule.forRoot(),
    NumberFormatModule,
    BreadcrumbModule,
    SpinnerModule.forRoot(),
    ToastModule.forRoot(),
    NavbarModule.forRoot(),
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

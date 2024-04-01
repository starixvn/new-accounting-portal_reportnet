import { AuthenticationModule } from 'nap-components/authentication';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { GridComponent } from './grid/grid.component';
import { TreeviewModule } from 'ngx-treeview';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { DateModule } from 'nap-components/date';
import { NavbarModule, NavbarService } from 'nap-components/navbar';

import { ToastModule } from 'nap-components/toast';
import { SpinnerModule } from 'nap-components/spinner';
import { NumberFormatModule } from 'nap-components/number-format';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { DropdownTypeaheadModule } from 'nap-components/dropdown-typeahead';
import { PaginationModule, PaginationConfig } from 'nap-components/pagination';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppService } from './app.service';
import * as moment from 'moment';
import { CONFIG, DateConfig } from './common/date-config';
import { CustomNavbarService } from './custom-navbar-service';
import { CustomPaginationConfig } from './common/paging-config';
import { FormsModule } from '@angular/forms';
import { SiteTreeviewPipe } from './filter/site-treeview.pipe';
import { LeagueGroupTreeviewPipe } from './filter/league-group-treeview.pipe';
import { SportTreeviewPipe } from './filter/sport-treeview.pipe';
import { forkJoin } from 'rxjs';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
    GridComponent,
    SiteTreeviewPipe,
    LeagueGroupTreeviewPipe,
    SportTreeviewPipe
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
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
    DropdownTypeaheadModule,
    AuthenticationModule
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
    forkJoin([service.getToday(), service.getBalanceUpDate()]).subscribe(data => {
      const today = moment.utc(data[0]);
      const balanceUpDate = moment.utc(data[1]);
      CONFIG.date = new DateConfig(today, balanceUpDate);
      resolve(true);
    });
  });
}

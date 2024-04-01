import { BetFromTreeviewPipe } from './filter/bet-from-treeview.pipe';
import { CommonModule } from '@angular/common';
import { LeagueGroupTreeviewPipe } from './filter/league-group-treeview.pipe';
import { DropdownTypeaheadModule } from 'nap-components/dropdown-typeahead';
import { SportTreeviewPipe } from './filter/sport-treeview.pipe';
import { SiteTreeviewPipe } from './filter/site-treeview.pipe';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { AuthenticationModule } from 'nap-components/authentication';
import { NumberFormatModule } from 'nap-components/number-format';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DateModule } from 'nap-components/date';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { SpinnerModule } from 'nap-components/spinner';
import { HttpModule } from 'nap-components/http/http.module';
import { CustomNavbarService } from './custom-navbar-service';
import { AppService } from './app.service';
import { CONFIG, DateConfig, CustomPaginationConfig, Permission } from './common/config';
import * as moment from 'moment';
import { TreeviewModule } from 'ngx-treeview';
import { CurrencyTreeviewPipe } from './filter/currency-treeview.pipe';
import { SummaryGridComponent } from './summary-grid/summary-grid.component';
import { PaginationModule, PaginationConfig } from 'nap-components/pagination';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { VirtualRateGridComponent } from './virtual-rate-grid/virtual-rate-grid.component';
import { ToastModule } from 'nap-components/toast';
import { CommonItemTreeviewPipe } from './filter/common-item-treeview.pipe';

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
    CurrencyTreeviewPipe,
    SummaryGridComponent,
    SiteTreeviewPipe,
    SportTreeviewPipe,
    VirtualRateGridComponent,
    LeagueGroupTreeviewPipe,
    CommonItemTreeviewPipe,
    BetFromTreeviewPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule,
    DateModule,
    HttpModule.forRoot(),
    NavbarModule.forRoot(),
    SpinnerModule.forRoot(),
    NumberFormatModule,
    TreeviewModule.forRoot(),
    DropdownSelectModule,
    AuthenticationModule,
    PaginationModule.forRoot(),
    BreadcrumbModule,
    DropdownTypeaheadModule,
    CommonModule,
    ToastModule
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
  bootstrap: [AppComponent],
  entryComponents: [VirtualRateGridComponent]
})
export class AppModule { }

export function initConfiguration(service: AppService): Function {
  return () => new Promise(resolve => {
    service.getToday().subscribe(model => {
      const today = moment.utc(model);
      CONFIG.date = new DateConfig(today);
      resolve(true);
    });

    service.getPermissionProfileId().subscribe(permission => {
      CONFIG.permission = new Permission(permission);
      resolve(true);
    });
  });
}

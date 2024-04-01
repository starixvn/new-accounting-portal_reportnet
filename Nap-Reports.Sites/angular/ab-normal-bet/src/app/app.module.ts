import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import * as moment from 'moment';

import { AppComponent } from './app.component';
import { LicenseeGridComponent } from './licensee-grid/licensee-grid.component';
import { MemberGridComponent } from './member-grid/member-grid.component';
import { FilterModule } from './filter/filter.module';
import { AppService } from './app.service';
import { CONFIG, DateConfig } from './common/date-config';
import { HttpModule } from 'nap-components/http';
import { SpinnerModule } from 'nap-components/spinner';
import { ToastModule } from 'nap-components/toast';
import { AuthenticationModule } from 'nap-components/authentication';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { GridComponent } from './grid/grid.component';
import { PaginationModule } from 'nap-components/pagination';
import { NumberFormatModule } from 'nap-components/number-format';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { CustomNavbarService } from './custom-navbar-service';


@NgModule({
  declarations: [
    AppComponent,
    LicenseeGridComponent,
    MemberGridComponent,
    GridComponent
  ],
  imports: [
    BrowserModule,
    HttpModule.forRoot(),
    SpinnerModule.forRoot(),
    ToastModule.forRoot(),
    NavbarModule.forRoot(),
    AuthenticationModule,
    PaginationModule.forRoot(),
    NumberFormatModule,
    BreadcrumbModule,
    FilterModule
  ],
  providers: [
  { provide: NavbarService, useClass: CustomNavbarService },
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

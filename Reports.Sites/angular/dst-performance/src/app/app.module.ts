import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { CustomNavbarService } from './custom-navbar-service';
import * as moment from 'moment';
import { AppService } from './app.service';
import { CONFIG, DateConfig } from './common/date-config';
import { HttpModule } from 'nap-components/http';
import { SpinnerModule } from 'nap-components/spinner';
import { ToastModule } from 'nap-components/toast';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DateModule } from 'nap-components/date';
import { AuthenticationModule } from 'nap-components/authentication';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { GridComponent } from './grid/grid.component';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { SportGridComponent } from './grid/sport-grid/sport-grid.component';
import { BetTypeGridComponent } from './grid/bettype-grid/bettype-grid.component';
import { DecimalFormatDirective } from './directive/decimal-format.directive';
import { LeagueGridComponent } from './grid/league-grid/league-grid.component';
import { MatchGridComponent } from './grid/match-grid/match-grid.component';

declare module "@angular/core" {
  interface ModuleWithProviders<T = any> {
    ngModule: Type<T>;
    providers?: Provider[];
  }
}

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
    GridComponent,
    SportGridComponent,
    BetTypeGridComponent,
    LeagueGridComponent,
    MatchGridComponent,
    DecimalFormatDirective
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
    DateModule,
    DropdownSelectModule,
    BreadcrumbModule
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

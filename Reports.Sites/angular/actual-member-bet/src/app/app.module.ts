import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { DateModule } from 'nap-components/date';
import { HttpModule } from 'nap-components/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { SpinnerModule } from 'nap-components/spinner';
import { LegendModule, LegendService } from 'nap-components/legend';
import { CONFIG, DateConfig } from './common/config';
import { AppService } from './app.service';
import * as moment from 'moment';
import { CustomNavbarService } from './custom-navbar-service';
import { GridModule } from './grid/grid.module';

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    DateModule,
    HttpModule.forRoot(),
    NavbarModule.forRoot(),
    SpinnerModule.forRoot(),
    LegendModule.forRoot(),
    GridModule
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

export class AppModule {
  constructor(
    private legendService: LegendService
  ) {
    legendService.resolveUrl = (id) => {
      return `https://reports.oneops.net/Reports-Legend-API/api/legend/${id}`;
    };
  }
}

export function initConfiguration(service: AppService): Function {
  return () => new Promise(resolve => {
    service.getToday().subscribe(model => {
      const today = moment.utc(model);
      CONFIG.date = new DateConfig(today);
      resolve(true);
    });
  });
}

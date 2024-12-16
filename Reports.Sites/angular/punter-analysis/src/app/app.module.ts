import * as moment from 'moment';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpModule } from 'nap-components/http';
import { HttpService } from 'nap-components/http';
import { ExceptionService } from 'nap-components/http';
import { DateModule } from 'nap-components/date';
import { LegendModule, LegendService } from 'nap-components/legend';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { PaginationConfig, PaginationService } from 'nap-components/pagination';
import { ToastService, ToastModule } from 'nap-components/toast';
import { SpinnerService, SpinnerModule } from 'nap-components/spinner';
import { FilterModule } from './filter/filter.module';
import { GridModule } from './grid/grid.module';
import { PerformanceModule } from './performance/performance.module';
import { AppComponent } from './app.component';
import { CustomNavbarService } from './custom-navbar-service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    DateModule,
    HttpModule.forRoot(),
    NavbarModule.forRoot(),
    SpinnerModule.forRoot(),
    LegendModule.forRoot(),
    ToastModule,
    FilterModule,
    GridModule,
    PerformanceModule
  ],
  providers: [
    { provide: NavbarService, useClass: CustomNavbarService },
    PaginationConfig,
    PaginationService,
    HttpService,
    ExceptionService,
    ToastService,
    SpinnerService
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

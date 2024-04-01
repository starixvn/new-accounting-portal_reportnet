import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { DateModule } from 'nap-components/date';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { ToastModule } from 'nap-components/toast';
import { SpinnerModule } from 'nap-components/spinner';
import { NumberFormatModule } from 'nap-components/number-format';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { LegendModule, LegendService } from 'nap-components/legend';
import { PaginationModule, PaginationConfig } from 'nap-components/pagination';
import { UpdatedUsernameComponent } from './updated-username/updated-username.component';
import { UpdatedCreditComponent } from './updated-credit/updated-credit.component';
import { GridComponent } from './grid/grid.component';
import { CustomPaginationConfig } from './common/config';
import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { CustomNavbarService } from './custom-navbar-service';

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
    UpdatedUsernameComponent,
    UpdatedCreditComponent,
    GridComponent
  ],
  imports: [
    BrowserModule,
    DropdownSelectModule,
    DateModule,
    NgbModule,
    LegendModule.forRoot(),
    PaginationModule.forRoot(),
    NumberFormatModule,
    BreadcrumbModule,
    SpinnerModule.forRoot(),
    ToastModule.forRoot(),
    NavbarModule.forRoot()
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: NavbarService, useClass: CustomNavbarService },
    { provide: PaginationConfig, useClass: CustomPaginationConfig }
  ]
})
export class AppModule {
  constructor(
    legendService: LegendService
  ) {
    legendService.resolveUrl = (id) => {
      return `https://reports.oneops.net/Reports-Legend-API/api/legend/${id}`;
    };
  }
}

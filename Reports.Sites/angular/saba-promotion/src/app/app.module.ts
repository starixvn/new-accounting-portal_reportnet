import { APP_INITIALIZER, Injectable, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { HttpModule } from 'nap-components/http';
import { SpinnerModule } from 'nap-components/spinner';
import { ToastModule } from 'nap-components/toast';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DateModule } from 'nap-components/date';
import { AuthenticationModule } from 'nap-components/authentication';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { AppService } from './app.service';
import { CONFIG, DateConfig } from './common/date-config';
import * as moment from 'moment';
import { CustomNavbarService } from './custom-navbar-service';
import { TreeviewModule } from 'ngx-treeview';
import { FilterItemTreeviewPipe } from './filter/filter-item-treeview.pipe';
import { GridComponent } from './grid/grid.component';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { NgxPaginationModule } from 'ngx-pagination';
import { PaginationModule, PaginationConfig } from 'nap-components/pagination';

@Injectable()
export class CustomPaginationConfig extends PaginationConfig {
    pageSizes = [100, 200, 300, 500];
    itemsPerPage = 100;
}

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
    FilterItemTreeviewPipe,
    GridComponent
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
    TreeviewModule.forRoot(),
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

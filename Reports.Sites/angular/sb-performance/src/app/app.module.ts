import { AuthenticationModule } from 'nap-components/authentication';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppComponent } from './app.component';
import { FilterComponent } from './filter/filter.component';
import { DateModule } from 'nap-components/date';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { SpinnerModule } from 'nap-components/spinner';
import { CONFIG, DateConfig } from './common/date-config';
import { AppService } from './app.service';
import * as moment from 'moment';
import { CustomNavbarService } from './custom-navbar-service';
import { HttpModule } from 'nap-components/http/http.module';
import { NumberFormatModule } from 'nap-components/number-format';
import { FormsModule } from '@angular/forms';
import { GridModule } from './grid/grid.module';

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent
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
    AuthenticationModule,
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

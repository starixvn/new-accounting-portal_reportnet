import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { FilterModule } from './filter/filter.module';
import { HttpModule } from 'nap-components/http';
import { SpinnerModule } from 'nap-components/spinner';
import { ToastModule } from 'nap-components/toast';
import { AuthenticationModule } from 'nap-components/authentication';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { AppService } from './app.service';
import { CustomNavbarService } from './custom-navbar-service';
import { CONFIG, DateConfig } from './common/date-config';
import * as moment from 'moment';
import { GridModule } from './grid/grid.module';

declare module "@angular/core" {
  interface ModuleWithProviders<T = any> {
    ngModule: Type<T>;
    providers?: Provider[];
  }
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    SpinnerModule,
    ToastModule,
    NavbarModule,
    AuthenticationModule,
    FilterModule,
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

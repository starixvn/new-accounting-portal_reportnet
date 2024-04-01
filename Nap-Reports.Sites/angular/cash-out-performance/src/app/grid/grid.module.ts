import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatchDetailGridComponent } from './match-detail-grid/match-detail-grid.component';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { SportGridComponent } from './sport-grid/sport-grid.component';
import { GridComponent } from './grid.component';
import { GridService } from './grid.service';
import { NumberFormatModule } from 'nap-components/number-format';
import { PaginationModule, PaginationService, PaginationConfig, } from 'nap-components/pagination';
import { CustomPaginationConfig } from '../common/config';
import { EventStatusGridComponent } from './event-status-grid/event-status-grid.component';
import { BettypeGridComponent } from './bettype-grid/bettype-grid.component';

@NgModule({
  declarations: [
    GridComponent,
    MatchDetailGridComponent,
    SportGridComponent,
    EventStatusGridComponent,
    BettypeGridComponent
  ],
  imports: [
    CommonModule,
    BreadcrumbModule,
    NumberFormatModule,
    PaginationModule
  ],
  exports: [
    GridComponent
  ],
  providers: [
    GridService,
    PaginationService,
    PaginationConfig,
    { provide: PaginationConfig, useClass: CustomPaginationConfig },
  ]
})
export class GridModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { GridComponent } from './grid.component';
import { SiteGridComponent } from './site-grid/site-grid.component';
import { GridService } from './grid.service';
import { SiteGridService } from './site-grid/site-grid.service';
import { NumberFormatModule } from 'nap-components/number-format';
import { LeagueGridComponent } from './league-grid/league-grid.component';
import { SportGridComponent } from './sport-grid/sport-grid.component';
import { EventGridComponent } from './event-grid/event-grid.component';
import { MatchGridComponent } from './match-grid/match-grid.component';
import { BettypeGridComponent } from './bettype-grid/bettype-grid.component';
import { MainComponent } from '../main/main.component';
import { MainItemComponent } from '../main-item/main-item.component';
import { UsernameGridComponent } from './username-grid/username-grid.component';

@NgModule({
  declarations: [
    GridComponent,
    MainComponent,
    MainItemComponent,
    SiteGridComponent,
    LeagueGridComponent,
    SportGridComponent,
    EventGridComponent,
    MatchGridComponent,
    BettypeGridComponent,
    UsernameGridComponent
  ],
  imports: [
    CommonModule,
    BreadcrumbModule,
    NumberFormatModule
  ],
  exports: [
    GridComponent
  ],
  providers: [
    GridService,
    SiteGridService
  ]
})
export class GridModule { }

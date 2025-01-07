import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TreeviewModule } from 'ngx-treeview';
import { PaginationModule } from 'nap-components/pagination';
import { NumberFormatModule } from 'nap-components/number-format';
import { GridComponent } from './grid.component';
import { GridService } from './grid.service';
import { SummaryGridComponent } from './summary-grid/summary-grid.component';
import { SummaryGridService } from './summary-grid/summary-grid.service';
import { BetCountGridComponent } from './bet-count-grid/bet-count-grid.component';
import { BetCountGridService } from './bet-count-grid/bet-count-grid.service';
import { AssociatedAccountGridComponent } from './associated-account-grid/associated-account-grid.component';
import { AssociatedAccountGridService } from './associated-account-grid/associated-account-grid.service';
import { SportTypeGridComponent } from './sport-type-grid/sport-type-grid.component';
import { SportTypeGridService } from './sport-type-grid/sport-type-grid.service';
import { BetTypeGridComponent } from './bet-type-grid/bet-type-grid.component';
import { BetTypeGridService } from './bet-type-grid/bet-type-grid.service';
import { LeagueGridComponent } from './league-grid/league-grid.component';
import { LeagueGridService } from './league-grid/league-grid.service';
import { BreadcrumbModule } from '../breadcrumb/breadcrumb.module';
import { CommonService } from './common.service';
import { GameGridComponent } from './game-grid/game-grid.component';
import { BetTypeGroupGridComponent } from './bet-type-group-grid/bet-type-group-grid.component';
import { GameBetTypeGridService } from './game-bet-type-grid/game-bet-type-grid.service';
import { BetTypeGroupGridService } from './bet-type-group-grid/bet-type-group-grid.service';
import { GameGridService } from './game-grid/game-grid.service';
import { GameBetTypeGridComponent } from './game-bet-type-grid/game-bet-type-grid.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        TreeviewModule.forRoot(),
        PaginationModule,
        NumberFormatModule,
        BreadcrumbModule
    ],
    declarations: [
        GridComponent,
        SummaryGridComponent,
        BetCountGridComponent,
        AssociatedAccountGridComponent,
        SportTypeGridComponent,
        BetTypeGridComponent,
        LeagueGridComponent,
        GameGridComponent,
        BetTypeGroupGridComponent,
        GameBetTypeGridComponent
    ],
    exports: [
        GridComponent
    ],
    providers: [
        GridService,
        SummaryGridService,
        BetCountGridService,
        AssociatedAccountGridService,
        SportTypeGridService,
        BetTypeGridService,
        LeagueGridService,
        CommonService,
        GameGridService,
        BetTypeGroupGridService,
        GameBetTypeGridService
    ]
})
export class GridModule { }

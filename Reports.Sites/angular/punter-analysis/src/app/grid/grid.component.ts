import {
    Component, Input, OnInit, OnChanges, ComponentFactoryResolver, Injector, ViewContainerRef,
    ComponentRef, ViewChild, SimpleChanges, OnDestroy, ComponentFactory, Renderer
} from '@angular/core';
import * as _ from 'lodash';
import { FilterModel } from '../filter/filter.model';
import { GridService } from './grid.service';
import { Grid, GridType } from './grid.model';
import { SummaryGridComponent } from './summary-grid/summary-grid.component';
import { BetCountGridComponent } from './bet-count-grid/bet-count-grid.component';
import { AssociatedAccountGridComponent } from './associated-account-grid/associated-account-grid.component';
import { SportTypeGridComponent } from './sport-type-grid/sport-type-grid.component';
import { BetTypeGridComponent } from './bet-type-grid/bet-type-grid.component';
import { LeagueGridComponent } from './league-grid/league-grid.component';
import { hasBreadcrumbFilter } from '../breadcrumb/breadcrumb.model';
import { BreadcrumbConfig } from '../breadcrumb/breadcrumb-config';
import { BetTypeGroupGridComponent } from './bet-type-group-grid/bet-type-group-grid.component';
import { GameGridComponent } from './game-grid/game-grid.component';
import { GameBetTypeGridComponent } from './game-bet-type-grid/game-bet-type-grid.component';

@Component({
    selector: 'nap-grid',
    templateUrl: './grid.component.html',
    styleUrls: ['./grid.component.scss'],
    entryComponents: [
        SummaryGridComponent,
        BetCountGridComponent,
        AssociatedAccountGridComponent,
        SportTypeGridComponent,
        BetTypeGridComponent,
        LeagueGridComponent,
        GameGridComponent,
        BetTypeGroupGridComponent,
        GameBetTypeGridComponent
    ]
})
export class GridComponent implements OnInit, OnChanges, OnDestroy {
    @Input() filterModel: FilterModel;
    @ViewChild('container', { read: ViewContainerRef }) container: ViewContainerRef;
    GridType = GridType;
    gridType = GridType.UserName;
    breadcrumbItems: string[];
    breadcrumbConfig: BreadcrumbConfig;
    private components: ComponentRef<Grid>[] = [];

    constructor(
        private componentFactoryResolver: ComponentFactoryResolver,
        private injector: Injector,
        private renderer: Renderer,
        private service: GridService
    ) { }

    ngOnInit() {
        this.service.generate = (gridType: GridType) => this.generate(gridType);
        this.service.goBack = (index: number) => this.goBack(index);
    }

    ngOnChanges(changes: SimpleChanges) {
        this.gridType = GridType.UserName;
        this.service.filterModel = this.filterModel;
        this.createGrid();
    }

    ngOnDestroy() {
        this.container.clear();
    }

    private generate(gridType: GridType) {
        this.gridType = gridType;
        this.createGrid();
    }

    private goBack(index: number) {
        while (this.components.length - 1 > index) {
            const component = this.components.pop();
            component.destroy();
        }
        this.service.activeGrid = this.components[this.components.length - 1].instance;
        this.afterGridChange();
    }

    private getComponentFactory(): ComponentFactory<Grid> {
        switch (this.gridType) {
            case GridType.UserName:
                return this.componentFactoryResolver.resolveComponentFactory(SummaryGridComponent);
            case GridType.BetCount:
                return this.componentFactoryResolver.resolveComponentFactory(BetCountGridComponent);
            case GridType.AssociatedAccount:
                return this.componentFactoryResolver.resolveComponentFactory(AssociatedAccountGridComponent);
            case GridType.SportType:
                return this.componentFactoryResolver.resolveComponentFactory(SportTypeGridComponent);
            case GridType.BetType:
                return this.componentFactoryResolver.resolveComponentFactory(BetTypeGridComponent);
            case GridType.League:
                return this.componentFactoryResolver.resolveComponentFactory(LeagueGridComponent);
            case GridType.Game:
                return this.componentFactoryResolver.resolveComponentFactory(GameGridComponent);
            case GridType.BetTypeGroup:
                return this.componentFactoryResolver.resolveComponentFactory(BetTypeGroupGridComponent);
            case GridType.GameBetType:
                return this.componentFactoryResolver.resolveComponentFactory(GameBetTypeGridComponent);
            default:
                throw new Error('Not implemented yet');
        }
    }

    private createGrid() {
        const factory = this.getComponentFactory();
        const component = this.container.createComponent(factory);
        this.components.push(component);
        const grid = component.instance;
        this.service.activeGrid = grid;
        grid.query().subscribe({
            complete: () => this.afterGridChange()
        });
    }

    private afterGridChange() {
        this.updateBreadcrumb();
        this.showCurrentAndHideLast();
    }

    private updateBreadcrumb() {
        this.breadcrumbItems = [];
        this.components.forEach(component => {
            const componentTitle = component.instance.title;
            this.breadcrumbItems.push(componentTitle);
        });
        this.breadcrumbConfig = {
            goBack: (index) => this.goBack(index)
        };
        const activeGrid = this.service.activeGrid;
        if (activeGrid.hasExcel) {
            this.breadcrumbConfig.exportExcel = () => activeGrid.exportExcel();
        }
        if (hasBreadcrumbFilter(activeGrid)) {
            this.breadcrumbConfig.filterColumns = activeGrid.getFilterColumns();
            this.breadcrumbConfig.filterCallback = (selectedColumns) => activeGrid.filterColumns(selectedColumns);
        }
    }

    private showCurrentAndHideLast() {
        const length = this.components.length;
        const currentComponent = this.components[length - 1];
        this.renderer.setElementAttribute(currentComponent.location.nativeElement, 'hidden', null);
        if (length > 1) {
            const lastComponent = this.components[length - 2];
            this.renderer.setElementAttribute(lastComponent.location.nativeElement, 'hidden', 'hidden');
        }
    }
}

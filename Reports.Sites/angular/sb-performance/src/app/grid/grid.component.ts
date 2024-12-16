import { Component, ComponentFactory, ComponentFactoryResolver, ComponentRef, Input,
  OnChanges,
  OnDestroy, OnInit, Renderer2, ViewChild, ViewContainerRef } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid, GridType } from './grid.model';
import { GridService } from './grid.service';
import * as _ from 'lodash';
import { LeagueGridComponent } from './league-grid/league-grid.component';
import { SportGridComponent } from './sport-grid/sport-grid.component';
import { EventGridComponent } from './event-grid/event-grid.component';
import { MatchGridComponent } from './match-grid/match-grid.component';
import { BettypeGridComponent } from './bettype-grid/bettype-grid.component';
import { MainComponent } from '../main/main.component';
import { SiteGridComponent } from './site-grid/site-grid.component';
import { UsernameGridComponent } from './username-grid/username-grid.component';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
  entryComponents: [
    MainComponent,
    SportGridComponent,
    LeagueGridComponent,
    EventGridComponent,
    MatchGridComponent,
    BettypeGridComponent,
    SiteGridComponent,
    UsernameGridComponent
  ]
})
export class GridComponent implements OnInit, OnChanges, OnDestroy {
  @Input() filterModel: FilterModel;
  @ViewChild('container', { read: ViewContainerRef }) container: ViewContainerRef;

  gridType = GridType.Main;
  breadcrumbItems: string[];
  private components: ComponentRef<Grid>[] = [];

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private renderer: Renderer2,
    private service: GridService
  ) { }

  ngOnInit() {
    this.service.generate = (gridType: GridType, queryModel: any) => this.generate(gridType, queryModel);
  }

  ngOnDestroy() {
    this.container.clear();
  }

  ngOnChanges() {
    this.breadcrumbItems = [];
    while (this.components.length) {
      const component = this.components.pop();
      component.destroy();
    }
    this.gridType = GridType.Main;
    this.service.filterModel = this.filterModel;
    this.service.queryModel = this.filterModel;
    this.createGridComponent();
  }

  private generate(gridType: GridType, model: any) {
    this.service.queryModel = _.assign(this.service.filterModel, model);
    this.gridType = gridType;
    this.createGridComponent();
  }

  private getComponentFactory(): ComponentFactory<Grid> {
    switch (this.gridType) {
        case GridType.Main:
            return this.componentFactoryResolver.resolveComponentFactory(MainComponent);
        case GridType.Sport:
            return this.componentFactoryResolver.resolveComponentFactory(SportGridComponent);
        case GridType.League:
            return this.componentFactoryResolver.resolveComponentFactory(LeagueGridComponent);
        case GridType.Event:
            return this.componentFactoryResolver.resolveComponentFactory(EventGridComponent);
        case GridType.Match:
            return this.componentFactoryResolver.resolveComponentFactory(MatchGridComponent);
        case GridType.BetType:
            return this.componentFactoryResolver.resolveComponentFactory(BettypeGridComponent);
        case GridType.Site:
            return this.componentFactoryResolver.resolveComponentFactory(SiteGridComponent);
        case GridType.Username:
            return this.componentFactoryResolver.resolveComponentFactory(UsernameGridComponent);
        default:
            throw new Error('Not implemented yet');
    }
  }

  private createGridComponent() {
    const factory = this.getComponentFactory();
    const component = this.container.createComponent(factory);
    this.components.push(component);

    const grid = component.instance;
    this.service.activeGrid = grid;
    grid.query().subscribe({
      complete: () => this.showGridAndBreadcrumb()
    });
  }

  private showGridAndBreadcrumb() {
    this.updateBreadcrumb();
    this.showOnlyLastGrid();
  }

  private updateBreadcrumb() {
    this.breadcrumbItems = [];
    this.components.forEach((component, index) => {
      const componentTitle = index === 0 ? this.service.mainTitle : component.instance.title;
      this.breadcrumbItems.push(componentTitle);
    });
  }

  private showOnlyLastGrid() {
    const length = this.components.length;
    const currentComponent = this.components[length - 1];
    this.renderer.removeAttribute(currentComponent.location.nativeElement, 'hidden');

    if (length > 1) {
      const lastComponent = this.components[length - 2];
      this.renderer.setAttribute(lastComponent.location.nativeElement, 'hidden', 'hidden');
    }
  }

  private destroyComponentsFrom(index: number) {
    while (this.components.length - 1 > index) {
      const component = this.components.pop();
      component.destroy();
    }

    this.service.activeGrid = this.components[this.components.length - 1].instance;
    this.showGridAndBreadcrumb();
  }

  onBreadcrumbItemClick(event: number) {
    this.destroyComponentsFrom(event);
    this.showGridAndBreadcrumb();
  }
}

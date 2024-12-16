import {
    Component, Input, OnInit, OnChanges, ComponentFactoryResolver, Injector, ViewContainerRef,
    ComponentRef, ViewChild, SimpleChanges, OnDestroy, ComponentFactory, Renderer
} from '@angular/core';
import * as _ from 'lodash';
import { FilterModel } from '../filter/filter.model';
import { FilterModelWrapper, PeriodType } from '../filter/filter.model.main';
import { PerformanceType, Performance } from './performance.model';
import { PerformanceService } from './performance.service';
import { SummaryPerformanceComponent } from './summary-performance/summary-performance.component';
import { SummaryPerformanceService } from './summary-performance/summary-performance.service';
import { PerformanceStatistic } from './summary-performance/summary-performance.model';
import { ProductPerformanceComponent } from './product-performance/product-performance.component';
import { BreadcrumbConfig } from '../breadcrumb/breadcrumb-config';

@Component({
    selector: 'nap-performance',
    templateUrl: './performance.component.html',
    styleUrls: ['./performance.component.scss'],
    entryComponents: [
        SummaryPerformanceComponent,
        ProductPerformanceComponent
    ]
})
export class PerformanceComponent implements OnInit, OnChanges, OnDestroy {
    hasData = false;
    statistic: PerformanceStatistic;
    @Input() filterModel: FilterModel;
    @ViewChild('container', { read: ViewContainerRef }) container: ViewContainerRef;
    performanceType = PerformanceType.Main;
    breadcrumbItems: string[];
    breadcrumbConfig: BreadcrumbConfig;
    private components: ComponentRef<Performance>[] = [];

    constructor(
        private componentFactoryResolver: ComponentFactoryResolver,
        private injector: Injector,
        private renderer: Renderer,
        private service: PerformanceService,
        private summaryPerformanceService: SummaryPerformanceService
    ) { }

    ngOnInit() {
        this.service.generate = (performanceType: PerformanceType) => this.generate(performanceType);
        this.service.goBack = (index: number) => this.goBack(index);
    }

    ngOnChanges(changes: SimpleChanges) {
        this.performanceType = PerformanceType.Main;
        this.service.filterModel = this.filterModel;
        this.summaryPerformanceService.query().subscribe(statistic => {
            this.statistic = statistic;
            this.service.summaryPerformanceStatistic = statistic;
            this.hasData = this.statistic.MonthStatistics.length !== 0;
            if (this.hasData) {
                this.createPerformance();
            } else {
                const filterModel = this.filterModel;
                const userName = filterModel.performance.userName;
                const dateString = FilterModelWrapper.getDateFromToString(PeriodType.MonthRange, filterModel.fromDate, filterModel.toDate);
                this.breadcrumbItems = [`Punter ${userName}'s Performance ${dateString}`];
            }
        });
    }

    ngOnDestroy() {
        this.container.clear();
    }

    private generate(performanceType: PerformanceType) {
        this.performanceType = performanceType;
        this.createPerformance();
    }

    private goBack(index: number) {
        while (this.components.length - 1 > index) {
            const component = this.components.pop();
            component.destroy();
        }
        this.service.activePerformance = this.components[this.components.length - 1].instance;
        this.service.performances = this.components.map(item => item.instance);
        this.afterPerformanceChange();
    }

    private getComponentFactory(): ComponentFactory<Performance> {
        switch (this.performanceType) {
            case PerformanceType.Main:
                return this.componentFactoryResolver.resolveComponentFactory(SummaryPerformanceComponent);
            case PerformanceType.Product:
                return this.componentFactoryResolver.resolveComponentFactory(ProductPerformanceComponent);
            default:
                throw new Error('Not implemented yet');
        }
    }

    private createPerformance() {
        const factory = this.getComponentFactory();
        const component = this.container.createComponent(factory);
        this.components.push(component);
        const performance = component.instance;
        this.service.activePerformance = this.components[this.components.length - 1].instance;
        this.service.performances = this.components.map(item => item.instance);
        performance.query();
        this.afterPerformanceChange();
    }

    private afterPerformanceChange() {
        this.updateBreadcrumb();
        this.showCurrentAndHideLast();
    }

    private updateBreadcrumb() {
        this.breadcrumbConfig = {
            goBack: (index) => this.goBack(index)
        };
        this.breadcrumbItems = [];
        this.components.forEach(component => {
            const componentTitle = component.instance.title;
            this.breadcrumbItems.push(componentTitle);
        });
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

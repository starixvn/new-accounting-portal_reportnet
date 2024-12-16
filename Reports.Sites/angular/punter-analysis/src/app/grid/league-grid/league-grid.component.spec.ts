import { DebugElement } from '@angular/core';
import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { GridService } from '../grid.service';
import { GridType, LiveStatus } from '../grid.model';
import { LeagueGridService } from './league-grid.service';
import { LeagueGridModel } from './league-grid.model';
import { LeagueGridComponent } from './league-grid.component';
import { fakeData } from './league-grid.model.spec';
import { AppModule } from '../../app.module';
import { expect } from '../../../testing';

class Page {
    constructor(
        private fixture: ComponentFixture<LeagueGridComponent>
    ) { }

    queryEntityRow(rowIndex: number): DebugElement {
        const tableDebugElements = this.fixture.debugElement.queryAll(By.css('table'));
        return tableDebugElements[1].query(By.css('tr'));
    }
}

describe('LeagueGridComponent', () => {
    let fixture: ComponentFixture<LeagueGridComponent>;
    let component: LeagueGridComponent;
    let page: Page;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(LeagueGridComponent);
        component = fixture.componentInstance;
        page = new Page(fixture);
    });

    it('should have no table if no binding data', () => {
        fixture.detectChanges();
        const tableDebugElements = fixture.debugElement.queryAll(By.css('table'));
        expect(tableDebugElements.length).toBe(0);
    });

    it('should have one table to show message "There is no data"', () => {
        component.data = {
            GroupNames: [],
            Entities: [],
            Total: null
        };
        fixture.detectChanges();
        const tableDebugElements = fixture.debugElement.queryAll(By.css('table'));
        expect(tableDebugElements.length).toBe(2);
        const firstTdCell = tableDebugElements[1].query(By.css('th'));
        expect(firstTdCell.nativeElement).toHaveText('There is no data');
    });

    it('should mark hasData as false if entities is empty', () => {
        expect(component.hasData).toBe(false);
    });

    describe('mock', () => {
        let gridService: GridService;
        let selectedGridType: GridType;
        let service: LeagueGridService;

        beforeEach(() => {
            gridService = fixture.debugElement.injector.get(GridService);
            gridService.generate = (gridType) => selectedGridType = gridType;
            const fakeLeagueGridModel: LeagueGridModel = {
                betTypeId: 1,
                betTypeName: 'Bet Type 1',
                liveStatusId: LiveStatus.NonLive,
                sportId: 1,
                sportName: 'Sport 1',
                custId: 1,
                custName: 'Customer 1'
            };

            service = fixture.debugElement.injector.get(LeagueGridService);
            service.model = fakeLeagueGridModel;
            spyOn(service, 'query').and.returnValue(Observable.of(fakeData));

            component.query().subscribe();
            fixture.detectChanges();
        });

        it('should mark hasData as true if entities is not empty', () => {
            expect(component.hasData).toBe(true);
        });

        it('should have title "Bet Type 1 - Non-Live"', () => {
            expect(component.title).toBe('Bet Type 1 - Non-Live');
        });

        it('should invoke exportExcel in service when call exportExcel', () => {
            const spy = spyOn(service, 'exportExcel').and.callFake(() => { });
            component.exportExcel();
            expect(spy.calls.count()).toBe(1);
        });

        it('should have 3 tables', () => {
            const tables = fixture.debugElement.queryAll(By.css('table'));
            expect(tables.length).toBe(3);
        });
    });
});

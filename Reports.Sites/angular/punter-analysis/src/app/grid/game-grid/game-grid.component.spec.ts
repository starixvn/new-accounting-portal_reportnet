import { DebugElement } from '@angular/core';
import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { GridService } from '../grid.service';
import { GridType } from '../grid.model';
import { BetCountGridService } from '../bet-count-grid/bet-count-grid.service';
import { BetCountGridModel } from '../bet-count-grid/bet-count-grid.model';
import { AppModule } from '../../app.module';
import { expect } from '../../../testing';
import { GameGridComponent } from './game-grid.component';
import { GameGridService } from './game-grid.service';
import { GameGridModel } from './game-grid.model';
import { BetTypeGroupGridModel } from '../bet-type-group-grid/bet-type-group-grid.model';
import { fakeData } from './game-grid.model.spec';
import { BetTypeGroupGridService } from '../bet-type-group-grid/bet-type-group-grid.service';

class Page {
    constructor(
        private fixture: ComponentFixture<GameGridComponent>
    ) { }

    queryEntityRow(rowIndex: number): DebugElement {
        const tableDebugElements = this.fixture.debugElement.queryAll(By.css('table'));
        const trs = tableDebugElements[1].queryAll(By.css('tr'));
        return trs[rowIndex];
    }

    queryTotalRow(): DebugElement {
        const tableDebugElements = this.fixture.debugElement.queryAll(By.css('table'));
        return tableDebugElements[2].query(By.css('tr'));
    }
}

describe('GameGridComponent', () => {
    let fixture: ComponentFixture<GameGridComponent>;
    let component: GameGridComponent;
    let page: Page;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(GameGridComponent);
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
        let service: GameGridService;

        beforeEach(() => {
            gridService = fixture.debugElement.injector.get(GridService);
            gridService.generate = (gridType) => selectedGridType = gridType;

            const fakeGameGridModel: GameGridModel = {
                custId: 1,
                custName: 'Customer 1',
                sportId: 43,
                sportName: 'E-Sports'
            };

            service = fixture.debugElement.injector.get(GameGridService);
            service.model = fakeGameGridModel;
            spyOn(service, 'query').and.returnValue(Observable.of(fakeData));

            component.query().subscribe();
            fixture.detectChanges();
        });

        it('should mark hasData as true if entities is not empty', () => {
            expect(component.hasData).toBe(true);
        });

        it('should have title "E-Sports"', () => {
            expect(component.title).toBe('E-Sports');
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

        it('should invoke to generate BetType when click on the first cell', () => {
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));
            const link = tds[0].query(By.css('a'));
            link.triggerEventHandler('click', {});
            expect(selectedGridType).toBe(GridType.BetTypeGroup);
            const betTypeGridService = fixture.debugElement.injector.get(BetTypeGroupGridService);
            const expectedModel: BetTypeGroupGridModel = {
                custId: 1,
                custName: 'Customer 1',
                sportId: 43,
                sportName: 'E-Sports',
                leagueGroupId: 1,
                leagueGroupName: 'group 1'
            };
            expect(betTypeGridService.model).toEqual(expectedModel);
        });

        it('should invoke to generate BetCount when click on the fifth cell', () => {
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));
            const link = tds[4].query(By.css('a'));
            link.triggerEventHandler('click', {});
            expect(selectedGridType).toBe(GridType.BetCount);
            const betCountGridService = fixture.debugElement.injector.get(BetCountGridService);
            const expectedModel: BetCountGridModel = {
                fromGridType: GridType.Game,
                custId: 1,
                custName: 'Customer 1',
                sportId: 43,
                sportName: 'E-Sports',
                leagueGroupId: 1,
                leagueGroupName: 'group 1'
            };
            expect(betCountGridService.model).toEqual(expectedModel);
        });

        it('all cells of first row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['group 1', '13,024.23', '2,490.18', '523.02%', '10'];
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });

        it('all cells of second row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['group 2', '12,337.55', '2,712.90', '454.77%', '31'];
            const row = page.queryEntityRow(1);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });

        it('all cells of total row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['Total', '25,361.78', '5,203.08', '487.44%', '41'];
            const row = page.queryTotalRow();
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });
    });
});

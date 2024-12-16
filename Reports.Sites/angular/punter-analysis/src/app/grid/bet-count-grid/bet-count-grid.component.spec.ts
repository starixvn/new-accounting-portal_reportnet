import { DebugElement } from '@angular/core';
import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { GridService } from '../grid.service';
import { GridType } from '../grid.model';
import { BetCountGridService } from './bet-count-grid.service';
import { BetCountGridModel } from './bet-count-grid.model';
import { BetCountGridComponent } from './bet-count-grid.component';
import { BetCountData } from './bet-count-grid.model';
import { AppModule } from '../../app.module';
import { fakeUserFilterModel } from '../../filter/filter.model.spec';
import { expect } from '../../../testing';

class Page {
    constructor(
        private fixture: ComponentFixture<BetCountGridComponent>
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

describe('BetCountGridComponent', () => {
    let fixture: ComponentFixture<BetCountGridComponent>;
    let component: BetCountGridComponent;
    let page: Page;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(BetCountGridComponent);
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
        let service: BetCountGridService;

        beforeEach(() => {
            gridService = fixture.debugElement.injector.get(GridService);
            gridService.filterModel = fakeUserFilterModel;
            gridService.generate = (gridType) => selectedGridType = gridType;
            const fakeBetCountGridModel: BetCountGridModel = {
                fromGridType: GridType.UserName,
                custId: 1,
                custName: 'Customer 1',
                sportId: 1,
                liveStatusId: 2,
                betTypeId: 1
            };

            const fakeData: BetCountData = {
                Entities: [
                    { Date: '2016-12-19', WinLoss: 13024.2343, BetCount: 10, WinCount: 6, DrawCount: 4 },
                    { Date: '2016-12-26', WinLoss: 12337.5491, BetCount: 31, WinCount: 24, DrawCount: 7 },
                ],
                Total: { Date: '', WinLoss: 25361.7834, BetCount: 41, WinCount: 30, DrawCount: 11 },
            };

            service = fixture.debugElement.injector.get(BetCountGridService);
            service.model = fakeBetCountGridModel;
            spyOn(service, 'query').and.returnValue(Observable.of(fakeData));

            component.query().subscribe();
            fixture.detectChanges();
        });

        it('should mark hasData as true if entities is not empty', () => {
            expect(component.hasData).toBe(true);
        });

        it('should have title "Punter Customer 1 by bet count"', () => {
            expect(component.title).toBe('Punter Customer 1 by bet count');
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

        it('all cells of first row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['2016-12-19', '10', '6', '4', '13,024.23'];
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });

        it('all cells of second row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['2016-12-26', '31', '24', '7', '12,337.55'];
            const row = page.queryEntityRow(1);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });

        it('all cells of total row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['Total', '41', '30', '11', '25,361.78'];
            const row = page.queryTotalRow();
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });
    });
});

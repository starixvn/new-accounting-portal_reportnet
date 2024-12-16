import { DebugElement } from '@angular/core';
import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { PaginationInstance } from 'nap-components/pagination';
import { GridService } from '../grid.service';
import { GridType } from '../grid.model';
import { BetCountGridService } from '../bet-count-grid/bet-count-grid.service';
import { BetCountGridModel } from '../bet-count-grid/bet-count-grid.model';
import { AssociatedAccountGridService } from '../associated-account-grid/associated-account-grid.service';
import { AssociatedAccountGridModel } from '../associated-account-grid/associated-account-grid.model';
import { SportTypeGridService } from '../sport-type-grid/sport-type-grid.service';
import { SportTypeGridModel } from '../sport-type-grid/sport-type-grid.model';
import { SummaryGridService } from './summary-grid.service';
import { SummaryGridComponent } from './summary-grid.component';
import { fakeData } from './summary-grid.model.spec';
import { AppModule } from '../../app.module';
import { expect } from '../../../testing';

class Page {
    constructor(
        private fixture: ComponentFixture<SummaryGridComponent>
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

describe('SummaryGridComponent', () => {
    let fixture: ComponentFixture<SummaryGridComponent>;
    let component: SummaryGridComponent;
    let page: Page;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(SummaryGridComponent);
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
        let service: SummaryGridService;

        beforeEach(() => {
            gridService = fixture.debugElement.injector.get(GridService);
            gridService.generate = (gridType) => selectedGridType = gridType;
            service = fixture.debugElement.injector.get(SummaryGridService);

            spyOn(service, 'query').and.returnValue(Observable.of(fakeData));
            component.query().subscribe();
            fixture.detectChanges();
        });

        it('should mark hasData as true if entities is not empty', () => {
            expect(component.hasData).toBe(true);
        });

        it('should have 2 items for pagination', () => {
            const expectedConfig: PaginationInstance = {
                id: 'custom',
                itemsPerPage: 100,
                currentPage: 1,
                totalItems: 2
            };
            expect(component.config).toEqual(expectedConfig);
        });

        it('should have title "Main - Punter Analysis" if it is the last grid', () => {
            gridService.activeGrid = component;
            expect(component.title).toBe('Main - Punter Analysis');
        });

        it('should have title "Main" if it is not the last grid', () => {
            expect(component.title).toBe('Main');
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

        it('should invoke to generate SportType when click on the first cell', () => {
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));
            const link = tds[0].query(By.css('a'));
            link.triggerEventHandler('click', {});
            expect(selectedGridType).toBe(GridType.SportType);
            const sportTypeGridService = fixture.debugElement.injector.get(SportTypeGridService);
            const expectedModel: SportTypeGridModel = {
                custId: 23266239,
                custName: 'HecbetRMB011154'
            };
            expect(sportTypeGridService.model).toEqual(expectedModel);
        });

        it('should invoke to generate BetCount when click on the fifth cell', () => {
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));
            const link = tds[4].query(By.css('a'));
            link.triggerEventHandler('click', {});
            expect(selectedGridType).toBe(GridType.BetCount);
            const betCountGridService = fixture.debugElement.injector.get(BetCountGridService);
            const expectedModel: BetCountGridModel = {
                fromGridType: GridType.UserName,
                custId: 23266239,
                custName: 'HecbetRMB011154'
            };
            expect(betCountGridService.model).toEqual(expectedModel);
        });

        it('should invoke to generate AssociatedAccount when click on the last cell', () => {
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));
            const link = tds[tds.length - 3].query(By.css('a'));
            link.triggerEventHandler('click', {});
            expect(selectedGridType).toBe(GridType.AssociatedAccount);
            const asssociatedAccountGridService = fixture.debugElement.injector.get(AssociatedAccountGridService);
            const expectedModel: AssociatedAccountGridModel = {
                custId: 23266239,
                custName: 'HecbetRMB011154'
            };

            expect(asssociatedAccountGridService.model).toEqual(expectedModel);
        });

        it('all cells of first row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['HecbetRMB011154', '13,024.23', '2,490.18', '523.02%', '10', 'view', '', 'category'];
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });

        it('all cells of second row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['PYB0201002', '12,337.55', '2,712.90', '454.77%', '31', 'view', '', 'category'];
            const row = page.queryEntityRow(1);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });

        it('all cells of total row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['Total', '25,361.78', '5,203.08', '487.44%', '41', '', '', ''];
            const row = page.queryTotalRow();
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });
    });
});

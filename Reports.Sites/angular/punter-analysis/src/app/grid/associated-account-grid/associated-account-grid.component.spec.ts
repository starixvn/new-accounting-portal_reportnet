import { DebugElement } from '@angular/core';
import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import * as _ from 'lodash';
import { PaginationInstance } from 'nap-components/pagination';
import { GridService } from '../grid.service';
import { GridType } from '../grid.model';
import { AssociatedAccountGridService } from './associated-account-grid.service';
import { AssociatedAccountGridModel } from './associated-account-grid.model';
import { AssociatedAccountGridComponent } from './associated-account-grid.component';
import { AssociatedAccountData } from './associated-account-grid.model';
import { AppModule } from '../../app.module';
import { fakeUserFilterModel } from '../../filter/filter.model.spec';
import { expect } from '../../../testing';

class Page {
    constructor(
        private fixture: ComponentFixture<AssociatedAccountGridComponent>
    ) { }

    queryEntityRow(rowIndex: number): DebugElement {
        const tableDebugElements = this.fixture.debugElement.queryAll(By.css('table'));
        return tableDebugElements[1].queryAll(By.css('tr'))[rowIndex];
    }

    queryTotalRow(): DebugElement {
        const tableDebugElements = this.fixture.debugElement.queryAll(By.css('table'));
        return tableDebugElements[2].query(By.css('tr'));
    }
}

describe('AssociatedAccountGridComponent', () => {
    let fixture: ComponentFixture<AssociatedAccountGridComponent>;
    let component: AssociatedAccountGridComponent;
    let page: Page;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                AppModule
            ]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AssociatedAccountGridComponent);
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
            Total: {
                Username: '',
                RoleName: '',
                WinLoss: 0,
                Turnover: 0,
                BetCount: 0,
                Category: ''
            }
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
        let service: AssociatedAccountGridService;

        beforeEach(() => {
            gridService = fixture.debugElement.injector.get(GridService);
            gridService.filterModel = fakeUserFilterModel;
            gridService.generate = (gridType) => selectedGridType = gridType;
            const fakeAssociatedAccountGridModel: AssociatedAccountGridModel = {
                custId: 1,
                custName: 'Customer 1'
            };

            const fakeData: AssociatedAccountData = {
                Entities: [
                    {
                        Username: 'member01',
                        RoleName: 'Member',
                        WinLoss: 300,
                        Turnover: 500,
                        BetCount: 5,
                        Category: 'category'
                    },
                    {
                        Username: 'member02',
                        RoleName: 'Member',
                        WinLoss: 0,
                        Turnover: 0,
                        BetCount: 0,
                        Category: 'category'
                    },
                    {
                        Username: 'agent01',
                        RoleName: 'Agent',
                        WinLoss: 0,
                        Turnover: 0,
                        BetCount: 0,
                        Category: 'category'
                    }
                ],
                Total: {
                    Username: '',
                    RoleName: '',
                    WinLoss: 300,
                    Turnover: 500,
                    BetCount: 5,
                    Category: ''
                }
            };

            service = fixture.debugElement.injector.get(AssociatedAccountGridService);
            service.model = fakeAssociatedAccountGridModel;
            spyOn(service, 'query').and.returnValue(Observable.of(fakeData));

            component.query().subscribe();
            fixture.detectChanges();
        });

        it('should have 3 items for pagination', () => {
            const expectedConfig: PaginationInstance = {
                id: 'custom',
                itemsPerPage: 100,
                currentPage: 1,
                totalItems: 3
            };
            expect(component.config).toEqual(expectedConfig);
        });

        it('should mark hasData as true if entities is not empty', () => {
            expect(component.hasData).toBe(true);
        });

        it('should have title "Associated accounts of Punter Customer 1', () => {
            expect(component.title).toBe('Associated accounts of Punter Customer 1');
        });

        it('should have 3 tables', () => {
            const tables = fixture.debugElement.queryAll(By.css('table'));
            expect(tables.length).toBe(3);
        });

        it('should invoke exportExcel in service when call exportExcel', () => {
            const spy = spyOn(service, 'exportExcel').and.callFake(() => { });
            component.exportExcel();
            expect(spy.calls.count()).toBe(1);
        });

        it('all cells of first row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['member01', 'Member', '300.00', '500.00', '60.00%', '5', 'category'];
            const row = page.queryEntityRow(0);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });

        it('all cells of second row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['member02', 'Member', '0.00', '0.00', 'N/A', '0', 'category'];
            const row = page.queryEntityRow(1);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });
        it('all cells of third row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['agent01', 'Agent', '', '', '', '', ''];
            const row = page.queryEntityRow(2);
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });
        it('all cells of total row should match', () => {
            const cellValues = [];
            const expectedCellValue = ['Total', '', '300.00', '500.00', '60.00%', '5', ''];
            const row = page.queryTotalRow();
            const tds = row.queryAll(By.css('td'));

            for (const td of tds) {
                cellValues.push(td.nativeElement.innerText);
            }

            expect(cellValues).toEqual(expectedCellValue);
        });
    });
});

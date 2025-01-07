import { NO_ERRORS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import * as moment from 'moment';
import { DateHelper, DateModule } from 'nap-components/date';
import { FilterComponent } from './filter.component';
import { ToastService } from 'nap-components/toast';
import { DropdownSelectModule } from 'nap-components/dropdown-select';
import { TimePeriod } from './filter.model';

describe('FilterComponent', () => {
  let component: FilterComponent;
  let fixture: ComponentFixture<FilterComponent>;
  let toastSpy: jasmine.Spy;
  let generateSpy: jasmine.Spy;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [DateModule, DropdownSelectModule],
      declarations: [FilterComponent],
      schemas: [
        NO_ERRORS_SCHEMA
      ],
      providers: [ToastService]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterComponent);
    component = fixture.componentInstance;
    const toastService = fixture.debugElement.injector.get(ToastService);
    toastSpy = spyOn(toastService, 'activate');
    generateSpy = spyOn(component.onGenerated, 'emit');
    fixture.detectChanges();
  });

  describe('Filter default value', () => {
    it('should set "Date" is LastMonth', () => {
      expect(component.timePeriod).toBe(TimePeriod.LastMonth);
    });
  });

  describe('Generate', () => {
    let generateButton: any;
    beforeEach(() => {
      generateButton = fixture.debugElement.nativeElement.querySelector('button');
      component.startDate = DateHelper.convertStringToDateStruct('Jan 01, 2013');
      component.endDate = DateHelper.convertStringToDateStruct('Jan 01, 2015');
    });

    it(`should active toast message "Date range selection is limited to 6 months to protect system's performance!"`, () => {
      generateButton.click();
      fixture.detectChanges();
      expect(toastSpy.calls.count()).toBe(1);
      const args = toastSpy.calls.mostRecent().args;
      expect(args[0]).toBe(`Date range selection is limited to 6 months to protect system's performance!`);
      expect(generateSpy.calls.any()).toBeFalsy();
    });
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseeGridComponent } from './licensee-grid.component';

describe('LicenseeGridComponent', () => {
  let component: LicenseeGridComponent;
  let fixture: ComponentFixture<LicenseeGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseeGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

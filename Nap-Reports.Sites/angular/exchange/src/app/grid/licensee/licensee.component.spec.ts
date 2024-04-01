import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseeComponent } from './licensee.component';

describe('LicenseeComponent', () => {
  let component: LicenseeComponent;
  let fixture: ComponentFixture<LicenseeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LicenseeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

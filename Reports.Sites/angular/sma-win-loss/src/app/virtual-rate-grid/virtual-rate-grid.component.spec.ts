import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VirtualRateGridComponent } from './virtual-rate-grid.component';

describe('VirtualRateGridComponent', () => {
  let component: VirtualRateGridComponent;
  let fixture: ComponentFixture<VirtualRateGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VirtualRateGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VirtualRateGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

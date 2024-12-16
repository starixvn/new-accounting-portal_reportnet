import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GridDrillDownComponent } from './grid.component';

describe('GridDrillDownComponent', () => {
  let component: GridDrillDownComponent;
  let fixture: ComponentFixture<GridDrillDownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GridDrillDownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GridDrillDownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

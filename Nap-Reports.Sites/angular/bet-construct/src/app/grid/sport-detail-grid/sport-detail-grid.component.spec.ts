import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportDetailGridComponent } from './sport-detail-grid.component';

describe('SportDetailGridComponent', () => {
  let component: SportDetailGridComponent;
  let fixture: ComponentFixture<SportDetailGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SportDetailGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SportDetailGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

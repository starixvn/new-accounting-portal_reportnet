import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WinningProbabilityGridComponent } from './winning-probability-grid.component';

describe('WinningProbabilityGridComponent', () => {
  let component: WinningProbabilityGridComponent;
  let fixture: ComponentFixture<WinningProbabilityGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WinningProbabilityGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WinningProbabilityGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

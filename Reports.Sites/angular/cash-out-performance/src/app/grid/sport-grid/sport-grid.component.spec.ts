import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SportGridComponent } from './sport-grid.component';

describe('SportGridComponent', () => {
  let component: SportGridComponent;
  let fixture: ComponentFixture<SportGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SportGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SportGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

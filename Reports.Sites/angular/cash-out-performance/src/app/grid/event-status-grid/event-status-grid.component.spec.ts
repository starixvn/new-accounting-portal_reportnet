import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventStatusGridComponent } from './event-status-grid.component';

describe('EventStatusGridComponent', () => {
  let component: EventStatusGridComponent;
  let fixture: ComponentFixture<EventStatusGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventStatusGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventStatusGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

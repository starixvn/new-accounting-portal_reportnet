import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BettypeGridComponent } from './bettype-grid.component';

describe('BettypeGridComponent', () => {
  let component: BettypeGridComponent;
  let fixture: ComponentFixture<BettypeGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BettypeGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BettypeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

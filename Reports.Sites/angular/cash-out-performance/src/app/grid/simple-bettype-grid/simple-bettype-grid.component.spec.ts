import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleBettypeGridComponent } from './simple-bettype-grid.component';

describe('SimpleBettypeGridComponent', () => {
  let component: SimpleBettypeGridComponent;
  let fixture: ComponentFixture<SimpleBettypeGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleBettypeGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleBettypeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

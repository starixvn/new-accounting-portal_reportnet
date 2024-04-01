import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarginBySiteGridComponent } from './site-grid.component';

describe('MarginBySiteGridComponent', () => {
  let component: MarginBySiteGridComponent;
  let fixture: ComponentFixture<MarginBySiteGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarginBySiteGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarginBySiteGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

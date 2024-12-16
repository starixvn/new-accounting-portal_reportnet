import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailGameTimeGridComponent } from './detail-game-time-grid.component';

describe('DetailGameTimeGridComponent', () => {
  let component: DetailGameTimeGridComponent;
  let fixture: ComponentFixture<DetailGameTimeGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailGameTimeGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailGameTimeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

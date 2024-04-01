import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchDetailGridComponent } from './match-detail-grid.component';

describe('MatchDetailGridComponent', () => {
  let component: MatchDetailGridComponent;
  let fixture: ComponentFixture<MatchDetailGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MatchDetailGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchDetailGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

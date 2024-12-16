import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueGridComponent } from './league-grid.component';

describe('LeagueGridComponent', () => {
  let component: LeagueGridComponent;
  let fixture: ComponentFixture<LeagueGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

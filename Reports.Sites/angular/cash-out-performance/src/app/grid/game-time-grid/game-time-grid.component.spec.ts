import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameTimeGridComponent } from './game-time-grid.component';

describe('GameTimeGridComponent', () => {
  let component: GameTimeGridComponent;
  let fixture: ComponentFixture<GameTimeGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameTimeGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GameTimeGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

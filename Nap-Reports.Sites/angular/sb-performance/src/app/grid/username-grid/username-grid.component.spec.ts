import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsernameGridComponent } from './username-grid.component';

describe('UsernameGridComponent', () => {
  let component: UsernameGridComponent;
  let fixture: ComponentFixture<UsernameGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsernameGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsernameGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

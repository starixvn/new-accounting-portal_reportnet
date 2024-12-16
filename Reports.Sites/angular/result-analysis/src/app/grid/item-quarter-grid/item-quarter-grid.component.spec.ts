import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemQuarterGridComponent } from './item-quarter-grid.component';

describe('ItemQuarterGridComponent', () => {
  let component: ItemQuarterGridComponent;
  let fixture: ComponentFixture<ItemQuarterGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ItemQuarterGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemQuarterGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

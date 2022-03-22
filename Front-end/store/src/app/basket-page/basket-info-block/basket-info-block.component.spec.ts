import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasketInfoBlockComponent } from './basket-info-block.component';

describe('BasketInfoBlockComponent', () => {
  let component: BasketInfoBlockComponent;
  let fixture: ComponentFixture<BasketInfoBlockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasketInfoBlockComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasketInfoBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

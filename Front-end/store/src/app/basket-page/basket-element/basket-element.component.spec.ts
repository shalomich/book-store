import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasketElementComponent } from './basket-element.component';

describe('BasketElementComponent', () => {
  let component: BasketElementComponent;
  let fixture: ComponentFixture<BasketElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasketElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasketElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

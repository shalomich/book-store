import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchHintComponent } from './search-hint.component';

describe('SearchHintComponent', () => {
  let component: SearchHintComponent;
  let fixture: ComponentFixture<SearchHintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchHintComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchHintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

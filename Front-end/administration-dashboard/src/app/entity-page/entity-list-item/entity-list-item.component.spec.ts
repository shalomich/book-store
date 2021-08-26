import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EntityListItemComponent } from './entity-list-item.component';

describe('EntityListItemComponent', () => {
  let component: EntityListItemComponent;
  let fixture: ComponentFixture<EntityListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EntityListItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EntityListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

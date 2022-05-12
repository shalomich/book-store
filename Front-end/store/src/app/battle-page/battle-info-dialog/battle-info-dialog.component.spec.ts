import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BattleInfoDialogComponent } from './battle-info-dialog.component';

describe('BattleInfoDialogComponent', () => {
  let component: BattleInfoDialogComponent;
  let fixture: ComponentFixture<BattleInfoDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BattleInfoDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BattleInfoDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-battle-info-dialog',
  templateUrl: './battle-info-dialog.component.html',
  styleUrls: ['./battle-info-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BattleInfoDialogComponent implements OnInit {

  constructor(private readonly dialog: MatDialog) { }

  ngOnInit(): void {
  }

  public onClose(): void {
    this.dialog.closeAll();
  }
}

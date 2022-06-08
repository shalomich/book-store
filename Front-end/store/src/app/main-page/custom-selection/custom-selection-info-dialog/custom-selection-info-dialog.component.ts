import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-custom-selection-info-dialog',
  templateUrl: './custom-selection-info-dialog.component.html',
  styleUrls: ['./custom-selection-info-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class CustomSelectionInfoDialogComponent implements OnInit {

  constructor(private readonly dialog: MatDialog) { }

  ngOnInit(): void {
  }

  public onClose(): void {
    this.dialog.closeAll();
  }
}

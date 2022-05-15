import { Component, Inject, OnInit } from '@angular/core';

import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

interface DialogData {
  message: string;

  onConfirm: () => void;
}

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.css'],
})
export class ConfirmationModalComponent implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData, public dialogRef: MatDialogRef<ConfirmationModalComponent>) { }

  ngOnInit(): void {
  }

  public onConfirm(): void {
    this.data.onConfirm();
  }

  public onClose(): void {
    this.dialogRef.close();
  }
}

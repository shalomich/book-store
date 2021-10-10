import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent implements OnInit {

  @Input()
  public filesControl: AbstractControl = new FormControl();

  public files: File[] = [];

  public constructor() { }

  public ngOnInit(): void {
  }

  public handleFilesChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    console.log(target.files);
    this.filesControl.setValue(target.files);
  }
}

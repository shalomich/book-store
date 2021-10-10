import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';

import { combineLatest, merge, of, Subscription } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import fileHelper from '../../core/utils/file-helper';
import { Album } from '../../core/interfaces/album';

@Component({
  selector: 'app-file-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css'],
})
export class ImageUploadComponent implements OnInit, OnDestroy {

  @Input()
  public imagesControl: AbstractControl = new FormControl();

  public images: File[] = [];

  public titleImageName = new FormControl('');

  private readonly subscriptions = new Subscription();

  public constructor() {}

  public ngOnInit(): void {
    const sub = this.titleImageName.valueChanges.pipe(
      map(_ => this.imagesControl.setValue({
        titleImageName: this.titleImageName.value,
        ...this.imagesControl.value,
      })),
    ).subscribe();

    this.subscriptions.add(sub);
  }

  public ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  public handleFilesChange(event: Event): void {
    const target = event.target as HTMLInputElement;
    const files: File[] = Array.prototype.slice.call(target.files);

    const sub = of(files).pipe(
      map(filesData => filesData.map(file => fileHelper.fileToImage(file))),
      switchMap(filesData => combineLatest(filesData)),
      map(filesData => this.imagesControl.setValue({
        images: filesData,
      } as Album)),
    )
      .subscribe();

    this.subscriptions.add(sub);
  }
}

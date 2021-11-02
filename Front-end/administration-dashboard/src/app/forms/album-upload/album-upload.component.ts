import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';

import { combineLatest, merge, of, Subscription } from 'rxjs';
import { map, startWith, switchMap } from 'rxjs/operators';

import { Album } from '../../core/interfaces/album';
import { ImageConverterService } from '../../core/services/image-converter.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './album-upload.component.html',
  styleUrls: ['./album-upload.component.css'],
})
export class AlbumUploadComponent implements OnInit, OnDestroy {

  @Input()
  public albumControl: AbstractControl = new FormControl();

  public titleImageNameControl = new FormControl('');

  private readonly subscriptions = new Subscription();

  public constructor(private readonly imageConverterService: ImageConverterService) {}

  public ngOnInit(): void {
    this.titleImageNameControl.setValue(this.albumControl.value.titleImageName);
    const sub = this.titleImageNameControl.valueChanges.pipe(
      map(_ => this.albumControl.setValue({
        ...this.albumControl.value,
        titleImageName: this.titleImageNameControl.value,
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

    if (!files.length) {
      this.albumControl.setValue({
        ...this.albumControl.value,
        images: [],
      } as Album);
    } else {
      const sub = of(files).pipe(
        map(filesData => filesData.map(file => this.imageConverterService.fileToImage(file))),
        switchMap(filesData => combineLatest(filesData)),
        map(filesData => this.albumControl.setValue({
          ...this.albumControl.value,
          images: filesData,
        } as Album)),
      )
        .subscribe();

      this.subscriptions.add(sub);
    }
  }
}

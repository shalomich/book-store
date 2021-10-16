import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { readFileAsDataURL } from '@webacad/observable-file-reader';
import { map, switchMap } from 'rxjs/operators';

import { Base64Image } from '../interfaces/base64-image';

@Injectable({
  providedIn: 'root',
})
export class ImageConverterService {

  public constructor() { }

  public fileToImage(inputFile: File): Observable<Base64Image> {
    return readFileAsDataURL(inputFile).pipe(
      switchMap(file => {
        const image = new Image();
        image.src = file;

        const resultImage$ = new Subject<Base64Image>();

        image.onload = function() {
          let fileString = file.split(':')[1];
          const format = fileString.split(';')[0];
          fileString = fileString.split(';')[1];
          const data = fileString.split(',')[1];

          resultImage$.next({
            name: inputFile.name.split('.')[0],
            format,
            data,
            height: image.height,
            width: image.width,
          });
        };


        return resultImage$.pipe();
      }),
    );
  }
}

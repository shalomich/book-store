import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Image } from '../interfaces/image';
import { readFileAsDataURL } from '@webacad/observable-file-reader';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ImageConverterService {

  constructor() { }

  public fileToImage(inputFile: File): Observable<Image> {
    return readFileAsDataURL(inputFile).pipe(
      map(file => {
        let fileString = file.split(':')[1];
        const format = fileString.split(';')[0];
        fileString = fileString.split(';')[1];
        const data = fileString.split(',')[1];

        return {
          name: inputFile.name.split('.')[0],
          format,
          data,
        };
      }),
    );
  }
}

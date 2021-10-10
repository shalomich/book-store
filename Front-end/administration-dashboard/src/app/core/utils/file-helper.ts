import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { readFileAsDataURL } from '@webacad/observable-file-reader';

import { Image } from '../interfaces/image';

class FileHelper {
  public fileToImage(inputFile: File): Observable<Image> {
    return readFileAsDataURL(inputFile).pipe(
      map(file => this.base64ToObject(file, inputFile.name.split('.')[0])),
    );
  }

  private base64ToObject(fileData: string, fileName: string): Image {
    let fileString = fileData.split(':')[1];
    const format = fileString.split(';')[0];
    fileString = fileString.split(';')[1];
    const data = fileString.split(',')[1];

    return {
      name: fileName,
      format,
      data,
    };
  }
}

export default new FileHelper();

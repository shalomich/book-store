import { Pipe, PipeTransform } from '@angular/core';
import { Image } from '../interfaces/image';

@Pipe({
  name: 'base64ToImgSrc',
})
export class Base64ToImgSrcPipe implements PipeTransform {

  transform(image: Image): unknown {
    return `data:${image.format};base64,${image.data}`;
  }

}

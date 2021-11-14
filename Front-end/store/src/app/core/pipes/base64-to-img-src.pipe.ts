import { Pipe, PipeTransform } from '@angular/core';

import { Base64Image } from '../interfaces/base64-image';

@Pipe({
  name: 'base64ToImgSrc',
})
export class Base64ToImgSrcPipe implements PipeTransform {

  public transform(image: Base64Image): unknown {
    return `data:${image.format};base64,${image.data}`;
  }
}

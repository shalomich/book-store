import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Base64ToImgSrcPipe } from './pipes/base64-to-img-src.pipe';


@NgModule({
  declarations: [Base64ToImgSrcPipe],
  imports: [CommonModule],
  exports: [
    Base64ToImgSrcPipe
  ]
})
export class CoreModule { }

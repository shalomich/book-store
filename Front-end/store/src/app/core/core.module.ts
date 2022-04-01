import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JwtModule } from '@auth0/angular-jwt';

import { Base64ToImgSrcPipe } from './pipes/base64-to-img-src.pipe';


@NgModule({
  declarations: [Base64ToImgSrcPipe],
  imports: [
    CommonModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token'),
      },
    }),
  ],
  exports: [Base64ToImgSrcPipe],
})
export class CoreModule { }

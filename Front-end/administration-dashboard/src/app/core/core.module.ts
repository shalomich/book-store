import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { Base64ToImgSrcPipe } from './pipes/base64-to-img-src.pipe';
import { AuthGuard } from './guards/auth.guard';
import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';


@NgModule({
  declarations: [Base64ToImgSrcPipe],
  imports: [
    CommonModule,
    HttpClientModule,
  ],
  exports: [Base64ToImgSrcPipe],
  providers: [{ provide: JWT_OPTIONS, useValue: JWT_OPTIONS }, JwtHelperService],
})
export class CoreModule { }

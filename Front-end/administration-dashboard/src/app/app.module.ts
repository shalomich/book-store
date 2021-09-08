import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { MainPageComponent } from './main-page/main-page.component';
import { AppRoutingModule } from './app-routing.module';
import { EntityPageComponent } from './entity-page/entity-page.component';
import { EntityListItemComponent } from './entity-page/entity-list-item/entity-list-item.component';
import { CoreModule } from './core/core.module';
import {CommonModule} from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    EntityPageComponent,
    EntityListItemComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }

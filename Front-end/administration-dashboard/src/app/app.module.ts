import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { MainPageComponent } from './main-page/main-page.component';
import { AppRoutingModule } from './app-routing.module';
import { EntityPageComponent } from './entity-page/entity-page.component';
import { EntityListItemComponent } from './entity-page/entity-list-item/entity-list-item.component';
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
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }

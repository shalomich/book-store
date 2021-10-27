import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CommonModule } from '@angular/common';

import { MatExpansionModule } from '@angular/material/expansion';

import { MatButtonModule } from '@angular/material/button';

import { MatIconModule } from '@angular/material/icon';

import { Injector } from '@angular/core';

import { AppComponent } from './app.component';
import { ProductTypePageComponent } from './product-type-page/product-type-page.component';
import { AppRoutingModule } from './app-routing.module';
import { ProductPageComponent } from './product-page/product-page.component';
import { ProductListItemComponent } from './product-page/product-list-item/product-list-item.component';
import { CoreModule } from './core/core.module';
import { RelatedEntityPageComponent } from './related-entity-page/related-entity-page.component';
import { RelatedEntityListItemComponent } from './related-entity-page/related-entity-list-item/related-entity-list-item.component';
import { FormModule } from './forms/form.module';

export let InjectorInstance: Injector;
import {RangeFilterComponent} from "./shared/range-filter/range-filter.component";
import { PlentyFilterComponent } from './shared/plenty-filter/plenty-filter.component';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    ProductTypePageComponent,
    ProductPageComponent,
    ProductListItemComponent,
    RelatedEntityPageComponent,
    RelatedEntityListItemComponent,
    RangeFilterComponent,
    RangeFilterComponent,
    PlentyFilterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    FormModule,
    BrowserAnimationsModule,
    MatExpansionModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {
  public constructor(private injector: Injector) {
    InjectorInstance = this.injector;
  }
}

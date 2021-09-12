import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainPageComponent } from './main-page/main-page.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { RelatedEntityPageComponent } from './related-entity-page/related-entity-page.component';


const routes: Routes = [
  { path: '', redirectTo: '/dashboard/product', pathMatch: 'full' },
  { path: 'dashboard/product', component: MainPageComponent },
  { path: 'dashboard/product/:product', component: ProductPageComponent },
  { path: 'dashboard/product/:product/:relatedEntity', component: RelatedEntityPageComponent },
];

/**
 * Module for application routing.
 */
@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
})
export class AppRoutingModule { }

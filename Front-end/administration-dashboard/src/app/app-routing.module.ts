import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainPageComponent } from './main-page/main-page.component';
import { EntityPageComponent } from './entity-page/entity-page.component';


const routes: Routes = [
  { path: '', redirectTo: '/product', pathMatch: 'full' },
  { path: 'product', component: MainPageComponent },
  { path: 'product/:entity', component: EntityPageComponent},
];

/**
 * Module for application routing.
 */
@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
})
export class AppRoutingModule { }

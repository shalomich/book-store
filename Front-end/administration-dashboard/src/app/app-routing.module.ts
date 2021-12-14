import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductTypePageComponent } from './product-type-page/product-type-page.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { RelatedEntityPageComponent } from './related-entity-page/related-entity-page.component';
import { BookFormComponent } from './forms/book-form/book-form.component';
import { RelatedEntityFormComponent } from './forms/related-entity-form/related-entity-form.component';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginPageComponent } from './login-page/login-page.component';

const productFormsRoutes: Routes = [
  { path: 'dashboard/form/book', component: BookFormComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'dashboard/form/book/:id', component: BookFormComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'dashboard/form/:relatedEntity', component: RelatedEntityFormComponent, canActivate: [AuthGuard] },
  { path: 'dashboard/form/:relatedEntity/:id', component: RelatedEntityFormComponent, canActivate: [AuthGuard] },
];

const routes: Routes = [
  { path: '', redirectTo: '/dashboard/product', pathMatch: 'full' },
  { path: 'dashboard/login', component: LoginPageComponent },
  { path: 'dashboard/product', component: ProductTypePageComponent, canActivate: [AuthGuard] },
  { path: 'dashboard/product/:product', component: ProductPageComponent, canActivate: [AuthGuard] },
  { path: 'dashboard/product/:product/:relatedEntity', component: RelatedEntityPageComponent, canActivate: [AuthGuard] },
];

/**
 * Module for application routing.
 */
@NgModule({
  imports: [RouterModule.forRoot(routes.concat(productFormsRoutes), { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
  providers: [AuthGuard],
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, Routes } from '@angular/router';
import { CheckoutComponent } from './checkout.component';
import { authGuard } from '../core/guard/auth.guard';

const routes: Routes = [
  {path: '',
canActivate: [authGuard],
  component: CheckoutComponent}
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CheckoutRoutingModule { }

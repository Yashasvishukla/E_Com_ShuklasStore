import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BasketComponent } from './basket.component';

const route: Routes = [
  {path: '', component: BasketComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(route)
  ]
})
export class BasketRoutingModule { }

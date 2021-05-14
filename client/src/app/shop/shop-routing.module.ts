import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopComponent } from './shop.component';


const route: Routes = [
  {path: '', component: ShopComponent},
  {path: ':id', component: ProductDetailsComponent}
];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(route)
  ],
  exports: [RouterModule]
})
export class ShopRoutingModule { }

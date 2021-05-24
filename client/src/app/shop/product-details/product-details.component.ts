import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from 'src/app/shared/models/BasketItem';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;
  constructor(
    private shopService: ShopService,
    private activeRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this.breadcrumbService.set('@productName', '');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(): void {
    this.shopService
      .getProduct(+this.activeRoute.snapshot.paramMap.get('id'))
      .subscribe(
        (product) => {
          this.product = product;
          this.breadcrumbService.set('@productName', this.product.name);
        },
        (error) => {
          console.log(error);
        }
      );
  }

  incrementQuantity(product: IProduct): void {
    this.quantity ++;
  }

  decrementQuantity(product: IProduct): void {
    if (this.quantity > 1){
      this.quantity -- ;
    }
  }

  addItemToCart(): void
  {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }
}

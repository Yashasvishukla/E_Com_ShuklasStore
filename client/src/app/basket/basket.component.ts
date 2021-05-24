import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from '../shared/models/Basket';
import { IBasketItem } from '../shared/models/BasketItem';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  basket$: Observable<IBasket>;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  incrementQuantity(item: IBasketItem): void
  {
    this.basketService.incrementQuantity(item);
  }

  decrementQuantity(item: IBasketItem): void
  {
    this.basketService.decrementQuantity(item);
  }

  removeItemFromBasket(item: IBasketItem): void
  {
    this.basketService.removeItemFromBasket(item);
  }

}

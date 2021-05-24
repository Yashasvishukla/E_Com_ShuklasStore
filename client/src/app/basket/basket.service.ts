import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket, IBasket, IBasketTotals } from '../shared/models/Basket';
import { IBasketItem } from '../shared/models/BasketItem';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;

  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient, private toastService: ToastrService) { }

  // tslint:disable-next-line: typedef
  getBasket(basketId: string)
  {
    return this.http.get(this.baseUrl + 'basket?basketId=' + basketId).pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        this.calculateTotal();
      })
    );
  }

  // tslint:disable-next-line: typedef
  updateBasket(basket: IBasket)
  {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketSource.next(response);
      this.calculateTotal();
      console.log(response);
      this.toastService.success('Added/Updated/Removed Basket Item Successfully', 'Basket Nofitication');
    }, error => {
      this.toastService.error(error.error, 'An Error Has occured !!');
    }
    );
  }

  getCurrentBasket(): IBasket
  {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity: number = 1): void
  {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity);
    const basket: IBasket = this.getCurrentBasket() ?? this.createBasket();
    basket.items = this.addOrUpdateBasket(basket.items, itemToAdd, quantity);
    this.updateBasket(basket);
  }
  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private addOrUpdateBasket(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = items.findIndex( i => i.id === itemToAdd.id);
    if (index === -1){
      items.push(itemToAdd);
    }
    else {
      items[index].quantity += quantity;
    }

    return items;
  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.productType
    };
  }

  private calculateTotal(): void
  {
    const basket = this.getCurrentBasket();
    const shipping = 0;
    const subtotal = basket.items.reduce((a , b) => (b.price * b.quantity) + a , 0);
    const total = subtotal + shipping;
    this.basketTotalSource.next({shipping, subtotal, total});
  }

  incrementQuantity(item: IBasketItem): void
  {
    const basket = this.getCurrentBasket();
    const items = basket.items;
    const index = items.findIndex( x => x.id === item.id);
    items[index].quantity++;
    this.updateBasket(basket);
  }

  decrementQuantity(item: IBasketItem): void
  {
    const basket = this.getCurrentBasket();
    const items = basket.items;
    const index = items.findIndex( x => x.id === item.id);
    if (items[index].quantity > 1)
    {
      items[index].quantity --;
      this.updateBasket(basket);
    }
    else
    {
      this.removeItemFromBasket(item);
    }
  }
  removeItemFromBasket(item: IBasketItem): void
  {
    const basket = this.getCurrentBasket();
    if (basket.items.some( x => x.id === item.id))
    {
      basket.items = basket.items.filter( x => x.id !== item.id);
      if(basket.items.length > 0){
        this.updateBasket(basket);
      }
      else
      {
        this.deleteBasket(basket);
        this.calculateTotal();
      }
    }

  }
  deleteBasket(basket: IBasket)
  {
    return this.http.delete(this.baseUrl + 'basket?basketId=' + basket.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });
  }
}

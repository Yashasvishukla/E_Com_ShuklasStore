import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams): Observable<IPagination>
  {
    let params = new HttpParams();
    if (shopParams.brandIdSelected !== 0)
    {
      params = params.append('brandId', shopParams.brandIdSelected.toString());
    }
    if (shopParams.typeIdSelected !== 0)
    {
      params = params.append('typeId', shopParams.typeIdSelected.toString());
    }
    if (shopParams.sortSelected)
    {
      params = params.append('sort', shopParams.sortSelected);
    }

    if (shopParams.search)
    {
      params = params.append('search', shopParams.search);
    }
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'product', {observe: 'response', params})
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }

  getBrands(): Observable<IBrand[]>
  {
    return this.http.get<IBrand[]>(this.baseUrl + 'product/brands');
  }

  getTypes(): Observable<IType[]>
  {
    return this.http.get<IType[]>(this.baseUrl + 'product/types');
  }

}

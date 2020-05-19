import { ShopParams } from './../models/shopParams';
import {  IProductType } from './../models/productType';
import { IPagination } from './../models/pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { IProductBrand } from '../models/productBrand';
import {  map, delay } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.urlAPI;

  constructor(private http: HttpClient ) {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.productBrandId) {
      params = params.append('brandId', shopParams.productBrandId.toString());
    }

    if (shopParams.productTypeId) {
      params = params.append('typeId', shopParams.productTypeId.toString());
    }

    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }

    // if (shopParams.productName) {
    //   params = params.append('name',shopParams.productName);
    // }

    return this.http.get<IPagination>(`${this.baseUrl}/products`, {observe: 'response', params})
          .pipe(
            map(res => {
              return res.body;
            })
          );

  }

  getProductBrands() {
    return this.http.get<IProductBrand[]>(`${this.baseUrl}/products/brands`);
  }

  getProductTypes() {
    return this.http.get<IProductType[]>(`${this.baseUrl}/products/types`);
  }
}

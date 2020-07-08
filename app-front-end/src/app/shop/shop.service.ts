import { ShopParams } from './../models/shopParams';
import {  IProductType } from './../models/productType';
import { IPagination } from './../models/pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { IProductBrand } from '../models/productBrand';
import {  map, delay } from 'rxjs/operators';
import {IProduct} from "../models/product";
@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient ) {}

  setShopParams(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.id !== 0) {
      params = params.append('id', shopParams.id.toString());
    }

    if (shopParams.productBrandId !== 0) {
      params = params.append('brandId', shopParams.productBrandId.toString());
    }

    if (shopParams.productTypeId !== 0) {
      params = params.append('typeId', shopParams.productTypeId.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }


    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());
    return params;
  }

  getProducts(shopParams: ShopParams) {
    const params = this.setShopParams(shopParams);

    return this.http.get<IPagination>(`${this.baseUrl}/products/list-all`, {observe: 'response', params})
          .pipe(
            map(res => {
              return res.body;
            })
          );

  }

  getProduct(shopParams: ShopParams) {
    const params = this.setShopParams(shopParams);
    return this.http.get<IProduct>(`${this.baseUrl}/products/product-detail`, {observe: 'response', params})
      .pipe(
        map(res => {
          return res.body;
        })
      );;
  }
  getProductBrands() {
    return this.http.get<IProductBrand[]>(`${this.baseUrl}/products/brands`);
  }

  getProductTypes() {
    return this.http.get<IProductType[]>(`${this.baseUrl}/products/types`);
  }
}

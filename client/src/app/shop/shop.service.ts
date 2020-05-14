import {  IProductType } from './../models/productType';
import { IPagination } from './../models/pagination';
import { HttpClient } from '@angular/common/http';
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

  getProducts(brandId?: number, typeId?: number, sort?: string) {
    let params = new HttpParams();

    if (brandId) {
      params = params.append('brandId', brandId.toString())
    }

    if (typeId) {
      params = params.append('typeId', typeId.toString())
    }

    if (sort) {
      params = params.append('sort', sort)
    }

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

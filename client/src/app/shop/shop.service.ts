import {  IProductType } from './../models/productType';
import { IPagination } from './../models/pagination';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { IBrand } from '../models/brand';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.urlAPI;

  constructor(private http: HttpClient ) {}

  getProducts() {
    return this.http.get<IPagination>(`${this.baseUrl}/products`);
  }

  getBrands() {
    return this.http.get<IBrand[]>(`${this.baseUrl}/products/brands`);
  }

  getProductTypes() {
    return this.http.get<IProductType[]>(`${this.baseUrl}/products/types`);
  }
}

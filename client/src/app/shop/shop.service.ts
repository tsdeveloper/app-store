import { IPagination } from './../models/pagination';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.urlAPI;

  constructor(private http: HttpClient ) {}

  getProducts() {
    return this.http.get<IPagination>(`${this.baseUrl}/products`);
  }
}

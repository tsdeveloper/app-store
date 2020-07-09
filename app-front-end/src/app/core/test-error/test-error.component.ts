import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {environment} from "../../../environments/environment";
import {ShopParams} from '../../models/shopParams';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
baseUrl = environment.apiUrl;
validationErros: any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
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

  get404Error(){
    this.http.get(`${this.baseUrl}/products/42`).subscribe(r => {
      console.log(r);
    }, error => {
      console.log(error);
    });
  }

  get500Error(){
    this.http.get(`${this.baseUrl}/buggy/servererror`).subscribe(r => {
      console.log(r);
    }, error => {
      console.log(error);
    });
  }

  get400Error(){
    this.http.get(`${this.baseUrl}/buggy/badrequest`).subscribe(r => {
      console.log(r);
    }, error => {
      console.log(error);
    });
  }

  get400ValidationError(){
    let params = new HttpParams();
    params = params.append('id', 'testes');
    this.http.get(`${this.baseUrl}/products/product-detail`, {observe: 'response', params}).subscribe(r => {
      console.log(r);
    }, error => {
      console.log(error);
      this.validationErros = error.errors;
    });
  }

}

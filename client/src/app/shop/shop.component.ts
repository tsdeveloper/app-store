import { IBrand } from './../models/brand';
import { ShopService } from './shop.service';
import { IProduct } from './../models/product';
import { Component, OnInit } from '@angular/core';
import { IProductType } from '../models/productType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  productBrands: IBrand[];
  productTypes: IProductType[];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
this.getProducts();
this.getBrands();
this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts().subscribe(res => {
      this.products = res.data;
    }, error => console.log(error));
  }

  getBrands() {
    this.shopService.getBrands().subscribe(res => {
      this.productBrands = res;
    }, error => console.log(error));
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe(res => {
      this.productTypes = res;
    }, error => console.log(error));
  }

}

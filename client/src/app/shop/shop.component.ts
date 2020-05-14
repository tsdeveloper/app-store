import { IProductBrand } from './../models/productBrand';
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
  productBrands: IProductBrand[];
  productTypes: IProductType[];
  productBrandIdSelected = 0;
  productTypeIdSelected = 0;

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
this.getProducts();
this.getProductBrands();
this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.productBrandIdSelected, this.productTypeIdSelected).subscribe(res => {
      this.products = res.data;
    }, error => console.log(error));
  }

  getProductBrands() {
    this.shopService.getProductBrands().subscribe(res => {
      this.productBrands = [{id: 0, name: 'All'}, ...res];
    }, error => console.log(error));
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe(res => {
      this.productTypes = [{id: 0, name: 'All'}, ...res];
    }, error => console.log(error));
  }

  onProductBrandSelect(productBrandId: number) {
    this.productBrandIdSelected = productBrandId;

    this.getProducts();
  }

  onProductTypeSelect(productTypeId: number) {
    this.productTypeIdSelected = productTypeId;

    this.getProducts();
  }

}

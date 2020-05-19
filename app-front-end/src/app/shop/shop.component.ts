import { ShopParams } from './../models/shopParams';
import { IProductBrand } from './../models/productBrand';
import { ShopService } from './shop.service';
import { IProduct } from './../models/product';
import { Component, OnInit, Input } from '@angular/core';
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
  totalCount: number;
 @Input() productNameFilter = '';
  shopParams =  new ShopParams();
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
this.getProducts();
this.getProductBrands();
this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(res => {
      this.products = res.data;
      this.shopParams.pageIndex = res.pageIndex;
      this.shopParams.pageSize = res.pageSize;
      this.totalCount = res.count;
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
    this.shopParams.productBrandId = productBrandId;

    this.getProducts();
  }

  onProductTypeSelect(productTypeId: number) {
    this.shopParams.productTypeId = productTypeId;

    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onClearFilter() {
    this.shopParams = this.shopParams.resetParams();
    this.getProducts();
  }

  onSearchFilter() {
   this.getProducts();
  }

  onPageChanged(event: any) {
    this.shopParams.pageIndex = event.page;
    this.getProducts();
   }
}

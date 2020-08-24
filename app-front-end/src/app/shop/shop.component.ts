import {ShopParams} from './../models/shopParams';
import {IProductBrand} from './../models/productBrand';
import {ShopService} from './shop.service';
import {IProduct} from './../models/product';
import {Component, OnInit, Input, ViewChild, ElementRef} from '@angular/core';
import {IProductType} from '../models/productType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  products: IProduct[];
  productBrands: IProductBrand[];
  productTypes: IProductType[];
  totalCount: number;
  @Input() productNameFilter = '';
  shopParams = new ShopParams();
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];

  constructor(private shopService: ShopService) {
  }

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(res => {
      this.products = res.data;
      this.shopParams.pageNumber = res.pageIndex;
      this.shopParams.pageSize = res.pageSize;
      this.totalCount = res.count;
    }, error =>
        console.log(error)
    );
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
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onProductTypeSelect(productTypeId: number) {
    this.shopParams.productTypeId = productTypeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = this.shopParams.resetParams();
    this.shopParams.sort = 'name';
    this.getProducts();
  }

}

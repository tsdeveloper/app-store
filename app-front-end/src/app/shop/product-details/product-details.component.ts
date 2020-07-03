import { Component, OnInit } from '@angular/core';
import {IProduct} from "../../models/product";
import {ShopService} from "../shop.service";

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    this.shopService.getProduct('537b9a11-3860-7fb9-0895-9a98e16155ca%7D').subscribe(p => {
      this.product = p;
    }, error => {
      console.log(error);
    });
  }
}

import { Component, OnInit } from '@angular/core';
import {IProduct} from "../../models/product";
import {ShopService} from "../shop.service";
import {ActivatedRoute} from "@angular/router";
import {ShopParams} from "../../models/shopParams";
import {environment} from "../../../environments/environment";
import {BreadcrumbService} from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  urlPublicImage = environment.urlPublicImage;
  product: IProduct;
  shopParams = new ShopParams();
  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute,
              private bcService: BreadcrumbService) {
    this.bcService.set('@productDetails', '');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    this.shopParams.id  = +this.activateRoute.snapshot.paramMap.get('id');
    this.shopService.getProduct(this.shopParams).subscribe(p => {
      this.product = p;
      this.bcService.set('@productDetails', p.name);
    }, error => {
      console.log(error);
    });
  }
}

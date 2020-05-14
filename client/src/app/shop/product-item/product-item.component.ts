import { environment } from './../../../environments/environment';
import { IProduct } from './../../models/product';
import { Component, OnInit, Input, ViewChild } from '@angular/core';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {


@Input() product: IProduct;
  urlPublicImage = environment.urlPublicImage;
  constructor() { }


  ngOnInit(): void {
  }

}

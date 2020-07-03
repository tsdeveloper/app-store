import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from './../shared/shared.module';
import { ShopComponent } from './shop.component';
import { ShopRoutingModule } from './shop-routing.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import {RouterModule} from '@angular/router';


@NgModule({
  declarations: [ShopComponent, ProductItemComponent, ProductDetailsComponent],
  imports: [
    CommonModule,
    ShopRoutingModule,
    SharedModule,
    RouterModule
  ],
  exports:[ShopComponent, ProductItemComponent, ProductDetailsComponent]
})
export class ShopModule { }

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from './../shared/shared.module';
import { ShopComponent } from './shop.component';
import { ShopRoutingModule } from './shop-routing.module';


@NgModule({
  declarations: [ShopComponent, ProductItemComponent],
  imports: [
    CommonModule,
    ShopRoutingModule,
    SharedModule
  ],
  exports:[ShopComponent, ProductItemComponent]
})
export class ShopModule { }

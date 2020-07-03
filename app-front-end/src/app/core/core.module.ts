import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/nav-bar.component';
import {RouterModule} from '@angular/router';



@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [NavbarComponent]
})
export class CoreModule { }
